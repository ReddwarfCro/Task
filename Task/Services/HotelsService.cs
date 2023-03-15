using AutoMapper;
using BusinessLayer;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Task.Common;

namespace Task.Services;

/// <summary>
/// Hotels service
/// </summary>

public class HotelsService : IHotelsService, IScopeDependency
{
    /// <summary>
    /// Injected context
    /// </summary>
    public static DataContext? _context;

    /// <summary>
    /// Injected mapper
    /// </summary>
    public static IMapper? _mapper;

    /// <summary>
    /// Hotels service consturctor
    /// </summary>
    /// <param name="context">Injected context service</param>
    /// <param name="mapper">Injected mapper</param>
    public HotelsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets Hotels items
    /// </summary>
    /// <param name="myLongitude">User Longitude</param>
    /// <param name="myLatitude">User Latitude</param>
    /// <returns>Service response with list of Hotels items</returns>
    public ServiceResponse<List<HotelsDTO>> GetHotels(double myLongitude, double myLatitude)
    {
        var serviceResponse = new ServiceResponse<List<HotelsDTO>>();

        try
        {

            var items = _context?.Hotels?.Include(i => i.Address).ToList();

            if (items != null && items.Count > 0)
            {
                var hotels = _mapper?.Map<List<HotelsDTO>>(items);

                foreach (var hotel in hotels)
                {
                    var distance = CalculateDistance(myLongitude, myLatitude, hotel.longitude, hotel.latitude);
                    hotel.Distance = distance;
                }

                var hotelsOrdered = hotels.OrderBy(o => o.Price).ThenBy(t => t.Distance).ToList();

                serviceResponse.Data = hotelsOrdered;
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No hotels found";
            }
        }
        catch (System.Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    /// <summary>
    /// Gets single Hotel
    /// </summary>
    /// <param name="id">Hotels Id</param>
    /// <returns>Hotels item</returns>
    public ServiceResponse<HotelsDTO> GetHotel(int id)
    {
        var serviceResponse = new ServiceResponse<HotelsDTO>();

        try
        {

            var item = _context?.Hotels?.Include(i => i.Address).FirstOrDefault(w => w.Id == id);
            if (item != null)
            {
                serviceResponse.Data = _mapper?.Map<HotelsDTO>(item) ?? new HotelsDTO();

                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found";
            }
        }
        catch (System.Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    /// <summary>
    /// Inserts new Hotels in table
    /// </summary>
    /// <param name="name">Name of the Hotel</param>
    /// <param name="price">Price of hotel</param>
    /// <param name="longitude">Hotel Longitude</param>
    /// <param name="latitude">Hotel Latitude</param>
    /// <param name="addressId">Address Id</param>
    /// <returns>Inserted HotelItem</returns>
    public ServiceResponse<HotelsDTO> InsertHotelItem(string name, double price, double longitude, double latitude, int? addressId)
    {

        var serviceResponse = new ServiceResponse<HotelsDTO>();

        try
        {
            var item = CheckItemForInsert(name, price, longitude, latitude, addressId);

            if (item == null) return serviceResponse;

            var insertedItem = _context?.Hotels?.AddAsync(item);
            _context?.SaveChangesAsync();
            _context?.Dispose();

            serviceResponse.Data = _mapper?.Map<HotelsDTO>(insertedItem?.Result.Entity) ?? new HotelsDTO();
            serviceResponse.Success = true;
        }
        catch (System.Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }


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
    public ServiceResponse<HotelsDTO> UpdateHotelItem(int id, string? name, double? price, double? longitude, double? latitude, int? addressId)
    {

        var serviceResponse = new ServiceResponse<HotelsDTO>();

        try
        {
            
            var item = _context?.Hotels?.Include(i => i.Address).FirstOrDefault(w => w.Id == id);

            item.Name = String.IsNullOrEmpty(name) ? item.Name : name;
            if (!price.HasValue)
            {
                item.Price = (double)price;
            }
            if (!longitude.HasValue)
            {
                item.longitude = (double)longitude;
            }
            if (!latitude.HasValue)
            {
                item.latitude = (double)latitude;
            }

            var updatedItem = _context?.Hotels?.Update(item);
            _context?.SaveChangesAsync();
            _context?.Dispose();

            serviceResponse.Data = _mapper?.Map<HotelsDTO>(item) ?? new HotelsDTO();
            serviceResponse.Success = true;
        }
        catch (System.Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    /// <summary>
    /// Delete Hotel in table
    /// </summary>
    /// <param name="id">Hotel Id</param>
    /// <returns>Data weather action is successful</returns>
    public ServiceResponse<bool> DeleteHotelItem(int id)
    {

        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var item = _context?.Hotels?.Include(i => i.Address).FirstOrDefault(w => w.Id == id);

            if (item != null)
            {
                _context?.Remove(item);
                _context?.SaveChangesAsync();
                _context?.Dispose();
            }
            serviceResponse.Success = true;
        }
        catch (System.Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }



    /// <summary>
    /// Checks data validity
    /// </summary>
    /// <param name="name">Name of the Hotel</param>
    /// <param name="price">Price of hotel</param>
    /// <param name="longitude">Hotel Longitude</param>
    /// <param name="latitude">Hotel Latitude</param>
    /// <param name="addressId">Address Id</param>
    /// <returns></returns>
    private Hotels? CheckItemForInsert(string name, double price, double longitude, double latitude, int? addressId)
    {
        if (string.IsNullOrEmpty(name)) return null;
        addressId = addressId ?? 1;

        var databaseAddress = _context?.Address?.FirstOrDefault(w => w.Id == addressId);
        var databaseItem = _context?.Hotels?.FirstOrDefault(w => w.Name == name);

        if (databaseAddress == null || databaseItem != null) return null;

        var item = new Hotels()
        {
            Name = name,
            Price = price,
            longitude = longitude,
            latitude = latitude,
            Address = databaseAddress
        };

        return item;
    }

    /// <summary>
    /// Calculate distance of two locations
    /// </summary>
    /// <param name="longitudeMylocation">User Longitude</param>
    /// <param name="latitudeMyLocation">User Latitude</param>
    /// <param name="longitude">Hotel longitude</param>
    /// <param name="latitude">Hotel latitude</param>
    /// <returns>Distance</returns>
    private double CalculateDistance(double longitudeMylocation, double latitudeMyLocation, double longitude, double latitude)
    {
        double latDiff = latitudeMyLocation - latitude;
        double lngDiff = longitudeMylocation - longitude;

        // Calculate the distance using the Pythagorean theorem
        double distance = Math.Sqrt(latDiff * latDiff + lngDiff * lngDiff);

        return distance;
    }
}