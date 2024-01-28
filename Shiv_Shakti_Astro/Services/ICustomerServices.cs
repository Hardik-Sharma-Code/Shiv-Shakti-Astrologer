using Shiv_Shakti_Astro.Models;
using Shiv_Shakti_Astro.VM;

namespace Shiv_Shakti_Astro.Services
{
    public interface ICustomerServices
    {
        Task<PagedResult<Customer>> GetAll(int page = 1, int pageSize = 10);
        Task<CustomerVM> GetById(Guid id);
        Task Delete(Guid id);
        Task AddOrUpdate(Customer model);
        List<DietryCheckBox> GetCheckBox();
        Task<PagedResult<Customer>> Filter(FilterDto filter, int page = 1, int pageSize = 10);
    }
}
