using System.ComponentModel.DataAnnotations;

namespace CargoProject.Web.Services.Users
{
    public class User
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="İsmi Boş Geçemessiniz")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Şifre dolu olmalı")]
        public string Password { get; set; }

        public string Email { get; set; }

        public string GoogleId { get; set; }
    }
}