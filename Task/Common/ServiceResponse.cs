public class ServiceResponse<T>
{
    /// <summary>
    /// Response data
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// Bolean representing where the action is succesfull
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// Error messages
    /// </summary>
    public string Message { get; set; } = null;
}