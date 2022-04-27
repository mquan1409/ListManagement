using Library.ListManagement.Standard.DTO;
using API.ListManagement.EC;
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

        [HttpPost("AddOrUpdate")]
        public AppointmentDTO AddOrUpdate([FromBody] AppointmentDTO appointment)
        {
            return new AppointmentEC().AddOrUpdate(appointment);
        }
    }
}
