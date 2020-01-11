using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CascadeComboboxAsp2.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        
        public int LocalityId { get; set; }

        public Locality Locality { get; set; }
  
        public int SubLocalityId { get; set; }
        public SubLocality SubLocality { get; set; }
    }
}