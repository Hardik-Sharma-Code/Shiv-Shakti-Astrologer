using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shiv_Shakti_Astro.Data;
using Shiv_Shakti_Astro.Models;
using Shiv_Shakti_Astro.VM;
using System.Linq;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shiv_Shakti_Astro.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ShivShaktiDbContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomerServices(ShivShaktiDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task AddOrUpdate(Customer data)
        {
            try
            {
                string fetchPhoto = string.Empty;
                var existCustomer = await db.Customers.FirstOrDefaultAsync(x => x.Id == data.Id);

                if (data.CustomerPhoto != null && !string.IsNullOrEmpty(data.CustomerPhoto!.FileName))
                {
                    data.Profilepic = UploadPhoto(data.CustomerPhoto);
                }
                if (existCustomer is null)
                {

                    var newCustomer = new Customer
                    {
                        Id = new Guid(),
                        Name = data.Name,
                        Address1 = data.Address1,
                        Address2 = data.Address2,
                        AddressType = data.AddressType,
                        BirthDate = data.BirthDate,
                        Built = data.Built,
                        CasteGotra = data.CasteGotra,
                        Complexion = data.Complexion,
                        ContactNumber = data.ContactNumber,
                        DietryHabbits = data.DietryHabbits,
                        Email = data.Email,
                        FatherName = data.FatherName,
                        Gender = data.Gender,
                        Height = data.Height,
                        Hobbies = data.Hobbies,
                        MaritalStatus = data.MaritalStatus,
                        MotherName = data.MotherName,
                        Nationality = data.Nationality,
                        OccupationIncome = data.OccupationIncome,
                        OtherDetailes = data.OtherDetailes,
                        Place = data.Place,
                        PPBuilt = data.PPBuilt,
                        PPCasteGotra = data.PPCasteGotra,
                        PPComplexion = data.PPComplexion,
                        PPCountry = data.PPCountry,
                        PPDietryHabbit = data.PPDietryHabbit,
                        PPGender = data.PPGender,
                        PPHeight = data.PPHeight,
                        PPManglik = data.PPManglik,
                        PPOccupationIncome = data.PPOccupationIncome,
                        PPQualification = data.PPQualification,
                        PPReligionCommunity = data.PPReligionCommunity,
                        PPState = data.PPState,
                        Profilepic = data.Profilepic == null?string.Empty:data.Profilepic,
                        Qualification = data.Qualification,
                        RelegionCommunitity = data.RelegionCommunitity,
                        Siblings = data.Siblings
                    };
                    db.Customers.Add(newCustomer);
                }
                else
                {

                    existCustomer.Name = data.Name;
                    existCustomer.Address1 = data.Address1;
                    existCustomer.Address2 = data.Address2;
                    existCustomer.AddressType = data.AddressType;
                    existCustomer.BirthDate = data.BirthDate;
                    existCustomer.Built = data.Built;
                    existCustomer.CasteGotra = data.CasteGotra;
                    existCustomer.Complexion = data.Complexion;
                    existCustomer.ContactNumber = data.ContactNumber;
                    existCustomer.DietryHabbits = data.DietryHabbits;
                    existCustomer.Email = data.Email;
                    existCustomer.FatherName = data.FatherName;
                    existCustomer.Gender = data.Gender;
                    existCustomer.Height = data.Height;
                    existCustomer.Hobbies = data.Hobbies;
                    existCustomer.MaritalStatus = data.MaritalStatus;
                    existCustomer.MotherName = data.MotherName;
                    existCustomer.Nationality = data.Nationality;
                    existCustomer.OccupationIncome = data.OccupationIncome;
                    existCustomer.OtherDetailes = data.OtherDetailes;
                    existCustomer.Place = data.Place;
                    existCustomer.PPBuilt = data.PPBuilt;
                    existCustomer.PPCasteGotra = data.PPCasteGotra;
                    existCustomer.PPComplexion = data.PPComplexion;
                    existCustomer.PPCountry = data.PPCountry;
                    existCustomer.PPDietryHabbit = data.PPDietryHabbit;
                    existCustomer.PPGender = data.PPGender;
                    existCustomer.PPHeight = data.PPHeight;
                    existCustomer.PPManglik = data.PPManglik;
                    existCustomer.PPOccupationIncome = data.PPOccupationIncome;
                    existCustomer.PPQualification = data.PPQualification;
                    existCustomer.PPReligionCommunity = data.PPReligionCommunity;
                    existCustomer.PPState = data.PPState;
                    existCustomer.Profilepic = data.Profilepic;
                    existCustomer.Qualification = data.Qualification;
                    existCustomer.RelegionCommunitity = data.RelegionCommunitity;
                    existCustomer.Siblings = data.Siblings;
                    existCustomer.Profilepic = data.Profilepic == null ? string.Empty : data.Profilepic;
                    db.Customers.Update(existCustomer);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public async Task<PagedResult<Customer>> GetAll(int page = 1, int pageSize = 10)
        {
            // Calculate the number of records to skip based on the page and page size
            var skipAmount = (page - 1) * pageSize;

            // Get the total number of records in the table
            var totalRecords = await db.Customers.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            // Get a paginated subset of records
            var customers = await db.Customers
                .OrderBy(c => c.Id) // You can change the ordering based on your requirement
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();

            // Create a PagedResult object to return with the paginated data
            var result = new PagedResult<Customer>
            {
                Data = customers,
                pagination = new PaginationModel
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,


                }
            };

            return result;

        }

        public async Task<CustomerVM> GetById(Guid id)
        {
            try
            {
                var customerVM = new CustomerVM();
                var customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    if (!string.IsNullOrEmpty(customer.Profilepic))
                    {
                        customer.Profilepic = fetchProfilePhoto(customer.Profilepic);
                    }
                    List<DietryCheckBox> dietryList = GetCheckBox();
                    List<DietryCheckBox> ppDietryList = GetCheckBox();

                    if (customer.DietryHabbits != null)
                    {
                        foreach (var item in dietryList)
                        {
                            if (customer.DietryHabbits.Contains(item.Value!))
                            {
                                item.IsChecked = true;
                            }
                        }
                    }
                    if (customer.PPDietryHabbit != null)
                    {
                        foreach (var item in ppDietryList)
                        {
                            if (customer.PPDietryHabbit.Contains(item.Value!))
                            {
                                item.IsChecked = true;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(customer.Gender) && customer.Gender.ToLower() == "male")
                    {
                        customerVM.isMaleGenderSelect = true;
                    }
                    if (!string.IsNullOrEmpty(customer.Gender) && customer.Gender.ToLower() == "female")
                    {
                        customerVM.isFemaleGenderSelect = true;
                    }
                    if (!string.IsNullOrEmpty(customer.PPGender) && customer.PPGender.ToLower() == "male")
                    {
                        customerVM.isppMaleGenderSelect = true;
                    }
                    if (!string.IsNullOrEmpty(customer.PPGender) && customer.PPGender.ToLower() == "female")
                    {
                        customerVM.isppFemaleGenderSelect = true;
                    }
                    if (!string.IsNullOrEmpty(customer.MaritalStatus) && customer.MaritalStatus!.ToLower() == "yes")
                    {
                        customerVM.isMalikYes = true;
                    }
                    if (!string.IsNullOrEmpty(customer.MaritalStatus) && customer.MaritalStatus!.ToLower() == "no")
                    {
                        customerVM.isMalikNo = true;
                    }
                    if (!string.IsNullOrEmpty(customer.MaritalStatus) && customer.MaritalStatus!.ToLower() == "single")
                    {
                        customerVM.isSingle = true;
                    }
                    if (!string.IsNullOrEmpty(customer.MaritalStatus) && customer.MaritalStatus!.ToLower() == "divorced")
                    {
                        customerVM.isDivorced = true;
                    }
                    if (!string.IsNullOrEmpty(customer.PPManglik) && customer.PPManglik!.ToLower() == "no")
                    {
                        customerVM.isMalikNo = true;
                    }
                    if (!string.IsNullOrEmpty(customer.PPManglik) && customer.PPManglik!.ToLower() == "yes")
                    {
                        customerVM.isMalikYes = true;
                    }
                    customerVM.getCustomers = customer;
                    customerVM.dietryList = dietryList;
                    customerVM.ppDietryList = ppDietryList;
                }

                return customerVM!;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<DietryCheckBox> GetCheckBox()
        {
            List<DietryCheckBox> dietryList = new List<DietryCheckBox>()
            {
              new DietryCheckBox {Value="Veg",Text="Veg",IsChecked=false },
              new DietryCheckBox {Value="Non-Veg",Text="Non-Veg",IsChecked=false },
              new DietryCheckBox {Value="Eggetarian",Text="Eggetarian",IsChecked=false },
              new DietryCheckBox {Value="Vegan",Text="Vegan" ,IsChecked=false},
            };

            return dietryList.ToList();
        }

        public async Task<PagedResult<Customer>> Filter(FilterDto filter, int page = 1, int pageSize = 10)
        {
            var getCustomer = await GetAll();

            DateTime currentDate = DateTime.Now;
            List<Customer> filteredCustomers = new List<Customer>();

            if (getCustomer != null && getCustomer.Data != null)
            {
                if (filter.selectedAge == "18-24")
                {
                    filteredCustomers = getCustomer.Data
                        .Where(p => p.BirthDate > currentDate.AddYears(-24) && p.BirthDate <= currentDate.AddYears(-18) && (filter.SearchName == null || p.Name.Contains(filter.SearchName)))
                        .ToList();
                }
                else if (filter.selectedAge == "25-40")
                {
                    filteredCustomers = getCustomer.Data
                        .Where(p => p.BirthDate > currentDate.AddYears(-40) && p.BirthDate <= currentDate.AddYears(-25) && (filter.SearchName == null || p.Name.Contains(filter.SearchName)))
                        .ToList();
                }
                else if (filter.selectedAge == "40")
                {
                    filteredCustomers = getCustomer.Data
                        .Where(p => p.BirthDate <= currentDate.AddYears(-40) && (filter.SearchName == null || p.Name.Contains(filter.SearchName)))
                        .ToList();
                }
                else
                {
                    filteredCustomers = getCustomer.Data.Where(x => x.Name!.Contains(filter.SearchName)).ToList();
                }

                // Apply pagination to the filtered results
                var paginatedCustomers = filteredCustomers
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Get the total number of records in the table
                var totalRecords = filteredCustomers.Count();
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                return new PagedResult<Customer>
                {
                    Data = paginatedCustomers,
                    pagination = new PaginationModel
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalRecords = totalRecords,
                        TotalPages = totalPages,
                    }
                };
            }

            // If no records or an error occurs, return an empty PagedResult
            return new PagedResult<Customer>
            {
                Data = new List<Customer>(),

                pagination = new PaginationModel
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalRecords = 0,
                    TotalPages = 0,
                }
            };
        }

        public async Task Delete(Guid id)
        {
            var deleteRecord = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteRecord != null)
            {
                db.Customers.Remove(deleteRecord);
            }
            db.SaveChanges();
        }


        #region PrivateMethod
        private string UploadPhoto(IFormFile Profilepic)
        {
            string fileName = Path.GetFileName(Profilepic.FileName);

            string filePath = Path.Combine("wwwroot/CustomerImages", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Profilepic.CopyTo(fileStream);
            }

            return fileName;
        }

        private string fetchProfilePhoto(string profilepic)
        {
            string fileName = Path.GetFileName(profilepic);
            // Map the virtual path to the physical path within wwwroot
            string filePath = Path.Combine("~/CustomerImages/", fileName);

            return filePath;
        }
        
        #endregion
    }
}
