using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace API.Shared.Models
{
    [Table("WORKER")]
    public partial class Worker
    {
        [Required]
        [Key]
        [Column("WorkerId")]
        [Range(0, 999, ErrorMessage = "The ID's are in the range of 0 and 999.")]
        public int WorkerId { get; set; } = -1;

        public string Wsex { get; set; }
        public long WphoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The first name field must be filled.")]
        [RegularExpression(@"^[A-Z\s]+$", ErrorMessage = "The first name field can only contain UPPER CASE LETTERS and SPACES.")] 
        public string WfirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The last name field must be filled.")]
        [RegularExpression(@"^[A-Z\s]+$", ErrorMessage = "The last name field can only contain UPPER CASE LETTERS and SPACES.")] 
        public string WlastName { get; set; }
        public string Wcity { get; set; }
        public int? Wnumber { get; set; }
        public string WpostCode { get; set; }
        public string Wstreet { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Nurse Nurse { get; set; }
    }
}
