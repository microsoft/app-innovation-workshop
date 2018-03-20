namespace ContosoFieldService.Models
{
    public class ThirdPartyLibrary
    {
        public ThirdPartyLibrary(string name, string author, string url)
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
