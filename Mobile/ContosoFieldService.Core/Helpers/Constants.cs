namespace ContosoFieldService.Helpers
{
    public static class Constants
    {
        public static string BaseUrl = "http://contosomaintenance.azurewebsites.net/api/";
        public static string ApiManagementKey = "__ApiManagementKey__";

        public static string AppCenterIOSKey = "0f5e85f5-7774-4fbd-ab6c-f1f018845ce2";
        public static string AppCenterAndroidKey = "d08e7d3d-a10b-40c4-93bf-008a53c8a35e";
        public static string AppCenterUWPKey = "__UWPKey__";

        // Azure Active Directory B2C
        public static string Tenant = "myawesomenewstartup.onmicrosoft.com"; // __ADB2CTenant__
        public static string ApplicationId = "4844ddcc-786e-460a-a2f5-873dcc649207"; // __ADB2CClientId__
        public static string SignUpAndInPolicy = "B2C_1_GenericSignUpSignIn"; // __ADB2CSignUpPolicy__
        public static string[] Scopes = { "https://myawesomenewstartup.onmicrosoft.com/backend/read_only" }; // __ADB2CScopes__
    }
}
