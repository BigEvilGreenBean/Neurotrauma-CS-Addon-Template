
namespace MyAddon;

public class AddonItemMethods
{
    public static void DefineAllItems()
    {
        NTItemMethods.RegisterItemUseFunction("example_item", infos =>
        {
        });
    }
}


