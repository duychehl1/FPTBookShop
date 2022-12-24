using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; }
        [StringLength(50)]
        [Required]
        public string Author { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime PublicDate { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public string? PictureName { get; set; }
        public string? PicturePath { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? Picture { get; set; }
        [Required]
        public double Price { get; set; }
        public virtual Category? Category { get; set; }
    }
}
