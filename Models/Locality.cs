using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CascadeComboboxAsp2.Models
{
    public class Locality
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        
        public virtual ICollection<SubLocality> SubLocalities { get; set; }
    }
}
