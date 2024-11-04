namespace WGL.Auth.Domain.Constants
{
    public static class Constants
    {
        private const string _getOneLoginToken = "OneLoginToken";
        private const string _getOneLoginUserToken = "OneLoginUserToken";
        private const string _getTokenMethod= "auth/oauth2/v2/token";
        private const string _Authorization = "Authorization";
        private const string _grantType = "grant_type";
        private const string _userName = "UserName";
        private const string _password = "Password";
        private const string _bearer = "Bearer ";
        private const string _mediaType = "application/json";


        private const string _getUserTokenMethod = "api/1/login/auth";


        public static string getOneLoginToken = _getOneLoginToken;
        public static string getOneLoginUserToken = _getOneLoginUserToken;
        public static string getTokenMethod = _getTokenMethod;
        public static string Authorization = _Authorization;
        public static string grantType = _grantType;
        public static string userName = _userName;
        public static string password = _password;

        public static string getUserTokenMethod = _getUserTokenMethod;
        public static string Bearer= _bearer;
        public static string MediaType = _mediaType;
    }
}
