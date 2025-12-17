using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1")]
        public int Total_Quantity_Per_Prescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [ForeignKey("Patient_Ur_Namber")]
        public int Patient_Ur_Number { get; set; }
        [Required]
        [ForeignKey("Doctor_Id")]
        public int Doctor_Id { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<PrescriptionDrug>? PrescriptionDrugs { get; set; }
    }
}
