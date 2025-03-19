namespace CarRentalService.Contracts.Configurations
{
    public class JwtConfig
    {
        public const string SectionName = "JwtConfig";
        public string Secret { get; init; } = null!;
        public int ExpiryMinutes { get; init; }
        public int RefreshTokenExpiryMonths { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}
