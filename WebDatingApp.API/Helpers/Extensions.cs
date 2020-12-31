using System;
using Microsoft.AspNetCore.Http;

namespace WebDatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Expose-Header","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }

        public static int CalculateAge(this DateTime _dateTime){
            var age = DateTime.Today.Year - _dateTime.Year;

            if(_dateTime.AddYears(age) > DateTime.Today)
                return age-1;
            else
                return age;
        }
    }
}       