namespace NetStore.Models
{
    public class Warehouse
    {

        public int WhId { get; set; }
        public string? Name { get; set; }
        public virtual List<Product> Products { get; set; } = null!;
        public int Count { get; set; }
    }
}
