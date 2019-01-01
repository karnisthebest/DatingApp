using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    // user static because we don't want to create any new instance for this class
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            
            response.Headers.Add("Application-Error", message);
            // thses two lines below will allow the above line to show 
            // we put these two lines below in to have the allow origin error to go away 
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        // we need to use "this" inside the parameter because its a extension methods
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > DateTime.Today)
                age--;
            
            return age;

        }
    }
}