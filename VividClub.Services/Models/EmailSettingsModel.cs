namespace VividClub.Services.Models
{
    public class EmailSettingsModel
    {
        public string ApplicationName { get; set; }

        public string ApplicationEmail { get; set; }

        public string ApplicationEmailPassword { get; set; }

        public string Smtp { get; set; }

        public int Port { get; set; }

    }
}