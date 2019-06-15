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
        public static string Tenant = "contosomaintenance.onmicrosoft.com";  // Domain name
        public static string ClientID = "2acf5ccd-8cad-434d-a752-7629ad2d721f"; // Application ID
        public static string RedirectUri = "msalcontosomaintenance://auth";
        public static string SignUpAndInPolicy = "B2C_1_Generic";
        public static string[] Scopes = { "https://contosomaintenance.onmicrosoft.com/2acf5ccd-8cad-434d-a752-7629ad2d721f/mobile\n" };
    }
}
