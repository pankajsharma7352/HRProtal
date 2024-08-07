using HRPortal.Data.HRPortalContext;
using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Services.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public string DeleteEmployee(int id)
        {
           return employeeRepository.DeleteEmployee(id);
        }

        public ShowEmployeeDetails editEmployee(EditEmployeeDetails editEmployeeDetials)
        {
            return employeeRepository.EditEmployee(editEmployeeDetials);
        }

        public Employee EmployeeLogin(EmployeeLogin login)
        {
            return employeeRepository.EmployeeLogin(login);
        }

        public List<ShowEmployeeDetails> GetllAllDetails(int pageNumber, int pageSize, string filter, string sortBy)
        {
            return employeeRepository.GetllAllDetails(pageNumber,pageSize,filter,sortBy);
        }

        public void RegisterEmployee(EmployeeDetailsDTO employeeDetails)
        {
            employeeRepository.RegisterEmployee(employeeDetails);
        }

        public ShowEmployeeDetails FindById(int id)
        {
            return employeeRepository.FindById(id);
        }

        public List<ShowEmployeeDetails> GetllAllDetails()
        {
            return employeeRepository.GetllAllDetails();
        }
    }
}
