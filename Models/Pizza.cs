using System;
using System.Collections.Generic;

namespace pizza_place_backend.Models;

public partial class Pizza
{
    public string PizzaId { get; set; } = null!;

    public string PizzaTypeId { get; set; } = null!;

    public string Size { get; set; } = null!;

    public decimal Price { get; set; }
}
