using System.ComponentModel.DataAnnotations;

namespace Studio.ViewModels.AuthVm
{
    public class LoginVm
    {
        public string UsernameOrEmail { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isRemember { get; set; }
    }
}
