using System;
using System.Collections.Generic;

namespace pizza_place_backend.Models;

public partial class PizzaType
{
    public string PizzaTypeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Ingredients { get; set; } = null!;
}
