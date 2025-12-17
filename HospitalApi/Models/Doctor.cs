using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Specialty { get; set; }
        [Required]
        [Range(0,60)]
        public int Years_Of_Experience { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }
        public ICollection<DoctorPatientAppointment>? DoctorPatients { get; set; }
    }

}
