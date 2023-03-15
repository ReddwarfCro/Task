using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Authorization;


/// <summary>
/// Login Model
/// </summary>
public class LoginModel
{
    /// <summary>
    /// UserName
    /// </summary>
    /// <value>UserName</value>
    public string? UserName
    {
        get;
        set;
    }
    /// <summary>
    /// Password
    /// </summary>
    /// <value>Password</value>
    public string? Password
    {
        get;
        set;
    }
}
