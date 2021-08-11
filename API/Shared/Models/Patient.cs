using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace API.Shared.Models
{
    public partial class Patient
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The first name field must be filled.")]
        [RegularExpression(@"^[A-Z\s]+$", ErrorMessage = "The first name field can only contain UPPER CASE LETTERS and SPACES.")]
        public string PfirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The last name field must be filled.")]
        [RegularExpression(@"^[A-Z\s]+$", ErrorMessage = "The last name field can only contain UPPER CASE LETTERS and SPACES.")]
        public string PlastName { get; set; }

        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "The NIN must be a positive number.")]
        public long PnationalIdentificationNumber { get; set; } = -1;

        [Required]
        public DateTime PentryDate { get; set; } =  DateTime.Now;

        [Required]
        public DateTime PbirthDate { get; set; } = new DateTime(1980, 1, 1);
        public long? PphoneNumber { get; set; }
        public string Pcity { get; set; }
        public int? Pnumber { get; set; }
        public string PpostCode { get; set; }
        public string Pstreet { get; set; }
        public string RoomId { get; set; }
        public int? WorkerId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Doctor Worker { get; set; }
    }
}
