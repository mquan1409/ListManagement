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
            items.AddRange(new AppointmentEC().Get());
            return items;
        }

        [HttpPost("Search")]
        public IEnumerable<ItemDTO> Search([FromBody] SearchItemDTO searchItemDTO)
        {
            var items = Get();
            var incomplete_items = items.Where(i =>

               (!searchItemDTO.ShowComplete && !searchItemDTO.ShowQuery && !(((i as TaskDTO)?.isCompleted) ?? true))

               || searchItemDTO.ShowComplete);

            var filtered_items = incomplete_items.Where(i =>
                ((searchItemDTO.ShowQuery &&
                   ((i.Name.ToUpper() == searchItemDTO.Query) ||
                   (i.Description.ToUpper() == searchItemDTO.Query) ||
                   ((i as AppointmentDTO)?.Attendees?.Select(t => t.ToUpper())?.Contains(searchItemDTO.Query) ?? false)))
                || !searchItemDTO.ShowQuery));

            return filtered_items;
        }
    }
}
