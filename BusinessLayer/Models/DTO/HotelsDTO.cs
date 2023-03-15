namespace BusinessLayer.Models
{
    /// <summary>
    /// Hotels DTO
    /// </summary>
    public class HotelsDTO
    {
        /// <summary>
        /// Id of Hotel
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of hotel
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Geoloation longitude
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// Geolocation latitude
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// Distance from User
        /// </summary>
        public double Distance { get; set; }    

        /// <summary>
        /// Address
        /// </summary>
        public AddressDTO Address { get; set; } = new AddressDTO();


    }
}
