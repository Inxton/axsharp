using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Inxton.Operon
{
    public static class Helpers
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool ascending) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var keySelector = Expression.Lambda(property, parameter);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";

            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IQueryable<T>)method.Invoke(null, new object[] { source, keySelector });
        }

        public static List<int> ParseResultString(string input)
        {
            return input?.Split(',')
                        .Select(s => s.Trim())
                        .Where(s => int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToList() ?? [];
        }

        /// <summary>
        /// Extracts the HTML content of a <div> with a specified id from a .razor file.
        /// </summary>
        /// <param name="filePath">The full path to the .razor file.</param>
        /// <param name="targetDivId">The id of the <div> to extract.</param>
        /// <returns>The outer HTML of the found <div>, or null if not found.</returns>
        public static string ExtractDivContent(string filePath, string targetDivId)
        {
            return string.Empty;
            string combinedFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(combinedFilePath))
            {
                throw new FileNotFoundException("The specified .razor file was not found.", combinedFilePath);
            }

            string fileContent = File.ReadAllText(combinedFilePath);

            int startIndex = fileContent.IndexOf($"id=\"{targetDivId}\"", StringComparison.OrdinalIgnoreCase);
            startIndex = fileContent.IndexOf(">", startIndex, StringComparison.OrdinalIgnoreCase) + 1;

            if (startIndex == -1)
                return "";

            int index = startIndex;
            int openDivCount = 0;
            while (index < fileContent.Length)
            {
                int nextOpen = fileContent.IndexOf("<div", index, StringComparison.OrdinalIgnoreCase);
                int nextClose = fileContent.IndexOf("</div>", index, StringComparison.OrdinalIgnoreCase);

                if (nextClose == -1)
                    break;

                // If we find an opening tag before the next closing tag, increase the counter.
                if (nextOpen != -1 && nextOpen < nextClose)
                {
                    openDivCount++;
                    index = nextOpen + 4;
                }
                else
                {
                    openDivCount--;
                    index = nextClose + 6;

                    if (openDivCount < 0)
                        break;
                }
            }

            int length = index - startIndex - 6;
            if (length < 0 || startIndex + length > fileContent.Length)
            {
                return "";
            }
            return FormatContent(fileContent.Substring(startIndex, length));
        }

        private static string FormatContent(string content)
        {
            var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            int indentLevel = 0;

            var result = new StringBuilder();

            foreach (var line in lines)
            {
                int closeTagCount = CountString(line, "</") + CountString(line, "/>");
                int openTagCount = CountChar(line, '<') - closeTagCount;
                int tagDelta = openTagCount - closeTagCount;

                int closeBraceCount = CountChar(line, '}');
                int openBraceCount = CountChar(line, '{');
                int braceDelta = openBraceCount - closeBraceCount;

                if(tagDelta > 0 || braceDelta > 0)
                {
                    result.Append(new string(' ', indentLevel * 4));
                    indentLevel++;
                }
                else if(tagDelta < 0 || braceDelta < 0)
                {
                    indentLevel = Math.Max(indentLevel - 1, 0);
                    result.Append(new string(' ', indentLevel * 4));
                }
                else
                    result.Append(new string(' ', indentLevel * 4));
                result.AppendLine(line.Trim());
            }

            return result.ToString();
        }

        private static int CountChar(string line, char ch)
        {
            int count = 0;
            foreach (char c in line)
            {
                if (c == ch)
                    count++;
            }
            return count;
        }

        private static int CountString(string line, string find)
        {
            int count = 0;
            int pos = 0;
            while (pos < line.Length)
            {
                int index = line.IndexOf(find, pos);
                if (index == -1)
                    break;
                count++;
                pos = index + find.Length;
            }
            return count;
        }

        public static async Task CopyToClipboardExtractedDivContentAsync(string filePath, string targetDivId, IJSRuntime JS)
        {
            string result = ExtractDivContent(filePath, targetDivId);

            await JS.InvokeVoidAsync("copyTextToClipboard", result);
        }
    }

    public static class EnumerableExtensions
    {
        private static object AggregateByProperty<TItem>(this IEnumerable<TItem> source, string propertyName, Func<IEnumerable<IComparable>, object> aggregationFunction)
        {
            if (source == null || !source.Any())
                throw new ArgumentException("Source is null or empty");

            PropertyInfo propInfo = typeof(TItem).GetProperty(propertyName);
            if (propInfo == null)
                throw new ArgumentException($"Property '{propertyName}' does not exist on type {typeof(TItem).Name}");

            if (!typeof(IComparable).IsAssignableFrom(propInfo.PropertyType))
                throw new ArgumentException($"Property '{propertyName}' must implement IComparable");

            var values = source.Select(item => (IComparable)propInfo.GetValue(item)).ToList();

            return aggregationFunction(values);
        }

        public static object MinByProperty<TItem>(this IEnumerable<TItem> source, string propertyName)
        {
            return source.AggregateByProperty(propertyName, values => values.Min());
        }

        public static object MaxByProperty<TItem>(this IEnumerable<TItem> source, string propertyName)
        {
            return source.AggregateByProperty(propertyName, values => values.Max());
        }

        public static object AverageByProperty<TItem>(this IEnumerable<TItem> source, string propertyName)
        {
            return source.AggregateByProperty(propertyName, values =>
            {
                if (values.Count() == 0) return null;

                // Check if the values are numeric
                if (values.First() is int)
                    return values.Cast<int>().Average();
                if (values.First() is double)
                    return values.Cast<double>().Average();
                if (values.First() is float)
                    return values.Cast<float>().Average();
                if (values.First() is decimal)
                    return values.Cast<decimal>().Average();

                throw new ArgumentException($"Property '{propertyName}' is not a numeric type and cannot be averaged.");
            });
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime UtcToLocal(this DateTime source, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(source, timeZoneInfo);
        }

        public static DateTime LocalToUtc(this DateTime source, TimeZoneInfo timeZoneInfo)
        {
            source = DateTime.SpecifyKind(source, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(source, timeZoneInfo);
        }

        public static DateTime WithTZ(this DateTime source)
        {
            return TimeZoneInfo.ConvertTimeToUtc(source, TimeZoneInfo.Utc); //ToDo: chnage to SettingsData.TimeZoneInfo
        }
    }
}
