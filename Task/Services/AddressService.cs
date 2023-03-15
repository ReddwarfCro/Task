using AutoMapper;
using BusinessLayer;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Task.Common;

namespace Task.Services;

/// <summary>
/// Address service
/// </summary>

public class AddressService : IAddressService, IScopeDependency
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
    /// Address service consturctor
    /// </summary>
    /// <param name="context">Injected context service</param>
    /// <param name="mapper">Injected mapper</param>
    public AddressService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets Addresses
    /// </summary>
    /// <returns>Addresses items</returns>
    public ServiceResponse<List<AddressDTO>> GetAddress()
    {
        var serviceResponse = new ServiceResponse<List<AddressDTO>>();

        try
        {

            var items = _context?.Address?.ToList();
            if (items?.Count > 0)
            {
                serviceResponse.Data = _mapper?.Map<List<AddressDTO>>(items);
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
    /// Gets Address
    /// </summary>
    /// <param name="id">Address Id</param>
    /// <returns>Address item</returns>
    public ServiceResponse<AddressDTO> GetAddress(int id)
    {
        var serviceResponse = new ServiceResponse<AddressDTO>();

        try
        {

            var item = _context?.Address?.FirstOrDefault(w => w.Id == id);
            if (item != null)
            {
                serviceResponse.Data = _mapper?.Map<AddressDTO>(item) ?? new AddressDTO();
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
    /// Inserts new Address in table
    /// </summary>
    /// <param name="street">Street</param>
    /// <param name="number">Number</param>
    /// <param name="zipCode">Zipcode</param>
    /// <param name="city">City</param>
    /// <returns>Inserted Address</returns>
    public ServiceResponse<AddressDTO> InsertAddressItem(string street, string number, string zipCode, string city)
    {

        var serviceResponse = new ServiceResponse<AddressDTO>();

        try
        {
            var item = new Address()
            {
                City = city,
                Street = street,
                Number = number,
                ZipCode = zipCode
            };

            var insertedItem = _context?.Address?.AddAsync(item);
            _context?.SaveChangesAsync();
            _context?.Dispose();

            serviceResponse.Data = _mapper?.Map<AddressDTO>(insertedItem?.Result.Entity) ?? new AddressDTO();
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
    /// Update addresss in table
    /// </summary>
    /// <param name="id">address Id</param>
    /// <param name="street">Street</param>
    /// <param name="number">Number</param>
    /// <param name="zipCode">Zipcode</param>
    /// <param name="city">City</param>
    /// <returns>Updated address item</returns>
    public ServiceResponse<AddressDTO> UpdateAddresItem(int id, string street, string number, string zipCode, string city)
    {

        var serviceResponse = new ServiceResponse<AddressDTO>();

        try
        {
            var item = _context?.Address?.FirstOrDefault(w => w.Id == id);

            if (item != null)
            {

                item.Street = String.IsNullOrEmpty(street) ? item.Street : street;
                item.Number = String.IsNullOrEmpty(number) ? item.Number : number;
                item.ZipCode = String.IsNullOrEmpty(zipCode) ? item.ZipCode : zipCode;
                item.City = String.IsNullOrEmpty(city) ? item.City : city;

                var updatedItem = _context?.Address?.Update(item);
                _context?.SaveChangesAsync();
                _context?.Dispose();

                serviceResponse.Data = _mapper?.Map<AddressDTO>(item) ?? new AddressDTO();
                serviceResponse.Success = true;
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
    /// Delete address in table
    /// </summary>
    /// <param name="id">address Id</param>
    /// <returns>Data weather action is successful</returns>
    public ServiceResponse<bool> DeleteAddressItem(int id)
    {

        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var item = _context?.Address?.FirstOrDefault(w => w.Id == id);

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
}