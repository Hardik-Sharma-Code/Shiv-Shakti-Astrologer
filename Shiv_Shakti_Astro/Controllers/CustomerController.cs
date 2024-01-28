using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shiv_Shakti_Astro.Models;
using Shiv_Shakti_Astro.Services;
using Shiv_Shakti_Astro.VM;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shiv_Shakti_Astro.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerServices _customerServices;
        CustomerVM customerVM;
        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        
        public async Task<IActionResult> GetCustomer(FilterDto filter,int page = 1, int pageSize = 10)
        {
            customerVM = new CustomerVM();
            if (filter.selectedAge == null && filter.SearchName == null)
            {
                customerVM.getAllCustomers = await _customerServices.GetAll(page, pageSize);
            }
            else
            {
                customerVM.SelectedAge = filter.selectedAge;
                customerVM.SearchName = filter.SearchName;
                customerVM.getAllCustomers = await _customerServices.Filter(filter);
            }

            customerVM.dietryList = _customerServices.GetCheckBox();
            ViewBag.PaginationModel = customerVM.getAllCustomers.pagination;
            return View(customerVM);
        }

        [HttpPost]
        public async Task<IActionResult> addCustomer(Customer data)
        {
            await _customerServices.AddOrUpdate(data);
            return RedirectToAction("GetCustomer");
        }


        [HttpPost]
        public async Task<IActionResult> editCustomer(Guid Id)
        {
            customerVM = await _customerServices.GetById(Id);
            return PartialView("_editCustomer", customerVM);
        }

        public async Task<IActionResult> DeleteCustomer(Guid Id)
        {
            await _customerServices.Delete(Id);
            return RedirectToAction("GetCustomer");
        }
    }
}
