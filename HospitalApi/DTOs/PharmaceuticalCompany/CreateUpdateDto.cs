using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs.Pharmaceutical_Comp
{
    public class CreateUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
