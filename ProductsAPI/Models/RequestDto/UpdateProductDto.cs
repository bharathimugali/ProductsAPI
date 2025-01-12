namespace ProductsAPI.Models.RequestDto
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }
        public int Stock { get; set; }
    }
}
