namespace ContosoFieldService.Helpers
{
    public static class Constants
    {
        public static string BaseUrl = "http://contosomaintenance.azurewebsites.net/api/";
        public static string ApiManagementKey = "__ApiManagementKey__";

        public static string AppCenterIOSKey = "__IOSKey__";
        public static string AppCenterAndroidKey = "__AndroidKey__";
        public static string AppCenterUWPKey = "__UWPKey__";

        // Azure Active Directory B2C
        public static string Tenant = "__ADB2CTenant__";
        public static string ClientID = "__ADB2CClientId__";
        public static string SignUpAndInPolicy = "__ADB2CSignUpPolicy__";
        public static string[] Scopes = { "__ADB2CScopes__" };
    }
}
