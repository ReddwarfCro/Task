using AutoMapper;
using BusinessLayer;
using BusinessLayer.Models.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Task.Common;
using ConfigurationManager = Task.Common.ConfigurationManager;

namespace Task.Services;

/// <summary>
/// User service
/// </summary>

public class UserService : IUserService, IScopeDependency
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
    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// User login
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns>User Token</returns>

    public ServiceResponse<TokenModel> Login(string username, string password)
    {
        var serviceResponse = new ServiceResponse<TokenModel>();

        try
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));

            var user = _context?.Users.FirstOrDefault(f => f.UserName == username);


            if (user != null)
            {
                if (!sha1data.SequenceEqual(user.PasswordHash))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Wrong Username or password";
                    return serviceResponse;
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                serviceResponse.Data = new TokenModel
                {
                    Token = tokenString
                };
            }
            else {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wrong Username or password";
            }


        }
        catch (System.Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}