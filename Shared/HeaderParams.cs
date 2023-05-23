using Microsoft.AspNetCore.Mvc;

namespace Shared
{
    public class HeaderParams
    {
        [FromHeader(Name = "X-UserId")]
        public string? XUserId { get; set; }
        [FromHeader(Name = "X-digest")]
        public string? XDigest { get; set; }
    }
}
