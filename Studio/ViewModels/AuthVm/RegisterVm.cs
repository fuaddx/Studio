using System.ComponentModel.DataAnnotations;

namespace Studio.ViewModels.AuthVm
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "İstifadəçi adınızı daxil edin!!!"), MaxLength(24)]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(ConfirmPassword)), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{4,}$", ErrorMessage = "Wrong input for password")]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{4,}$", ErrorMessage = "Wrong input for password")]
        public string ConfirmPassword { get; set; }
    }
}
