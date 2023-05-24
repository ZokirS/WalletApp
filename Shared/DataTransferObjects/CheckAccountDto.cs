using Newtonsoft.Json;

namespace Shared.DataTransferObjects
{
    public record CheckAccountDto
    {
        [JsonProperty("Account-Number")]
        public string AccountNumber { get; set; }
    }
}
