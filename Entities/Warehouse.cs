namespace wms_api.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }       
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required string Zip { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public required string FileName { get; set; }
        public required string FileMimeType { get; set; }
        public required byte[] FileContent { get; set; }
    }
}
