using System.ComponentModel.DataAnnotations;

namespace SDSExam_Penote.Models
{
    public class RecyclableType
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Type { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public decimal MinKg { get; set; }

        [Required]
        public decimal MaxKg { get; set; }
    }
}