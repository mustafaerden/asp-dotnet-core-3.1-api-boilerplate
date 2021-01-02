using Core.CrossCuttingConcerns.Logging;

namespace Kurumsal.Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }
    }
}
