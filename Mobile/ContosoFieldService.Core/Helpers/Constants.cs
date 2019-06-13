namespace ContosoFieldService.Helpers
{
    public static class Constants
    {
        // Backend API
        public static string BaseUrl = "https://contosomaintenanceapi.azurewebsites.net/api/";
        public static string ApiManagementKey = "__ApiManagementKey__";

        // Visual Studio App Center
        public static string AppCenterIOSKey = "__IOSKey__";
        public static string AppCenterAndroidKey = "__AndroidKey__";
        public static string AppCenterUWPKey = "__UWPKey__";

        // Azure Active Directory B2C
        public static string Tenant = "__ADB2CTenant__";
        public static string SignUpAndInPolicy = "__ADB2CSignUpPolicy__";

        public static string ApplicationId = "2acf5ccd-8cad-434d-a752-7629ad2d721f";
        public static string RedirectUri = "msalcontosomaintenance://auth";
        public static string[] Scopes = { "https://contosomaintenance.onmicrosoft.com/2acf5ccd-8cad-434d-a752-7629ad2d721f/mobile\n" };
    }
}
