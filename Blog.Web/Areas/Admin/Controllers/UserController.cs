﻿using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Entity.ViewModels.Users;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstracts;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using static Blog.Web.ResultMessages.Messages;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private readonly IValidator<AppUser> validator;

        public UserController(IUserService userService, IMapper mapper, IToastNotification toastNotification, IValidator<AppUser> validator)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
            this.validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var result = await userService.GetAllUsersWithRoleAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await userService.GetAllRolesAsync();
            return View(new UserAddVM() { Roles = roles });
           
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddVM userAddVM)
        {
            var map = mapper.Map<AppUser>(userAddVM);
            var validation = await validator.ValidateAsync(map);
            var roles = await userService.GetAllRolesAsync();

            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserAsync(userAddVM);

                if (result.Succeeded)
                {               
                    toastNotification.AddSuccessToastMessage(Messages.User.AddSuccess(userAddVM.Email), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }

                else
                {
                    result.AddIdentModelState(this.ModelState);
                    validation.AddModelState(this.ModelState);

                    return View(new UserAddVM() { Roles = roles });
                }

            }

            return View(new UserAddVM() { Roles = roles });
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            var roles = await userService.GetAllRolesAsync();

            var map = mapper.Map<UserUpdateVM>(user);
            map.Roles = roles;
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateVM userUpdateVM)
        {
            var user = await userService.GetUserByIdAsync(userUpdateVM.Id);

            if (user != null)
            {
                var roles = await userService.GetAllRolesAsync();

                if (ModelState.IsValid)
                {
                    var map = mapper.Map(userUpdateVM, user);
                    var validation = await validator.ValidateAsync(map);

                    if (validation.IsValid)
                    {
                        user.UserName = userUpdateVM.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        var result = await userService.UpdateUserAsync(userUpdateVM); 

                        if (result.Succeeded)
                        {                            
                            toastNotification.AddSuccessToastMessage(Messages.User.UpdateSuccess(userUpdateVM.Email), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }

                        else
                        {
                            result.AddIdentModelState(this.ModelState);
                            validation.AddModelState(this.ModelState);

                            return View(new UserUpdateVM() { Roles = roles });
                        }
                    }

                    else
                    {
                        validation.AddModelState(this.ModelState);
                        return View(new UserUpdateVM() { Roles = roles });

                    }

                }
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid userId)
        {
                var result = await userService.DeleteUserAsync(userId);

                if (result.identityResult.Succeeded)
                {
                    toastNotification.AddSuccessToastMessage(Messages.User.Delete(result.email), new ToastrOptions() { Title = "Əməliyyat uğurla icra olundu." });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }

                else
                {
                    result.identityResult.AddIdentModelState(this.ModelState);
                }
                      
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await userService.GetUserProfileAsync();

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileVM userProfileVM)
        {

            if (ModelState.IsValid && userProfileVM.CurrentPassword != null)
            {
                var result = await userService.UpdateUserProfileAsync(userProfileVM);

                if (result)
                {
                    toastNotification.AddSuccessToastMessage("Profil yeniləmə əməliyyatı uğurla icra olundu.", new ToastrOptions() { Title = "Uğurlu əməliyyat" });
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                else
                {
                    var profile = await userService.GetUserProfileAsync();
                    toastNotification.AddErrorToastMessage("Profil yeniləmə əməliyyatı zamanı xəta baş verdi.", new ToastrOptions() { Title = "Uğursuz əməliyyat" });
                    return View(profile);
                }

            }
            else
            {
                var profile = await userService.GetUserProfileAsync();
                toastNotification.AddErrorToastMessage("Profil yeniləmə əməliyyatı zamanı xəta baş verdi. Cari şifrə boş buraxıla bilməz!", new ToastrOptions() { Title = "Uğursuz əməliyyat" });
                return View(profile);
            }
        }


    }
}
