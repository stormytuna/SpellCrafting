using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace SpellCrafting.Helpers;

public static class UIHelper
{
    public static TUIElement RecursivelyFindParent<TUIElement>(this UIElement element)
        where TUIElement : UIElement {
        if (element.Parent is null) {
            return null;
        }

        if (element.Parent is TUIElement parent) {
            return parent;
        }

        return RecursivelyFindParent<TUIElement>(element.Parent);
    }

    public static void TryPreventMouseInteraction(this UIElement element) {
        if (element.ContainsPoint(Main.MouseScreen)) {
            Main.LocalPlayer.mouseInterface = true;
        }
    }

    public static void TryPreventScrollWheelInteraction(this UIElement element) {
        if (element.IsMouseHovering) {
            PlayerInput.LockVanillaMouseScroll($"{nameof(SpellCrafting)}:{element.GetType().Name}");
        }
    }
}
