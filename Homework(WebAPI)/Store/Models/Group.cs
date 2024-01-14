namespace NetStore.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string? Name { get; set; }
        public virtual List<Product> Products { get; set; } = null!;
    }
}
