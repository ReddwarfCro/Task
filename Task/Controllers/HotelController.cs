using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Services;

namespace Task.Controllers;

/// <summary>
/// Hotel Controller
/// </summary>
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiController, Authorize]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class HotelController : ControllerBase
{
    /// <summary>
    /// Hotel service
    /// </summary>
    private IHotelsService _hotelsService { get; set; }

    /// <summary>
    /// Logger Service
    /// </summary>
    private readonly ILogger<HotelController> _logger;

    /// <summary>
    /// Hotel controller constructor
    /// </summary>
    /// <param name="logger">Injected logger</param>
    /// <param name="hotelsService">Injected hotelsService</param>
    public HotelController(
        ILogger<HotelController> logger,
        IHotelsService hotelsService)
    {
        _logger = logger;
        _hotelsService = hotelsService;
    }

    /// <summary>
    /// Get Hotels
    /// </summary>
    /// <returns>List of hotels</returns>
    /// <param name="longitude">longitude</param>
    /// <param name="latitude">latitude</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Get(double longitude, double latitude)
    {
        var hotels = _hotelsService.GetHotels(longitude, latitude);
        if (hotels.Success)
        {
            return Ok(hotels);
        }
        return BadRequest(hotels.Message);
    }

    /// <summary>
    /// Get Hotel
    /// </summary>
    /// <param name="id">id of hotel</param>
    /// <returns>Hotel item</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     Get /GetHotel:{Id}
    ///
    /// </remarks>
    /// <response code="200">Returns if everything is ok</response>
    /// <response code="400">If the item is null</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HotelsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public IActionResult GetHotel(int id)
    {
        var hotel = _hotelsService.GetHotel(id);
        return Ok(hotel);

    }

    /// <summary>
    ///  Get Hotel for version 2 of api
    /// </summary>
    /// <param name="id">id of hotel</param>
    /// <returns>Hotel</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     Get /GetHotel:{Id}
    ///
    /// </remarks>
    /// <response code="200">Returns if everything is ok</response>
    /// <response code="404">If the item is null</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HotelsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult GetHotelV2(int id)
    {
        var hotel = _hotelsService.GetHotel(id);

        if (hotel.Success)
        {
            return Ok(hotel);
        }
        return BadRequest(hotel.Message);
    }

    /// <summary>
    /// Inserts new hotel
    /// </summary>
    /// <param name="name">Name of the hotel</param>
    /// <param name="price">Hotel price</param>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitude</param>
    /// <param name="addressId">Id of address</param>
    /// <returns>Inserted Hotel</returns>
    [HttpPost()]
    [ProducesResponseType(typeof(HotelsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult InsertHotel(string name, double price, double longitude, double latitude, int? addressId)
    {
        var insertedItem = _hotelsService.InsertHotelItem(name, price, longitude, latitude, addressId);

        return Ok(insertedItem.Data);
    }

    /// <summary>
    /// Update hotel
    /// </summary>
    /// <param name="name">Name of the hotel</param>
    /// <param name="price">Hotel price</param>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitude</param>
    /// <param name="addressId">Id of address</param>
    /// <param name="id">Id of hotel</param>
    /// <returns>Inserted Hotel</returns>
    [HttpPut()]
    [ProducesResponseType(typeof(HotelsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult UpdateHotel(string? name, double? price, double? longitude, double? latitude, int? addressId, int id)
    {
        var updatedItem = _hotelsService.UpdateHotelItem(id, name, price, longitude, latitude, addressId);

        return Ok(updatedItem.Data);
    }

    /// <summary>
    /// Delete hotel
    /// </summary>
    /// <param name="id">Id of hotel</param>
    /// <returns>Inserted Hotel</returns>
    [HttpDelete()]
    [ProducesResponseType(typeof(HotelsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public IActionResult DeleteHotel(int id)
    {
        var deletedItem = _hotelsService.DeleteHotelItem(id);

        return Ok(deletedItem.Data);
    }
}
