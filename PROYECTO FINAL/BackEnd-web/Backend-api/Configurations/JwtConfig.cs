namespace Backend.Api.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
