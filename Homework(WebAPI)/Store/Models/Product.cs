namespace NetStore.Models
{
    public class Product
    {
        public int ProdId { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = null!;
        public int GroupID { get; set; }
        public virtual Group Group { get; set; } = new();
        public int Price { get; set; }
        public virtual List<Warehouse> Stores { get; set; } = null!;
    }
}
