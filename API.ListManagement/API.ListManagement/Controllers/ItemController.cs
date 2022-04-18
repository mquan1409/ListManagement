using Library.ListManagement.Standard.DTO;
using API.ListManagement.EC;
using Microsoft.AspNetCore.Mvc;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly ILogger<TaskController> _logger;

        public ItemController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<ItemDTO> Get()
        {
            var items = new List<ItemDTO>();
            items.AddRange(new TaskEC().Get().ToList());
            items.AddRange(new AppointmentEC().Get().ToList());
            return items;
        }
    }
}
