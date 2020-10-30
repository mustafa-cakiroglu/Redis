namespace RedisProjectForCloud.Config
{
    public class RedisSettings
    {
        public string Password { get; set; }

        public bool AllowAdmin { get; set; }

        public bool Ssl { get; set; }

        public string ConnectTimeout { get; set; }

        public int ConnectRetry { get; set; }

        public string Database { get; set; }

        public HostSettings HostSettings { get; set; }
    }
}
