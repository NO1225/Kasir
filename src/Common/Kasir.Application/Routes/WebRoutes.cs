using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Application.Routes
{
    public static class WebRoutes
    {
        public static class Auth
        {
            public const string Index = nameof(Auth) + "/index";
            public const string SignIn = nameof(Auth) + "/SignIn";

        }

        public static class Countries
        {
            public const string Index = nameof(Countries) + "/index";
            public const string Create = nameof(Countries) + "/Create";
            public const string Edit = nameof(Countries) + "/Edit";
            public const string Details = nameof(Countries) + "/Details";
            public const string Delete = nameof(Countries) + "/Delete";

        }
    }
}
