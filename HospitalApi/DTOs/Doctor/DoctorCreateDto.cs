using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs.Doctor
{
    public class DoctorCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; }

        [Required]
        [Range(0, 60)]
        public int Years_Of_Experience { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
