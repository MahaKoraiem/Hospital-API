using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalApi.DTOs.Pharmaceutical_Comp;
namespace HospitalApi.DTOs.Drug
{
    public class DrugReadDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Trade_Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Strength { get; set; }
        [Required]
        public int Company_Id { get; set; }

        public ReadDto? Company { get; set; }
    }
}
