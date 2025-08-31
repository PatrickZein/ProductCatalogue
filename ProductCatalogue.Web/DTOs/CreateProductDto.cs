namespace ProductCatalogue.Web.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Colours { get; set; } = string.Empty;
    }
}