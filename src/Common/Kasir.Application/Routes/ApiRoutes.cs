namespace Kasir.Application.Routes
{
    public static class ApiRoutes
    {
        public static class AppInfo
        {
            public const string Info = "api/" + nameof(AppInfo) + "/Info";

        }

        public static class Auth
        {
            public const string AddPushToken = "api/" + nameof(Auth) + "/AddPushToken";

        }

        public static class Token
        {
            public const string Refresh = "api/" + nameof(Token) + "/Refresh";
            public const string Revoke = "api/" + nameof(Token) + "/Revoke";
        }

        public static class Countries
        {
            public const string GetAll = "api/" + nameof(Countries) + "/GetAll";
        }

        public static class Words
        {
            public const string GetAll = "api/" + nameof(Words) + "/GetAll";
        }

        public static class Languages
        {
            public const string GetAll = "api/" + nameof(Languages) + "/GetAll";
        }
    }
}
