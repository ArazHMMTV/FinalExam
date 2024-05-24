using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace FinalExamAmoeba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployee();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                await _employeeService.AddEmployee(employee);
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);

            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existEmployee = _employeeService.GetEmployee(x => x.Id == id);
            if (existEmployee == null)
                return NotFound();

            return View(existEmployee);
        }

        [HttpPost]

        public IActionResult DeletePost(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
            }
            catch (EntityNotFound ex)
            {
                return NotFound();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var oldEmployee = _employeeService.GetEmployee(x => x.Id == id);
            if (oldEmployee == null)
                return NotFound();
            return View(oldEmployee);
        }
        [HttpPost]

        public IActionResult Update(int id,Employee newEmployee)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                _employeeService.UpdateEmployee(newEmployee.Id, newEmployee);
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);

            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}
