namespace ProductCatalogue.Web
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Colours { get; set; } = string.Empty; // This is a list of colours as a comma-separated string
    }
}
