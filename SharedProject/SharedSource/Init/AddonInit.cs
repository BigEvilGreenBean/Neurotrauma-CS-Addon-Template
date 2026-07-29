
namespace MyAddon
{
    public partial class AddonInit : IAssemblyPlugin
    {
        // ---------------------------         Ydrec Shit         --------------------------- \\

        // These are automatically assigned by the plugin service after the Constructor is called
        public IConfigService ConfigService { get; set; }
        public IEventService EventService { get; set; }
        public IPluginManagementService PluginService { get; set; }
        public ILoggerService LoggerService { get; set; }
        public ILuaScriptManagementService luaScriptManagementService = LuaCsSetup.Instance.LuaScriptManagementService;
        private Harmony ?harmony;

        // ---------------------------        Functions        --------------------------- \\
        // Called right after the constructor
        public void PreInitPatching()
        {
        }

        // When your plugin is loading, use this instead of the constructor for code relying on
        // the services above.
        // Put any code here that does not rely on other plugins.
        public void Initialize()
        {

            if (HF.GameIsMultiplayer())
            {
                #if SERVER
                    HF.Print("Initializing for Multiplayer.");
                    InitializeServer();
                #endif
            }

            if (HF.GameIsSingleplayer())
            {
                // ServersideInit.cs
                HF.Print("Initializing for Singleplayer.");
                InitializeServer();
            }
        }

        public void AddPatches()
        {
        }

        public void RemovePatches()
        {
        }

        // After all plugins have loaded
        // Put code that interacts with other plugins here.
        public void OnLoadCompleted()
        {
            // Shared Scripts
            AddonConfigData.Register();

            NTInfo.RegisterAddon(new NTAddon
                { 
                    Name = "Example Addon",
                    Version = "1.0.0",
                    VersionNum = 01000000,
                    MinNTVersion = "A1.17.4",
                    MinNTVersionNum = 1170400
                });

            // Serverside code that ALSO runs in Singleplayer
            // Add functions in SharedSource/SharedInit.cs
            if (HF.GameIsMultiplayer())
            {
                #if SERVER
                    HF.Print("OnLoadCompleted for Multiplayer.");
                    OnLoadCompletedServerside();
                    AddPatches();
                #endif
            }

            if (HF.GameIsSingleplayer())
            {
                // ServersideInit.cs
                HF.Print("OnLoadCompleted for Singleplayer.");
                OnLoadCompletedServerside();
                AddPatches();
            }

            // Clientside code
            // Add functions in ClientSource/ClientInit.cs
            #if CLIENT
                InitClientOnly();
            #endif

            // Serverside code that ONLY runs in Multiplayer
            // Add functions in ServerSource/ServerInit.cs
            #if SERVER
                InitServerOnly();
            #endif
        }

        public void Dispose()
        {
            RemovePatches();
            
            if (HF.IsMain())
            {
                if (harmony != null) harmony.UnpatchSelf();
            }
        }
    }
}
