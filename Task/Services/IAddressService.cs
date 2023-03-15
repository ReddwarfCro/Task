using BusinessLayer.Models;

namespace Task.Services;

/// <summary>
/// Hotels service
/// </summary>
public interface IAddressService
{

    /// <summary>
    /// Gets Addresses
    /// </summary>
    /// <returns>Addresses item</returns>
    ServiceResponse<List<AddressDTO>> GetAddress();


    /// <summary>
    /// Gets Address
    /// </summary>
    /// <param name="id">Address Id</param>
    /// <returns>Address item</returns>
    ServiceResponse<AddressDTO> GetAddress(int id);

    /// <summary>
    /// Inserts new Address in table
    /// </summary>
    /// <param name="street">Street</param>
    /// <param name="number">Number</param>
    /// <param name="zipCode">Zipcode</param>
    /// <param name="city">City</param>
    /// <returns>Inserted Address</returns>
    ServiceResponse<AddressDTO> InsertAddressItem(string street, string number, string zipCode, string city);

    /// <summary>
    /// Update addresss in table
    /// </summary>
    /// <param name="id">address Id</param>
    /// <param name="street">Street</param>
    /// <param name="number">Number</param>
    /// <param name="zipCode">Zipcode</param>
    /// <param name="city">City</param>
    /// <returns>Updated address item</returns>
    ServiceResponse<AddressDTO> UpdateAddresItem(int id, string? street, string? number, string? zipCode, string? city);

    /// <summary>
    /// Delete address in table
    /// </summary>
    /// <param name="id">address Id</param>
    /// <returns>Data weather action is successful</returns>
    ServiceResponse<bool> DeleteAddressItem(int id);
}