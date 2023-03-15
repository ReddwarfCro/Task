using BusinessLayer.Models.Authorization;

namespace Task.Services;

/// <summary>
/// Hotels service
/// </summary>
public interface IUserService
{
    /// <summary>
    /// User login
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns>User Token</returns>
    ServiceResponse<TokenModel> Login(string username, string password);

}