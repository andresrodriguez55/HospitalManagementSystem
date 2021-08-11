using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace API.Shared.Models
{
    public partial class Room
    {
        public Room()
        {
            Nurses = new HashSet<Nurse>();
            Patients = new HashSet<Patient>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The room ID field must be filled.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "The room ID field can only contain UPPER CASE LETTERS and NUMBERS.")]
        [StringLength(4, ErrorMessage = "The room ID field value cannot exceed 4 characters. ")]
        public string RoomId { get; set; } = "";

        [Range(1, 60, ErrorMessage = "The room capacity field must be between 1 and 60.")]
        public int Rcapacity { get; set; }

        [StringLength(50, ErrorMessage = "The room type field value cannot exceed 50 characters. ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The room type field must be filled.")]
        public string Rtype { get; set; }

        public virtual ICollection<Nurse> Nurses { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
