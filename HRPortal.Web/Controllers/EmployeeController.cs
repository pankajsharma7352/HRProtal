using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices employeeServices;
      

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;
       
        }

        [HttpGet("getAllDetails")]
      public List<ShowEmployeeDetails> getAllDetails([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search="", [FromQuery] string sortBy = "")
        {
            
            return employeeServices.GetllAllDetails(pageNumber,pageSize,search,sortBy);
        }

        [HttpPut("EditEmployee")]
        public ShowEmployeeDetails EditEmployee(EditEmployeeDetails employeeDetails)
        {
            return employeeServices.editEmployee(employeeDetails);
        }

        [HttpDelete("{id}")]
        public string DeleteEmpById(int id)
        {
            return employeeServices.DeleteEmployee(id);
        }


        [HttpGet("SearchById{id}")]
        public ShowEmployeeDetails Search(int id)
        {
            return employeeServices.FindById(id);
        }
    }
}
