namespace ProductsAPI.Models.RequestDto
{
    public class AddProductDto
    {
        public required string Name { get; set; }
        public int Stock { get; set; }
    }
}
