using AutoMapper;
using HRPortal.Data.HRPortalContext;
using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HRPortal.Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext _hrDbContext;
        private readonly IMapper mapper;

        public EmployeeRepository(HRDbContext hrDbContext, IMapper mapper)
        {
            _hrDbContext = hrDbContext;
            this.mapper = mapper;
        }

        public void RegisterEmployee(EmployeeDetailsDTO employeeDetails)
        {

            //Employee employee = new Employee();

            //employee.FirstName = employeeDetails.FirstName;
            //employee.LastName = employeeDetails.LastName;
            //employee.Email = employeeDetails.Email;
            //employee.Phone = employeeDetails.Phone;
            //employee.Gender = employeeDetails.Gender;
            //employee.Password= employeeDetails.Password;
            //employee.AddressId= employeeDetails.AddressId;

            var employee = mapper.Map<Employee>(employeeDetails);
            var address = mapper.Map<Address>(employeeDetails);

            //Address address = new Address();
            //address.AddressId= employeeDetails.AddressId;
            //address.City = employeeDetails.City;  
            //address.State = employeeDetails.State;
            //address.PinCode = employeeDetails.PinCode;
            //address.AddressLine1 = employeeDetails.AddressLine1;

            _hrDbContext.Addresses.Add(address);
            _hrDbContext.Employees.Add(employee);
            _hrDbContext.SaveChanges();

        }

        public Employee EmployeeLogin(EmployeeLogin login)
        {
            int empid;
            Int32.TryParse(login.Email_Or_Id, out empid);
            var employee = _hrDbContext.Employees.FirstOrDefault(e => e.Email == login.Email_Or_Id || e.Id==empid && e.Password ==login.Password);
            
            return employee;
        }

        public List<ShowEmployeeDetails> GetllAllDetails(int pageNumber,int pageSize, string filter, string sortBy)
        {
          List<Employee> employeeList =  _hrDbContext.Employees.ToList();
          List<Address> addressList = _hrDbContext.Addresses.ToList();
          
            var employeeDetails = from e in employeeList
                                  join a in addressList on e.AddressId equals a.AddressId
                                  select new ShowEmployeeDetails
                                  {
                                      FirstName = e.FirstName, 
                                      LastName = e.LastName,
                                      Email= e.Email,
                                      Password=e.Password,
                                      Phone=e.Phone,
                                      Gender=e.Gender,
                                      AddressLine1= a.AddressLine1,
                                      City=a.City,
                                      State=a.State,
                                      PinCode=a.PinCode,   
                                  };

            // Paginations are added
            var pagedEmployeeDetails = employeeDetails
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            // searching and sorting
            if (!string.IsNullOrWhiteSpace(filter))
            {
                pagedEmployeeDetails = pagedEmployeeDetails.Where(p => p.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase)
                                               || p.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase)
                                               || p.Gender.Contains(filter, StringComparison.OrdinalIgnoreCase)
                                               || p.City.Contains(filter, StringComparison.OrdinalIgnoreCase)

                                               ).ToList();
            }

            switch (sortBy)
            {
                case "name":
                    pagedEmployeeDetails = pagedEmployeeDetails.OrderBy(p => p.FirstName).ToList();
                    break;
                case "gender":
                    pagedEmployeeDetails = pagedEmployeeDetails.OrderBy(p => p.Gender).ToList();
                    break;
                case "city":
                    pagedEmployeeDetails = pagedEmployeeDetails.OrderBy(p => p.City).ToList();
                    break;
                // Add more sorting options as needed
                default:
                    break;
            }

            return pagedEmployeeDetails;

        }

        public ShowEmployeeDetails EditEmployee(EditEmployeeDetails employeeDetails)
        {
            Employee employee = _hrDbContext.Employees.Where(s =>s.Id== employeeDetails.Id).FirstOrDefault();
            Address address = _hrDbContext.Addresses.Where(s => s.AddressId == employee.AddressId).FirstOrDefault();

            employee.FirstName = employeeDetails.FirstName;
            employee.LastName = employeeDetails.LastName;
            employee.Email = employeeDetails.Email;
            employee.Password = employeeDetails.Password;
            employee.Phone = employeeDetails.Phone;
            employee.Gender = employeeDetails.Gender;
            
            address.AddressLine1 = employeeDetails.AddressLine1;
            address.City=employeeDetails.City;
            address.State=employeeDetails.State;
            address.PinCode=employeeDetails.PinCode;

            _hrDbContext.SaveChanges();
            ShowEmployeeDetails showDetails =
                new ShowEmployeeDetails(
                    employee.FirstName,
                    employee.LastName, 
                    employee.Email, 
                    employee.Password, 
                    employee.Phone, 
                    employee.Gender, 
                    address.AddressLine1, 
                    address.City, 
                    address.State, 
                    address.PinCode);

            return showDetails; 
        }

        public string DeleteEmployee(int id)
        {
            Employee employee = _hrDbContext.Employees.Where(s => s.Id ==id).FirstOrDefault();
            Address address = _hrDbContext.Addresses.Where(s => s.AddressId == employee.AddressId).FirstOrDefault();

            if (employee != null && address != null)
            {
                _hrDbContext.Employees.Remove(employee);
                _hrDbContext.Addresses.Remove(address);
                _hrDbContext.SaveChanges();
            }
            return "Record deleted successfully..";


        }

        public ShowEmployeeDetails FindById(int id)
        {
            Employee employee = _hrDbContext.Employees.Where(s => s.Id ==id).FirstOrDefault();
            Address address = _hrDbContext.Addresses.Where(s => s.AddressId == employee.AddressId).FirstOrDefault();

            ShowEmployeeDetails showDetails =
                    new ShowEmployeeDetails(
                        employee.FirstName,
                        employee.LastName,
                        employee.Email,
                        employee.Password,
                        employee.Phone,
                        employee.Gender,
                        address.AddressLine1,
                        address.City,
                        address.State,
                        address.PinCode);

            return showDetails; ;

        }

        public List<ShowEmployeeDetails> GetllAllDetails()
        {
            List<Employee> employeeList = _hrDbContext.Employees.ToList();
            List<Address> addressList = _hrDbContext.Addresses.ToList();

            var employeeDetails = from e in employeeList
                                  join a in addressList on e.AddressId equals a.AddressId
                                  select new ShowEmployeeDetails
                                  {
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Email = e.Email,
                                      Password = e.Password,
                                      Phone = e.Phone,
                                      Gender = e.Gender,
                                      AddressLine1 = a.AddressLine1,
                                      City = a.City,
                                      State = a.State,
                                      PinCode = a.PinCode,
                                  };
            return employeeDetails.ToList();
        }
    }
}
