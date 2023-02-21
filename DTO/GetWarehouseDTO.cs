namespace wms_api.DTO
{
    public class GetWarehouseDTO
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
    }
}
