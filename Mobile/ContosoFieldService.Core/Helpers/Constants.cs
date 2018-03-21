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
        public static string Tenant = "myawesomenewstartup.onmicrosoft.com"; // __ADB2CTenant__
        public static string ClientID = "4844ddcc-786e-460a-a2f5-873dcc649207"; // __ADB2CClientId__
        public static string SignUpAndInPolicy = "B2C_1_GenericSignUpSignIn"; // __ADB2CSignUpPolicy__
        public static string[] Scopes = { "https://myawesomenewstartup.onmicrosoft.com/backend/read_only" }; // __ADB2CScopes__
    }
}
