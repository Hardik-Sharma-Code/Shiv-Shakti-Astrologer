using Shiv_Shakti_Astro.Models;

namespace Shiv_Shakti_Astro.VM
{
    public class CustomerVM
    {
        public PagedResult<Customer>? getAllCustomers;

        public Customer? getCustomers;
        public bool isMaleGenderSelect { get; set; }
        public bool isFemaleGenderSelect { get; set; }
        public bool isppMaleGenderSelect { get; set; }
        public bool isppFemaleGenderSelect { get; set; }
        public bool isMalikYes { get; set; }
        public bool isMalikNo { get; set; }
        public bool isSingle { get; set; }
        public bool isDivorced { get; set; }
        public bool MartialSelect { get; set; }
        public string? SelectedAge { get; set; }
        public string? SearchName { get; set; }

        public List<DietryCheckBox>? dietryList;

        public List<DietryCheckBox>? ppDietryList;

    }
}
