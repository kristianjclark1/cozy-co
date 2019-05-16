using System;
using System.ComponentModel.DataAnnotations;

namespace CozyCo.Domain.Models
{
    public class Property
    {
        public int Id { get; set; } //For DB purposes to make it be more identifiable

        [Required(ErrorMessage = "Don't forget to add the location of the home")]
        public string Address { get; set; }

        [Display(Name ="Address cont.")]
        public string Address2 { get; set; }

        [Required] //Requirement for the database
        public string City { get; set; }

        [Required]
        public string Zipcode { get; set; }

        public string Image { get; set; }

        //Fully Defined Relationship for property type
        //Combining name of model with property Id
        [Display(Name = "Property Type")]
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        
        // Fully Defined Relationship for App User
        public string AppUserId { get; set; }
        public AppUser Landlord { get; set; }
        
    }
}
