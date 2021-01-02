using Core.CrossCuttingConcerns.Logging.Log4Net;

namespace Kurumsal.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger : LoggerServiceBase
    {
        public DatabaseLogger() : base("DatabaseLogger")
        {
        }
    }
}
