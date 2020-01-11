using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CascadeComboboxAsp2.Models
{
    public class SubLocality
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public int LocalityId { get; set; }
        public Locality Locality { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
