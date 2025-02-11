using System.Text.Json;
using System.Text;

namespace PermissionsAPI.Infrastructure
{
    public class Log : ILog
    {
        public Log() { }

        public void Auth(string message, object? obj = null)
        {
            WriteLog(obj, "AUTH", message);
        }
        public void Start(object objt)
        {
            WriteLog(objt, "START");
        }
        public void Info(string message, object? obj = null)
        {
            WriteLog(obj, "INFO", message);
        }
        public void Exeption(string message = "", object? obj = null)
        {
            WriteLog(obj, "EXCEPTION", message);
        }
        public void Error(object? obj = null, string? message = "")
        {
            WriteLog(obj, "ERROR", message: message);
        }
        public void WriteLog(object? objCTC = null, string? method = "", string? message = null)
        {
            try
            {
                var now = DateTime.Now;
                StringBuilder strRequest = new StringBuilder();
                //strRequest.Append(method switch
                //{
                //    "START" => " - [NEW POST]        : ",
                //    "INFO" => " - INFO        : ",
                //    "ERROR" => " - ERROR       : ",
                //    "EXCEPTION" => " - EXCEPTION   : ",
                //    _ => $" - POST WebMethod {method} : Parameters --> "
                //});
                switch (method)
                {
                    case "AUTH":
                        strRequest.Append($"\n|-- [{now:yyyy-MM-dd HH.mm.ss.fff}] [NEW POST FAILED AUTH] : ");

                        break;
                    case "START":
                        strRequest.Append($"\n|-- [{now:yyyy-MM-dd HH.mm.ss.fff}] [NEW POST] : ");

                        break;

                    case "WARNING":
                        strRequest.Append($"|   |-- [{now:yyyy-MM-dd HH.mm.ss.fff}] [WARNING] : ");
                        break;

                    case "ERROR":
                        strRequest.Append($"|   |-- [{now:yyyy-MM-dd HH.mm.ss.fff}] [ERROR] : ");
                        break;

                    case "EXCEPTION":
                        strRequest.Append($"|   |-- [{now:yyyy-MM-dd HH.mm.ss.fff}] [EXCEPTION] : ");
                        break;
                    default:
                        strRequest.Append($"|   |-- [{now:yyyy-MM-dd HH.mm.ss.fff}] [INFO]: ");
                        break;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    strRequest.Append(message);
                }
                if (objCTC != null)
                {
                    if (objCTC is Exception exception)
                    {
                        strRequest.Append($" - {exception.Message}");
                    }
                    else
                    {
                        strRequest.Append(JsonSerializer.Serialize(objCTC));
                        // string json = JsonConvert.SerializeObject(objeto, Formatting.Indented); Install-Package Newtonsoft.Json
                    }
                }
                //var logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"{DateTime.Now:yyyyMMdd}_SabadelBlink.log");
                var logFolder = Path.Combine(AppContext.BaseDirectory, "Log");
                var logFileName = Path.Combine(logFolder, $"{DateTime.Now:yyyy-MM-dd}_dailyLog.log");

                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                var fileInfo = new FileInfo(logFileName);
                var append = fileInfo.Exists && fileInfo.Length <= 25000000;


                var fullline = strRequest;

                using (StreamWriter writer = new StreamWriter(logFileName, append))
                {
                    writer.WriteLine(fullline);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
