namespace SchoolSchedule.Infrastructure.Authentication.TokenGenerator
{
    internal class JwtSetting
    {
        public const string Jwt = "JWT";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; } = 60;
    }
}
