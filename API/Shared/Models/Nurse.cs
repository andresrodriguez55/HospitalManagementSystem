using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace API.Shared.Models
{
    public partial class Nurse
    {
        [Required]
        [Range(0, 999, ErrorMessage = "The ID's are in the range of 0 and 999.")]
        public int WorkerId { get; set; } = -1;

        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "The room ID field can only contain UPPER CASE LETTERS and NUMBERS.")]
        [StringLength(4, ErrorMessage = "The room ID field value cannot exceed 4 characters. ")]
        public string RoomId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
