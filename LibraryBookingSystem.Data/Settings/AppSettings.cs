namespace LibraryBookingSystem.Data.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public JWT JWT { get; set; }

    }


    public class JWT {
        public string Key { get; set; }  
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    
}
