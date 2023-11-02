namespace Blog.Web.ResultMessages
{
    public static class Messages
    {
        public static class Article
        {
            public static string AddSuccess(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə uğurla əlavə edilmişdir.";
            }

            public static string AddError(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə əlavə edilərkən xəta meydana gəldi! ";
            }

            public static string UpdateSuccess(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə uğurla yenilənmişdir.";
            }

            public static string UpdateError(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə yenilənərkən xəta meydana gəldi! ";
            }

            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə uğurla silinmişdir.";
            }

            public static string UndoDelete(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə uğurla geri qaytarılmışdır.";
            }


        }


        public static class Category
        {
            public static string AddSuccess(string categoryName)
            {
                return $"{categoryName} başlıqlı kateqoriya uğurla əlavə edilmişdir.";
            }

            public static string AddError(string categoryName)
            {
                return $"{categoryName} başlıqlı kateqoriya əlavə edilərkən xəta meydana gəldi! ";
            }

            public static string UpdateSuccess(string categoryName)
            {
                return $"{categoryName} başlıqlı kateqoriya uğurla yenilənmişdir.";
            }

            public static string UpdateError(string categoryName)
            {
                return $"{categoryName} başlıqlı kateqoriya yenilənərkən xəta meydana gəldi! ";
            }

            public static string Delete(string categoryName)
            {
                return $"{categoryName} başlıqlı kateqoriya uğurla silinmişdir.";
            }

            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} başlıqlı kateqoriya uğurla geri qaytarılmışdır.";
            }

        }


        public static class User
        {
            public static string AddSuccess(string userName)
            {
                return $"{userName} e-poçt ünvanı olan istifadəçi uğurla əlavə edilmişdir.";
            }

            public static string AddError(string userName)
            {
                return $"{userName} e-poçt ünvanı olan istifadəçi əlavə edilərkən xəta meydana gəldi! ";
            }

            public static string UpdateSuccess(string userName)
            {
                return $"{userName} e-poçt ünvanı olan istifadəçi uğurla yenilənmişdir.";
            }

            public static string UpdateError(string userName)
            {
                return $"{userName} e-poçt ünvanı olan istifadəçi yenilənərkən xəta meydana gəldi! ";
            }

            public static string Delete(string userName)
            {
                return $"{userName} e-poçt ünvanı olan istifadəçi uğurla silinmişdir.";
            }

            public static string ProfileUpdateSuccess()
            {
                return "Məlumatlarınız uğurla yenilənmişdir.";
            }

            public static string ProfileUpdateError()
            {
                return "Məlumatlarınız yenilənərkən xəta yarandı!";
            }

            public static string ProfileCurrentPasswordNullError()
            {
                return "Zəhmət olmasa, cari şifrəni daxil edin!";
            }

            public static string ProfileUpdateWithPasswordSuccess()
            {
                return "Məlumatlarınız və şifrəniz uğurla yenilənmişdir.";
            }

            public static string ProfileSamePasswordUpdateError()
            {
                return "Əvvəlki şifrə ilə yeni şifrə eyni ola bilməz!";
            }


        }







    }
}
