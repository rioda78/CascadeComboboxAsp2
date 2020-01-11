using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CascadeComboboxAsp2.Models
{
    public class CustomerFormVM
    {
        [Required(ErrorMessage = "Please enter a First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select a city")]
        [Display(Name = "City")]
        public int? SelectedCity { get; set; }
        [Required(ErrorMessage = "Please select a locality")]

        [Display(Name = "Locality")]
        public int? SelectedLocality { get; set; }
        [Required(ErrorMessage = "Please select a sub locality")]
        [Display(Name = "Sub Locality")]
        public int? SelectedSubLocality { get; set; }

        public SelectList CityList { get; set; }
        public SelectList LocalityList { get; set; }
        public SelectList SubLocalityList { get; set; }
    }
}
