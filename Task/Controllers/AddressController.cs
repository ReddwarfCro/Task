using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Services;

namespace Task.Controllers;

/// <summary>
/// Address Controller
/// </summary>
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiController, Authorize]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AddressController : ControllerBase
{
    /// <summary>
    /// Address service
    /// </summary>
    private IAddressService _addressService { get; set; }

    /// <summary>
    /// Logger Service
    /// </summary>
    private readonly ILogger<AddressController> _logger;

    /// <summary>
    /// Address controller constructor
    /// </summary>
    /// <param name="logger">Injected logger</param>
    /// <param name="addressService">Injected addressService</param>
    public AddressController(
        ILogger<AddressController> logger,
        IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }

    /// <summary>
    /// Get Address
    /// </summary>
    /// <returns>List of Address</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Get()
    {
        var addresses = _addressService.GetAddress();
        if (addresses.Success)
        {
            return Ok(addresses);
        }
        return BadRequest(addresses.Message);
    }

    /// <summary>
    /// Get Address
    /// </summary>
    /// <param name="id">id of Address</param>
    /// <returns>Address item</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     Get /GetAddress:{Id}
    ///
    /// </remarks>
    /// <response code="200">Returns if everything is ok</response>
    /// <response code="400">If the item is null</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AddressDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public IActionResult GetAddress(int id)
    {
        var address = _addressService.GetAddress(id);
        return Ok(address);

    }

    /// <summary>
    ///  Get Address for version 2 of api
    /// </summary>
    /// <param name="id">id of Address</param>
    /// <returns>Address</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     Get /Address:{Id}
    ///
    /// </remarks>
    /// <response code="200">Returns if everything is ok</response>
    /// <response code="404">If the item is null</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AddressDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult GetAddressV2(int id)
    {
        var address = _addressService.GetAddress(id);

        if (address.Success)
        {
            return Ok(address);
        }
        return BadRequest(address.Message);
    }

    /// <summary>
    /// Inserts new Address
    /// </summary>
    /// <param name="street">Street</param>
    /// <param name="number">Number</param>
    /// <param name="zipCode">Zipcode</param>
    /// <param name="city">City</param>
    /// <returns>Inserted Address</returns>
    [HttpPost()]
    [ProducesResponseType(typeof(AddressDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult InsertAddress(string street, string number, string zipCode, string city)
    {
        var insertedItem = _addressService.InsertAddressItem(street, number, zipCode, city);

        return Ok(insertedItem.Data);
    }

    /// <summary>
    /// Update Address
    /// </summary>
    /// <param name="id">Id of Address</param>
    /// <param name="street">Street</param>
    /// <param name="number">Number</param>
    /// <param name="zipCode">Zipcode</param>
    /// <param name="city">City</param>
    /// <returns>Inserted Address</returns>
    [HttpPut()]
    [ProducesResponseType(typeof(AddressDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult UpdateAddress(int id, string? street, string? number, string? zipCode, string? city)
    {
        var updatedItem = _addressService.UpdateAddresItem(id, street, number, zipCode, city);

        return Ok(updatedItem.Data);
    }

    /// <summary>
    /// Delete Address
    /// </summary>
    /// <param name="id">Id of Address</param>
    /// <returns>Inserted Address</returns>
    [HttpDelete()]
    [ProducesResponseType(typeof(AddressDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult DeleteAddress(int id)
    {
        var deletedItem = _addressService.DeleteAddressItem(id);

        return Ok(deletedItem.Data);
    }
}
