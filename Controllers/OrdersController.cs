using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza_place_backend.Data;
using pizza_place_backend.Models;

namespace pizza_place_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaPlaceContext _context;

        public OrdersController(PizzaPlaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData(
        string? searchKey = null,
        int page = 1,
        int pageSize = 10,
        string orderBy = "orderdetailsid",
        string order = "asc")
            {
                var query = from od in _context.OrderDetails
                            join o in _context.Orders on od.OrderId equals o.OrderId
                            join p in _context.Pizzas on od.PizzaId equals p.PizzaId into pizzaJoin
                            from p in pizzaJoin.DefaultIfEmpty()
                            join pt in _context.PizzaTypes on p.PizzaTypeId equals pt.PizzaTypeId into ptJoin
                            from pt in ptJoin.DefaultIfEmpty()
                            select new OrderDetailDto
                            {
                                OrderDetailsId = od.OrderDetailsId,
                                OrderId = o.OrderId,
                                Date = o.Date,
                                Time = o.Time,
                                PizzaId = p == null ? "" : p.PizzaId,
                                Size = p == null ? "" : p.Size,
                                Price = p == null ? 0 : p.Price,
                                PizzaTypeId = pt == null ? "" : pt.PizzaTypeId,
                                Name = pt == null ? "" : pt.Name,
                                Category = pt == null ? "" : pt.Category,
                                Ingredients = pt == null ? "" : pt.Ingredients,
                                Quantity = od.Quantity
                            };

                if (!string.IsNullOrWhiteSpace(searchKey))
                {
                    var lowered = searchKey.ToLower();

                    query = query.Where(x =>
                        x.OrderDetailsId.ToString().Contains(lowered) ||
                        (x.Name ?? "").ToLower().Contains(lowered) ||
                        (x.Ingredients ?? "").ToLower().Contains(lowered) ||
                        (x.Category ?? "").ToLower().Contains(lowered)
                    );
                }
                var orderByLower = orderBy.ToLower();
                var orderDir = order.ToLower();

                query = (orderBy.ToLower(), order.ToLower()) switch
                {
                    ("orderdetailsid", "desc") => query.OrderByDescending(x => x.OrderDetailsId),
                    ("orderdetailsid", _) => query.OrderBy(x => x.OrderDetailsId),

                    ("name", "desc") => query.OrderByDescending(x => x.Name),
                    ("name", _) => query.OrderBy(x => x.Name),

                    ("category", "desc") => query.OrderByDescending(x => x.Category),
                    ("category", _) => query.OrderBy(x => x.Category),

                    ("quantity", "desc") => query.OrderByDescending(x => x.Quantity),
                    ("quantity", _) => query.OrderBy(x => x.Quantity),

                    ("price", "desc") => query.OrderByDescending(x => x.Price),
                    ("price", _) => query.OrderBy(x => x.Price),

                    ("date", "desc") => query.OrderByDescending(x => x.Date),
                    ("date", _) => query.OrderBy(x => x.Date),

                    ("size", "desc") => query.OrderByDescending(x => x.Size),
                    ("size", _) => query.OrderBy(x => x.Size),

                    _ => query.OrderBy(x => x.OrderDetailsId),
                };

                var totalCount = await query.CountAsync();

                    var pagedData = await query
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    return Ok(new
                    {
                        totalCount,
                        page,
                        pageSize,
                        data = pagedData
                    });
                }
    }
}
