using BusinessLayer.Models;

namespace Task.Services;

/// <summary>
/// Hotels service
/// </summary>
public interface IHotelsService
{
    /// <summary>
    /// Gets Hotels items
    /// </summary>
    /// <param name="myLongitude">User Longitude</param>
    /// <param name="myLatitude">User Latitude</param>
    /// <returns>Service response with list of Hotels items</returns>
    ServiceResponse<List<HotelsDTO>> GetHotels(double myLongitude, double myLatitude);

    /// <summary>
    /// Gets single Hotel
    /// </summary>
    /// <param name="id">Hotels Id</param>
    /// <returns>Hotels item</returns>
    ServiceResponse<HotelsDTO> GetHotel(int id);


    /// <summary>
    /// Inserts new Hotels in table
    /// </summary>
    /// <param name="name">Name of the Hotel</param>
    /// <param name="price">Price of hotel</param>
    /// <param name="longitude">Hotel Longitude</param>
    /// <param name="latitude">Hotel Latitude</param>
    /// <param name="addressId">Address Id</param>
    /// <returns>Inserted HotelItem</returns>
    ServiceResponse<HotelsDTO> InsertHotelItem(string name, double price, double longitude, double latitude, int? category);

    /// <summary>
    /// Update Hotel in table
    /// </summary>
    /// <param name="id">Hotel Id</param>
    /// <param name="name">Name of the Hotel</param>
    /// <param name="price">Price of hotel</param>
    /// <param name="longitude">Hotel Longitude</param>
    /// <param name="latitude">Hotel Latitude</param>
    /// <param name="addressId">Address Id</param>
    /// <returns>Updated HotelItem</returns>
    ServiceResponse<HotelsDTO> UpdateHotelItem(int id, string? name, double? price, double? longitude, double? latitude, int? addressId);

    /// <summary>
    /// Delete Hotel in table
    /// </summary>
    /// <param name="id">Hotel Id</param>
    /// <returns>Data weather action is successful</returns>
    public ServiceResponse<bool> DeleteHotelItem(int id);
}