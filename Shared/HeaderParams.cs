using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Shared
{
    public class HeaderParams
    {
        [JsonPropertyName("X-UserId")]
        public string? XUserId { get; set; }

        [JsonPropertyName("X-Digest")]
        public string? XDigest { get; set; }
    }
}
