namespace pizza_place_backend.Models
{
    public class OrderDetailDto
    {
        public long OrderDetailsId { get; set; } // Matches OrderDetailsId (long)

        // Order info
        public long OrderId { get; set; }        // Matches OrderId (long)
        public DateOnly Date { get; set; }       // Matches Order.Date (DateOnly)
        public TimeOnly Time { get; set; }       // Matches Order.Time (TimeOnly)

        // Pizza info
        public string PizzaId { get; set; } = null!;
        public string Size { get; set; } = null!;
        public decimal Price { get; set; }

        // Pizza Type info
        public string PizzaTypeId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Ingredients { get; set; } = null!;

        // OrderDetail specific
        public short Quantity { get; set; }       // Matches Quantity (short)
    }
}
