using System.ComponentModel.DataAnnotations;

namespace apisHotel.Models.Api
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Contrasena { get; set; }
    }
}
