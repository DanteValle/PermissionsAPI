namespace PermissionsAPI.Infrastructure
{
    public interface ILog
    {
        void Auth(string message, object? obj = null);
        void Start(object objt);
        void Info(string message, object? obj = null);
        void Error(object? obj = null, string? message = "");
        void Exeption(string message, object? obj = null);
        void WriteLog(object? objCTC = null, string? method = "", string? message = null);
    }
}