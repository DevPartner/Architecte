namespace CartService.Infrastructure.Configs;

public class JwtBearerOptions
{
    public string? Authority { get; set; }
    public bool ValidAudience { get; set; }
}
