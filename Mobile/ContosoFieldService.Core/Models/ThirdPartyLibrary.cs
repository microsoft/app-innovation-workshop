namespace ContosoFieldService.Models
{
    public class ThirdPartyLibrary
    {
        public ThirdPartyLibrary(string name, string url, string author)
        {
            Name = name;
            Url = url;
            Author = author;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
    }
}
