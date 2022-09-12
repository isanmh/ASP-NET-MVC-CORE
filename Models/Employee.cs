
using System.ComponentModel.DataAnnotations;
namespace MvcApp.Models
{
    public class Employee
    {
        // attribut yang dibutuhkan
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama harus diisi")]
        // [StringLength(100, MinimumLength = 10)]
        public string? Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string? Division { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}