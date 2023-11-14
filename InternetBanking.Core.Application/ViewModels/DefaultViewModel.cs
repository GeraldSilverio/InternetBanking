using System.Text.Json.Serialization;

namespace InternetBanking.Core.Application.ViewModels
{
    public class DefaultViewModel
    {
        [JsonIgnore]
        public DateTime CurrentDate { get; set; } = DateTime.Now;
    }
}
