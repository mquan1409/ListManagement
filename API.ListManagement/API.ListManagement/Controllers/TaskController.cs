using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using Library.ListManagement.Standard.EC;
using ListManagement.models;
using Microsoft.AspNetCore.Mvc;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<ItemDTO> Get()
        {
            return new TaskEC().Get();
        }
    }
}