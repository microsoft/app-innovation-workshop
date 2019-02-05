namespace ContosoFieldService.Helpers
{
    public static class Constants
    {
        // Backend API
        public static string BaseUrl = "__ApiManagementUrl__";
        public static string ApiManagementKey = "__ApiManagementKey__";

        // Visual Studio App Center
        public static string AppCenterIOSKey = "__IOSKey__";
        public static string AppCenterAndroidKey = "__AndroidKey__";
        public static string AppCenterUWPKey = "__UWPKey__";

        // Azure Active Directory B2C
        public static string Tenant = "__ADB2CTenant__";
        public static string ApplicationId = "__ADB2CApplicationId__";
        public static string SignUpAndInPolicy = "__ADB2CSignUpPolicy__";
        public static string[] Scopes = { "__ADB2CScopes__" };
    }
}
