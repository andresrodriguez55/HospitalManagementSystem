using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace API.Shared.Models
{
    [Table("DOCTOR")]
    public partial class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }

        [Required]
        [Key]
        [Column("WorkerId")]
        [Range(0, 999, ErrorMessage = "The ID's are in the range of 0 and 999.")]
        public int WorkerId { get; set; } = -1;

        [StringLength(50, ErrorMessage = "The specialization field value cannot exceed 50 characters. ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The specialization field must be filled.")]
        public string Dspecialization { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
