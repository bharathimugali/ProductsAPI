using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Stock { get; set; }
    }
}
