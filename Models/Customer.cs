using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Customer
    {
        [Key]
        public int CusId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Full name:")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth:")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Address:")]
        public string Address { get; set; }

        [Display(Name = "Picture:")]
        public string? CustomerPicture { get; set; }      
        public virtual Account Account { get; set; }
    }
}
