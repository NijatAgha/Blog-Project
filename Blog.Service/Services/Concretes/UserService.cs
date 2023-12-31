﻿using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Entity.ViewModels.Users;
using Blog.Service.AutoMapper.Users;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageHelper imageHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ClaimsPrincipal _user;

        public UserService(IUnitOfWork unitOfWork, IImageHelper imageHelper, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            _user = httpContextAccessor.HttpContext.User;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddVM userAddVM)
        {
            var map = mapper.Map<AppUser>(userAddVM);
            map.UserName = userAddVM.Email;
            var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userAddVM.Password) ? "" : userAddVM.Password);

            if (result.Succeeded)
            {
                var findRole = await roleManager.FindByIdAsync(userAddVM.RoleId.ToString());
                await userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
                return result;

        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
                return (result, user.Email);
            else
                return (result, null);

        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserVM>> GetAllUsersWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserVM>>(users);

            foreach (var item in map)
            {
                var findUser = await userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));
                item.Role = role;
            }

            return map;
        }

        public async Task<AppUser> GetUserByIdAsync(Guid userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return string.Join("", await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateVM userUpdateVM)
        {
            var user = await GetUserByIdAsync(userUpdateVM.Id);
            var userRole = await GetUserRoleAsync(user);
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await roleManager.FindByIdAsync(userUpdateVM.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            else
                return result;
        }

        public async Task<UserProfileVM> GetUserProfileAsync()
        {
            var userId = _user.GetLoggedInUserId();

            var getUserWithImage = await unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == userId, x => x.Image);
            var map = mapper.Map<UserProfileVM>(getUserWithImage);
            map.Image.FileName = getUserWithImage.Image.FileName;

            return map;
        }

        public async Task<Guid> UploadImageForUser(UserProfileVM userProfileVM)
        {
            var userEmail = _user.GetLoggedInEmail();

            var imageUpload = await imageHelper.Upload($"{userProfileVM.FirstName}{userProfileVM.LastName}", userProfileVM.Photo, ImageType.User);
            Image image = new(imageUpload.FullName, userProfileVM.Photo.ContentType, userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);
            return image.Id;
        }

        public async Task<bool> UpdateUserProfileAsync(UserProfileVM userProfileVM)
        {
            var userId = _user.GetLoggedInUserId();
            var user = await GetUserByIdAsync(userId);
            var isVerified = await userManager.CheckPasswordAsync(user, userProfileVM.CurrentPassword);

            if (isVerified && userProfileVM.NewPassword != null)
            {
                if (userProfileVM.CurrentPassword != userProfileVM.NewPassword)
                {
                    var result = await userManager.ChangePasswordAsync(user, userProfileVM.CurrentPassword, userProfileVM.NewPassword);

                    if (result.Succeeded)
                    {
                        await userManager.UpdateSecurityStampAsync(user);
                        await signInManager.SignOutAsync();
                        await signInManager.PasswordSignInAsync(user, userProfileVM.NewPassword, true, false);

                        mapper.Map(userProfileVM, user);

                        if (userProfileVM.Photo != null)
                            user.ImageId = await UploadImageForUser(userProfileVM);

                        await userManager.UpdateAsync(user);
                        await unitOfWork.SaveAsync();

                        return true;
                    }

                    else
                        return false;
                }

                else
                    return false;

            }
                
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(user);

                mapper.Map(userProfileVM, user);

                if (userProfileVM.Photo != null)
                    user.ImageId = await UploadImageForUser(userProfileVM);

                await userManager.UpdateAsync(user);
                await unitOfWork.SaveAsync();

                return true;
            }

            else
                return false;
        }

    }
}

