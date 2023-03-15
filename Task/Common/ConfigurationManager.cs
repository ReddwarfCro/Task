namespace Task.Common
{
    /// <summary>
    /// Configuration Manager
    /// </summary>
    static class ConfigurationManager
    {
        /// <summary>
        /// App Setting
        /// </summary>
        public static IConfiguration AppSetting
        {
            get;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}