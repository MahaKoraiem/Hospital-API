using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi.DTOs.Drug
{
    public class DrugCreateDto
    {
       
        [Required]
        [StringLength(100)]
        public string Trade_Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Strength { get; set; }
        [Required]
        [ForeignKey("Comapny_Id")]
        public int Company_Id { get; set; }
    }
}
