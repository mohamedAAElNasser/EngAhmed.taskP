using EngAhmed.TaskP.Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngAhmed.TaskP.Domain.Enitities.Operations
{
    [Table("Customers",Schema ="Operation")]
    public class Customer : EntityAudit<int>
    {
        [Required(ErrorMessage = "Customer Name is Required")]
        [StringLength(50, ErrorMessage = "Maximum Length 50 Char")]
        public string Name { get; set; } = string.Empty;

    }
}
