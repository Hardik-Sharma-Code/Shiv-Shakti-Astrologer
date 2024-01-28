using System.ComponentModel.DataAnnotations.Schema;

namespace Shiv_Shakti_Astro.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Place { get; set; }
        public string? Gender { get; set; }
        public string? Complexion { get; set; }
        public string? Height { get; set; }
        public string? Built { get; set; }
        public string? CasteGotra { get; set; }
        public string? RelegionCommunitity { get; set; }
        public string? Nationality { get; set; }
        public string? MaritalStatus { get; set; }
        public string? DietryHabbits { get; set; }
        public string? Qualification { get; set; }
        public string? OccupationIncome { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Siblings { get; set; }
        public int? AddressType { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Hobbies { get; set; }
        public string? OtherDetailes { get; set; }
        public string? PPManglik { get; set; }
        public string? PPGender { get; set; }
        public string? PPComplexion { get; set; }
        public string? PPHeight { get; set; }
        public string? PPBuilt { get; set; }
        public string? PPCasteGotra { get; set; }
        public string? PPReligionCommunity { get; set; }
        public string? PPDietryHabbit { get; set; }
        public string? PPQualification { get; set; }
        public string? PPOccupationIncome { get; set; }
        public string? PPCountry { get; set; }
        public string? PPState { get; set; }
        public string? Profilepic { get; set; }

        [NotMapped]
        public IFormFile? CustomerPhoto { get; set; }

    }
}
