namespace BusinessLayer.Models
{
    /// <summary>
    /// Address DTO
    /// </summary>
    public class AddressDTO
    {
        /// <summary>
        /// Id of address
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// ZipCode
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string  City { get; set; }

    }
}
