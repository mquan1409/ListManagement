using Library.ListManagement.Standard.DTO;
using Library.ListManagement.Standard.EC;
using Microsoft.AspNetCore.Mvc;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        public AppointmentController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ItemDTO> Get()
        {
            return new AppointmentEC().Get();
        }
    }
}
