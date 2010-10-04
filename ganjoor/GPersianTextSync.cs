using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    class GPersianTextSync
    {
        public static string Sync(string inputStr)
        {
            return
                inputStr
                    .Replace('ك', 'ک')
                    .Replace('ي', 'ی')
                    .Replace("ۀ", "هٔ")
                    .Replace("ه‌ی", "هٔ")
                    .Replace("0", "۰")
                    .Replace("1", "۱")
                    .Replace("2", "۲")
                    .Replace("3", "۳")
                    .Replace("4", "۴")
                    .Replace("5", "۵")
                    .Replace("6", "۶")
                    .Replace("7", "۷")
                    .Replace("8", "۸")
                    .Replace("9", "۹");
        }
    }
}
