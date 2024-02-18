using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace SpellCrafting.UI.Elements;

public class EnscribedEchoUIElement : UIElement
{
    public readonly Echo Echo;
    public int order;

    public EnscribedEchoUIElement(Echo echo, int order) {
        Echo = echo;
        this.order = order;
    }

    private Asset<Texture2D> DeleteIcon => ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/DeleteIcon", AssetRequestMode.ImmediateLoad);
    private Asset<Texture2D> MoveUpIcon => ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/MoveUpIcon", AssetRequestMode.ImmediateLoad);
    private Asset<Texture2D> MoveDownIcon => ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/MoveDownIcon", AssetRequestMode.ImmediateLoad);

    public override void OnInitialize() {
        UIPanel container = new() {
            Width = StyleDimension.FromPixels(450),
            Height = StyleDimension.FromPixels(50)
        };
        Append(container);

        UIImage icon = new(Echo.Icon) {
            VAlign = 0.5f
        };
        container.Append(icon);

        UIText displayName = new(Echo.DisplayName, 0.8f) {
            Left = StyleDimension.FromPixels(40),
            VAlign = 0.5f
        };
        container.Append(displayName);

        UIImageButton deleteButton = new(DeleteIcon) {
            Left = StyleDimension.FromPixelsAndPercent(-40f, 1f),
            VAlign = 0.5f
        };
        deleteButton.OnLeftClick += DeleteEnscribedEcho;
        container.Append(deleteButton);

        UIImageButton moveUpButton = new(MoveUpIcon) {
            Left = StyleDimension.FromPixelsAndPercent(-80f, 1f),
            VAlign = 0.5f
        };
        moveUpButton.OnLeftClick += MoveElementUp;
        container.Append(moveUpButton);

        UIImageButton moveDownButton = new(MoveDownIcon) {
            Left = StyleDimension.FromPixelsAndPercent(-120f, 1f),
            VAlign = 0.5f
        };
        moveDownButton.OnLeftClick += MoveElementDown;
        container.Append(moveDownButton);
    }


    protected override void DrawSelf(SpriteBatch spriteBatch) {
        this.TryPreventMouseInteraction();
    }

    private static void DeleteEnscribedEcho(UIMouseEvent evt, UIElement listeningelement) {
        EnscribedEchoUIList parentList = listeningelement.RecursivelyFindParent<EnscribedEchoUIList>();
        EnscribedEchoUIElement parent = listeningelement.RecursivelyFindParent<EnscribedEchoUIElement>();
        parentList.RemoveEnscribedEchoUiElement(parent);
    }

    private void MoveElementUp(UIMouseEvent evt, UIElement listeningelement) {
        EnscribedEchoUIList parentList = listeningelement.RecursivelyFindParent<EnscribedEchoUIList>();
        EnscribedEchoUIElement parent = listeningelement.RecursivelyFindParent<EnscribedEchoUIElement>();
        parentList.MoveEchoUiElementUp(parent);
    }

    private void MoveElementDown(UIMouseEvent evt, UIElement listeningelement) {
        EnscribedEchoUIList parentList = listeningelement.RecursivelyFindParent<EnscribedEchoUIList>();
        EnscribedEchoUIElement parent = listeningelement.RecursivelyFindParent<EnscribedEchoUIElement>();
        parentList.MoveEchoUiElementDown(parent);
    }
}
