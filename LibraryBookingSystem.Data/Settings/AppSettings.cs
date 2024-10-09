namespace LibraryBookingSystem.Data.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public IdentityServer IdentityServer { get; set; }

    }

    public class IdentityServer{
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
