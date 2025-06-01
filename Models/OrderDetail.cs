using System;
using System.Collections.Generic;

namespace pizza_place_backend.Models;

public partial class OrderDetail
{
    public long OrderDetailsId { get; set; }

    public long OrderId { get; set; }

    public string PizzaId { get; set; } = null!;

    public short Quantity { get; set; }
}
