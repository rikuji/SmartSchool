using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currenDate = DateTime.UtcNow;
            var age = currenDate.Year - dateTime.Year;

            if (currenDate < dateTime.AddYears(age))
                age--;

            return age;
        }
    }
}
