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
    [Table("Products", Schema = "Operation")]
    public class Product : EntityAudit<int>
    {
        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, ErrorMessage = "Maximum Length 100 Char")]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
