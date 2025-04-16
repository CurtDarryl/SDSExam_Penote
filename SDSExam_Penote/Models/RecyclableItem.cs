using System.ComponentModel.DataAnnotations;

namespace SDSExam_Penote.Models
{
    public class RecyclableItem
    {
        public int Id { get; set; }

        [Required]
        public int RecyclableTypeId { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Weight { get; set; }

        [Display(Name = "Computed Rate")]
        public decimal ComputedRate { get; set; }

        [Required, StringLength(150)]
        public string ItemDescription { get; set; }

        public RecyclableType RecyclableType { get; set; }
    }
}
