using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer // أو الـ Namespace الخاص بمشروعك
{
    public static class clsGlobal
    {
        // هذا المتغير هو اللي راح يحفظ بيانات المستخدم الحالي بعد الـ Login
        // افترضنا إن عندك كلاس للمستخدمين اسمه clsUser، لو اسمه غير هيك بتغيره
        public static clsBLUser CurrentUser = null;
    }
}