namespace wms_api.DTO
{
    public class CreateWarehouseDTO
    {       
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
        public required float Latitude { get; set; }
        public required float Longitude { get; set; }
        public required IFormFile File { get; set; }
    }
}
