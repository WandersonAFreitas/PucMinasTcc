using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebAPI.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        public static string PreparesTag(this string text, string tagOpen = "<", string tagClose = ">")
        {
            var tagFormatted = text.RemoveDiacritics().ToLower();
            tagFormatted = Regex.Replace(tagFormatted, "[^a-zA-Z0-9]", " ").ToLower().Trim();
            tagFormatted = Regex.Replace(tagFormatted, @"\s+", " ").Replace(' ', '_');
            return $"{tagOpen}{tagFormatted}{tagClose}";
        }

        public static string RemoveNonAlphanumeric(this string text)
        {
            text = Regex.Replace(text, "[^a-zA-Z0-9]", "");
            return text;
        }

        public static string UrlImageToBase64String(this string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var rawBytes = response.RawBytes;
            string base64String = Convert.ToBase64String(rawBytes);
            return base64String;
        }
    }
}
