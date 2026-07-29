
namespace MyAddon
{
    // Server-side AND Singleplayer code ONLY!
    public partial class AddonInit
    {
        // Server-specific code
        public void InitializeServer()
        {
            AddonAfflictions.DefineAllAfflictions();
            AddonStats.DefineAllStats();
            AddonItemMethods.DefineAllItems();
        }

        public void OnLoadCompletedServerside()
        {
            HF.Print("Running OnLoadCompletedServerside");
            InitLuaHooks(); // Initializes the Lua hooks at the bottom of this file
            AddHumanUpdateHooks();

            harmony = new Harmony("ntaddon.server");
        }

        public void DisposeServer()
        {
        }

        public static void InitLuaHooks() // Based off the Traumatic Presence mod by Lenny!
        {
#pragma warning disable CS0618 // Type or member is obsolete

#pragma warning restore CS0618 // Type or member is obsolete
        }

        public static void AddHumanUpdateHooks()
        {
        }
    }
    }
