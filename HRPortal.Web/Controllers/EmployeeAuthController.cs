using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAuthController : ControllerBase
    {

        private readonly IEmployeeServices employeeServices;

        public EmployeeAuthController(IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;
        }

        [HttpPost("register")]
        public string EmployeeRegister(EmployeeDetailsDTO employeeDetails)
        {
            employeeServices.RegisterEmployee(employeeDetails);
            return "Employee Register successfully..";
        }

        [HttpPost("login")]
        public Employee Login(EmployeeLogin login)
        {
            return employeeServices.EmployeeLogin(login);
        }



    }
}
