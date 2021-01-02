using Newtonsoft.Json;

namespace Kurumsal.Core.Extensions
{
    // Api den donen hata mesajlarini duzenlemek icin;
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
