using System.ComponentModel.DataAnnotations;


namespace BookShop.Models
    {
        public class CustomerViewModel
        {
            public int CusId { get; set; }
            [Required]
            [StringLength(50)]
            public string Name { get; set; }
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }
            [Required]
            [StringLength(10)]
            public string Gender { get; set; }
            public string Email { get; set; }
            [Required]
            [StringLength(100)]
            public string Address { get; set; }
            public IFormFile? UploadPicture { get; set; }
            public string? CustomerPicture { get; set; }
            public virtual Account Account { get; set; }
    }
  }


