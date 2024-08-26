using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Services;
using HRPortal.Web.EmailService;
using MailKit;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAuthController : ControllerBase
    {
        private readonly ISendMailService mailService;
        private readonly IEmployeeServices employeeServices;

        public EmployeeAuthController(IEmployeeServices employeeServices, ISendMailService mailService)
        {
            this.employeeServices = employeeServices;
            this.mailService = mailService;
        }

        [HttpPost("register")]
        public string EmployeeRegister(EmployeeDetailsDTO employeeDetails)
        {
            string email = employeeDetails.Email;
            string body = $" Hii {employeeDetails.FirstName} welcome to our HR Portal,Your Password email is {employeeDetails.Email} and Password is {employeeDetails.Password} Now you can enjoy our services Keep your email and password safely..";
            MailRequest req = new MailRequest(email, $"Congratulation {employeeDetails.FirstName} Your have been registured succssfully..", body);


            mailService.SendEmailAsync(req);

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
