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
      public List<ShowEmployeeDetails> getAllDetails()
        {
            return employeeServices.GetllAllDetails();
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
    }
}
