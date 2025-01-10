using Microsoft.Build.Framework;

namespace AdoptAPet.Services;

public class RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}