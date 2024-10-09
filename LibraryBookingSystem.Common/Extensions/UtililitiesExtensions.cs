using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace LibraryBookingSystem.Common.Extensions
{
    public static class UtililitiesExtensions
    {
        public static string NumberFormat(this decimal source)
        {
            return $"{source:n}";
        }

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

        public static bool HasSpecialChars(this string source)
        {
            return source.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public static string RemoveDecimals(this decimal source)
        {
            return source.ToString("G29");
        }

        public static MemoryStream ToMemoryStream(this Stream source)
        {
            byte[] inputBuffer = new byte[source.Length];
            source.Read(inputBuffer, 0, inputBuffer.Length);
            return new MemoryStream(inputBuffer);
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


        public static DateTime MonthEnd(this DateTime source)
        {
            var day = DateTime.DaysInMonth(source.Year, source.Month);
            return new DateTime(source.Year, source.Month, day);
        }

        public static DateTime FirstOfThisMonth(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, 1);
        }

        public static DateTime FirstOfNextMonth(this DateTime source)
        {
            return new DateTime(source.Year, source.Month, 1).AddMonths(1);
        }


        public static string Masked(this string source, int start, int count)
        {
            return source.Masked('x', start, count);
        }

        public static string Masked(this string source, char maskValue, int start, int count)
        {
            var firstPart = source.Substring(0, start);
            var lastPart = source.Substring(start + count);
            var middlePart = new string(maskValue, count);

            return firstPart + middlePart + lastPart;
        }

        public static byte[] HexStringToHex(this string inputHex)
        {
            inputHex = Regex.Replace(inputHex, @"\r|\n", "");
            inputHex = Regex.Replace(inputHex, @"([\da-fA-F]{2}) ?", "0x$1 ");
            inputHex = Regex.Replace(inputHex, @" +$", "");
            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                var foo = inputHex.Substring(i * 2, 2);
                resultantArray[i] = Convert.ToByte(foo, 16);
            }
            return resultantArray;
        }

        public static string HexString2B64String(this string input)
        {
            return Convert.ToBase64String(input.HexStringToHex());
        }

        public static decimal ToKobo(this decimal input)
        {
            return input * 100;
        }
    }
}
