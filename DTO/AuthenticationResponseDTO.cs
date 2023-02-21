namespace wms_api.DTO
{
    public class AuthenticationResponseDTO
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
