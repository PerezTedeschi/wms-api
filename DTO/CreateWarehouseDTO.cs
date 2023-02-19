namespace wms_api.DTO
{
    public class CreateWarehouseDTO
    {       
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required string Zip { get; set; }
        public required float Latitude { get; set; }
        public required float Longitude { get; set; }
        public IFormFile? File { get; set; }
    }
}
