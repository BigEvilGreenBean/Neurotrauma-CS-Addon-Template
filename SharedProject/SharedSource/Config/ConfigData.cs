namespace Neurotrauma
{
    public static class AddonConfigData
    {
        public static void Register()
        {
            NTConfig.AddConfigOptions(
                new ConfigExpansion
                {
                    Name = "Example Addon",
                    ConfigData = new Dictionary<string, ConfigEntry>
                    {
                        ["addon_header1"] = new ConfigEntry
                        {
                            Name = TextManager.Get("addonconfigname_header1"),
                            Type = ConfigEntryType.Category,
                        },
                    }
                }
            );
        }
    }
}