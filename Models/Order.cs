using System;
using System.Collections.Generic;

namespace pizza_place_backend.Models;

public partial class Order
{
    public long OrderId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }
}
