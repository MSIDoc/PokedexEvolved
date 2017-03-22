using Microsoft.AspNetCore.Builder;
using System;

namespace Pokedex.Services.Configuration
{
    public class IdentityOptionsConfiguration
    {
        public IdentityOptionsConfiguration(IdentityOptions options)
        {
            // password configurations
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;

            // lockout configs
            options.Lockout.MaxFailedAccessAttempts = 5;

            // Sign in configs
            options.SignIn.RequireConfirmedEmail = true;

            // User configs 
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ-_";
            options.User.RequireUniqueEmail = true;

            // Cookie configs
            options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
            options.Cookies.ApplicationCookie.LogoutPath = "/Account/Logout";
            options.Cookies.ApplicationCookie.CookieHttpOnly = false;
            options.Cookies.ApplicationCookie.CookieName = "PokeCookie";
            options.Cookies.ApplicationCookie.ReturnUrlParameter = "/Pokemon";


        }
    }
}
