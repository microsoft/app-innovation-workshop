using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ContosoFieldService.Models;
using Xamarin.Forms;

namespace ContosoFieldService.Helpers
{
    public static class Extensions
    {
        public static string EmailToGravatarUrl(string email)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            var hash = sBuilder.ToString();

            return $"https://www.gravatar.com/avatar/{hash}?s=512";
        }

        public static FormattedString SearchResultFormattedString(this Job job)
        {
            var formattedString = new FormattedString();
            //Lets check to see if the name contains any square brackets. 
            var regexPattern = @"\[(\w*)\]";
            var patternMatches = Regex.Matches(job.Name, regexPattern);

            //If the name doesn't contain a hit-highlights then we just return it without any extra styling. 
            if (patternMatches.Count == 0)
            {
                formattedString = new FormattedString { Spans = { new Span { Text = job.Name } } };
            }
            else
            {
                //We split the name input parts based on our RegEx. 
                var splitList = Regex.Split(job.Name, regexPattern);

                //Build a list of hit-highlighted words without brackets.
                var highlightedWords = new List<string>();
                foreach (Match match in patternMatches)
                {
                    highlightedWords.Add(RemoveBrackets(match.Value));
                }

                //Loop through each subString and add a span, checking that the contents isn't a highlighted item. 
                foreach (var subString in splitList)
                {
                    if (highlightedWords.Contains(subString) == true)
                        formattedString.Spans.Add(new Span { Text = subString, FontAttributes = FontAttributes.Bold });
                    else
                        formattedString.Spans.Add(new Span { Text = subString });
                }
            }
            return formattedString;
        }

        static string RemoveBrackets(string input)
        {
            var removedPreTag = input.Replace("[", "");
            var removedPostTag = removedPreTag.Replace("]", "");
            return removedPostTag;
        }
    }

}
