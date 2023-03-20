using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Models.Domain;
using MVCProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MVCProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCProjectDataContext mvcProjectDataContext;

        public EmployeeController(MVCProjectDataContext mvcProjectDataContext)
        {
            this.mvcProjectDataContext = mvcProjectDataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcProjectDataContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployee)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployee.Name,
                Email = addEmployee.Email,
                Salary = addEmployee.Salary,
                Department = addEmployee.Department,
                DateOfBirth = addEmployee.DateOfBirth
            };

            await mvcProjectDataContext.Employees.AddAsync(employee);
            await mvcProjectDataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewEmployee(Guid id)
        {
            var viewEmployee = mvcProjectDataContext.Employees.FirstOrDefault(employee => employee.Id == id);

            if (viewEmployee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = viewEmployee.Id,
                    Name = viewEmployee.Name,
                    Email = viewEmployee.Email,
                    Salary = viewEmployee.Salary,
                    Department = viewEmployee.Department,
                    DateOfBirth = viewEmployee.DateOfBirth
                };
                return View(viewModel);

            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult ViewEmployee(UpdateEmployeeViewModel model)
        {
            var employee = mvcProjectDataContext.Employees.Find(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                mvcProjectDataContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteEmployee(UpdateEmployeeViewModel model)
        {
            var employee = mvcProjectDataContext.Employees.Find(model.Id);
            if (employee != null)
            {
                mvcProjectDataContext.Remove(employee);
                mvcProjectDataContext.SaveChanges();
            }
            return RedirectToAction("Index");

        }

    }
}
