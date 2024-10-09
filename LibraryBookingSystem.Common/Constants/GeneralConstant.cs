namespace LibraryBookingSystem.Common.Constants
{
    public class GeneralConstant
    {
        public const int MaxFilePathLength = 256;
        public const int MaxCardNameLength = 26;
        public const decimal MaxLimitAmount = 1000000M;
        public const int AcctNumberLength = 10;
        public const int BvnLength = 11;
        public const int MaxUrlLength = 1024;
        public const int Length8 = 8;
        public const int Length16 = 16;
        public const int Length32 = 32;
        public const int LengthGuidString = 36;
        public const int Length64 = 64;
        public const int Length128 = 128;
        public const int Length256 = 256;
        public const int Length512 = 512;
        public const int Length1024 = 1024;
        public const int Length2048 = 2048;
        public const int Length4096 = 4096;
        public const int MaxNameLength = 64;
        public const int PhoneNumberLength = 15;
        public const int LimitValidityMonths = 2;
        public const string AllowedMIMEType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/msword,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,text/csv,application/vnd.openxmlformats-officedocument.presentationml.presentation,application/vnd.ms-powerpoint,application/pdf,image/bmp,image/jpeg,image/png,image/jpg";
        public const string ExcelCell = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        public const string PlatformsEnabled = "http://localhost:4200,http://localhost:3000,https://premium-trust-app.web.app,https://premiummobiledashboard.web.app";
        public const string PermittedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string accessKey = Environment.GetEnvironmentVariable("accessKey") ?? string.Empty;
        public const string secretKey = "";
        public const string AwsRegion = "";
        public const string TestOTP = "122333";
        public const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
        public const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string NUMERIC_CHARACTERS = "0123456789";
        public const string SPECIAL_CHARACTERS = @"";
        public const int PASSWORD_LENGTH_MIN = 8;
        public const int PASSWORD_LENGTH_MAX = 128;
        public const string PIN_PROHIBITED = "1111,2222,3333,4444,5555,6666,7777,8888,9999,0000,1234,4321,1212";
        public const int API_TIMEOUT = 5;
        public const bool LOG_API = true;
        public static readonly string CacheUrl = Environment.GetEnvironmentVariable("cacheUrl") ?? string.Empty;
        public static readonly string CacheSlaveUrl = Environment.GetEnvironmentVariable("cacheSlaveUrl") ?? string.Empty;
        public static readonly string CachePassword = Environment.GetEnvironmentVariable("cachePassword") ?? string.Empty;
        public static readonly string Env = Environment.GetEnvironmentVariable("env") ?? string.Empty;
    }
}
