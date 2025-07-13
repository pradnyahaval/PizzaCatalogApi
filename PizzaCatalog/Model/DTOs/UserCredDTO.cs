using System.ComponentModel.DataAnnotations;

namespace PizzaCatalog.WebApi.Model.DTOs
{
    public class UserCredDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public String[] roles { get; set; }
    }
}
