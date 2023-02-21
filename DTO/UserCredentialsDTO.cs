using System.ComponentModel.DataAnnotations;

namespace wms_api.DTO
{
    public class UserCredentialsDTO
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
