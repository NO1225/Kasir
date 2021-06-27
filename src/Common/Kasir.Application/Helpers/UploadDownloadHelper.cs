namespace Kasir.Application.Helpers
{
    public static class UploadDownloadHelper
    {
        public const string ROOT_PATH = "wwwroot";


        public const string SETUP_FILE_NAME = "setup.apk";

        public const string RELEASES_FOLDER_PATH = "/wwwroot/files/releases/";

        public const string COUNTRY_IMAGE_FOLDER_PATH = "files/images/country/";

        public static string ShowCountryImage(string imageName)
        {
            return "/"+ COUNTRY_IMAGE_FOLDER_PATH + imageName;
        }
    }
}
