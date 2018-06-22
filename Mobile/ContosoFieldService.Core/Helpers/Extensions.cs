using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using ContosoFieldService.Models;
using Xamarin.Forms;

namespace ContosoFieldService.Helpers
{
    public static class Extensions
    {
        public static string EmailToGravatarUrl(string email)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email ?? string.Empty));
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
            return NewConvertNameToFormattedString(job);
        }

        private static FormattedString NewConvertNameToFormattedString(Job job)
        {
            FormattedString formattedString = new FormattedString();

            //The job name has a hit!
            if(job.Name.Contains("[") && job.Name.Contains("]"))
            {
                //Copy job name 
                var name = job.Name;
                Console.WriteLine($"Job Name: {name}");

                //Get the stand and end index position in string of the area to bold. 
                var highlightPosition = PositionOfHighlight(name);

                //Length of the text to highlight minus the brakets 
                var lengthOfPreHighlight = highlightPosition.Item1;
                var lengthOfHighlight = highlightPosition.Item2 - highlightPosition.Item1 +1;
                var lengthOfPostHighlight = name.Length - (lengthOfPreHighlight + lengthOfHighlight);

                //Remove the brackets from name 
                name = name.Remove(highlightPosition.Item1, 1);
                name = name.Remove(highlightPosition.Item2 + 1, 1);

                Console.WriteLine($"Removed brackets and left with : {name}");

                if (highlightPosition.Item1 == 0)
                {
                    //The match is at the start of the string so we can assume we'll only have 2 substrings. One to bold and the other to remain normal
                    var matchedString = name.Substring(highlightPosition.Item1, lengthOfHighlight);
                    var postMatchString = name.Substring(highlightPosition.Item2 + 1);

                    formattedString.Spans.Add(new Span { Text = matchedString, FontAttributes = FontAttributes.Bold });
                    formattedString.Spans.Add(new Span { Text = postMatchString });
                }
                else if(highlightPosition.Item2 == name.Length - 1)
                {
                    //The match extends to the last character so we'll only need to split into two again

                    //The match is at the start of the string so we can assume we'll only have 2 substrings. One to bold and the other to remain normal
                    var preMatchString = name.Substring(0, highlightPosition.Item1);
                    var matchedString = name.Substring(highlightPosition.Item1, lengthOfHighlight);

                    formattedString.Spans.Add(new Span { Text = preMatchString });
                    formattedString.Spans.Add(new Span { Text = matchedString, FontAttributes = FontAttributes.Bold });
                }
                else
                {
                    // The String will need to be split into three sections as it'll have the pre-highlight, highlight and post-highlight text
                    var preMatchString = name.Substring(0, highlightPosition.Item1);
                    var matchedString = name.Substring(highlightPosition.Item1, lengthOfHighlight);
                    var postMatchString = name.Substring(highlightPosition.Item2 + 1);

                    formattedString.Spans.Add(new Span { Text = preMatchString });
                    formattedString.Spans.Add(new Span { Text = matchedString, FontAttributes = FontAttributes.Bold });
                    formattedString.Spans.Add(new Span { Text = postMatchString });
                }

                //Lets just check that we've created something...
                if(formattedString.Spans.Count == 0)
                    formattedString = new FormattedString { Spans = { new Span { Text = name } } }; 

            }
            else
            {
                formattedString = new FormattedString { Spans = { new Span { Text = job.Name } } }; 
            }

            return formattedString;
        }

        private static Tuple<int, int> PositionOfHighlight(string name)
        {
            /* We want to get the index positions for creating bold whilst taking into account that we'll be
             * removing the brackets which will effect the end position */

            //Copy the contents of the string to a char array for finding the positions for splitting the string
            var charArray = name.ToCharArray();

            //Position of [ matched highlight 
            var startOfHighlight = 0;
            //Position of ] matched hightlight 
            var endofHighlight = 0;

            //Loop through the chars looking for hits.
            for (int i = 0; i < charArray.Length; i++)
            {
                var character = charArray[i];
                if (character == char.Parse("["))
                    startOfHighlight = i;

                if (character == char.Parse("]"))
                    endofHighlight = i - 2;
            }
            return Tuple.Create(startOfHighlight, endofHighlight);
        }

        private static FormattedString LegacyConvertNameToFormattedString(Job job)
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
                    Console.WriteLine($"match : {match}");
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
            var removedWhiteSpaceTag = removedPostTag.Remove(0);
            return removedPostTag;
        }
    }

}
