using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LibraryBookingSystem.Common.Extensions
{
    public static class UtililitiesExtensions
    {

        public static string Stringify<T>(this T source)
        {
            if (source == null) return string.Empty;
            var camelSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(source, Formatting.None,
                camelSettings);
        }

        public static T? ToObject<T>(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return default;
            return JsonConvert.DeserializeObject<T>(source);
        }

        #region Nulls Or Empties
        public static bool IsNullOrEmpty(this DateTime? source)
        {
            return source == null || source.Value == DateTime.MinValue;
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNullOrEmpty(this int? source)
        {
            return source == null || source == 0;
        }

        public static bool IsNullOrEmpty(this decimal? source)
        {
            return source == null || source == 0;
        }
        public static bool IsNullOrEmpty(this object source)
        {
            return source == null;
        }

        public static bool IsNullOrEmpty<T>(this T source)
        {
            return source == null;
        }

        public static bool IsNullOrEmpty<T>(this List<T> source)
        {
            return source == null || !source.Any();
        }


        public static bool IsNotNullOrEmpty(this DateTime? source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty(this string source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty(this int? source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty(this decimal? source)
        {
            return !source.IsNullOrEmpty();
        }
        public static bool IsNotNullOrEmpty(this object source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty<T>(this T source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty<T>(this List<T> source)
        {
            return !source.IsNullOrEmpty();
        }

        #endregion

        public static long ToTimestamp(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        public static long? ToTimestamp(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
            return new DateTimeOffset(dateTime.Value).ToUnixTimeSeconds();
            }
            return null;
        }

       

       
    }
}
