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

        public static FormattedString ConvertNameToFormattedString(this Job job)
        {
            //We want to return a FormattedString which is a Xamarin.Forms Type that allows us to style text elements
             var formattedString = new FormattedString(); 

            //We'll use regex to nd content between square brackets [contents]
            var regexPattern = @"\[(\w*)\]";

            //Lets create a MatchCollection which will contain any matches from our job.name. 
            var patternMatches = Regex.Matches(job.Name, regexPattern);
            System.Diagnostics.Debug.WriteLine($"Text: {job.Name}");


            //If the name doesn't contain a matches then we just a default FormattedString with the name set. No extra work is required.  
            if (patternMatches.Count == 0)
            {
                formattedString = new FormattedString { Spans = { new Span { Text = job.Name } } };
            }
            else
            {
                //We create a list of matched wordsready for use in building the FormattedStrings property.
                var highlightedWords = new List<string>();

                //We loop through the matches and copy the values to a list of strings for easier use. 
                foreach (Match match in patternMatches)
                {
                    highlightedWords.Add(RemoveBrackets(match.Value));
                }

                //We split the name input parts based on our RegEx. "Hello [World]" would become an array of string containing two items, "Hello" and "World";
                var splitList = Regex.Split(job.Name, regexPattern);

                //We then loop through each subString and add a span, checking that the contents isn't contained in the highlightedWords property. 
                foreach (var subString in splitList)
                {
                    if (highlightedWords.Contains(subString) == true)
                        //We have found that the text is a highlighted word and thus needs to be bold. 
                        formattedString.Spans.Add(new Span { Text = subString, FontAttributes = FontAttributes.Bold });
                    else
                        //The text isn't a highlighted item so we 
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
