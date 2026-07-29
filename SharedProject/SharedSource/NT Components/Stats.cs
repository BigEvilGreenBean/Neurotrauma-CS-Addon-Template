
namespace MyAddon
{
    public static class AddonStats
    {
        public static void DefineAllStats()
        {
            NTStats.Stats["example_stat"] = new NTStatDouble("example_stat", 0, 100, 1, (C) =>
            {
                return 1;
            });

            NTStats.Stats["example_bool_stat"] = new NTStatBool("example_bool_stat", false, (C) =>
            {
                return false;
            });
        }

    }
}