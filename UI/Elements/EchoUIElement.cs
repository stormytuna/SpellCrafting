using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.Elements;

public class EchoUIElement : UIElement
{
    private readonly Echo echo;
    private bool dragging;
    private Vector2 mouseOffset;

    public EchoUIElement(Echo echo) {
        this.echo = echo;
    }

    public override void OnInitialize() {
        UIPanel container = new() {
            Width = StyleDimension.FromPixels(400),
            Height = StyleDimension.FromPixels(50)
        };
        Append(container);

        UIImage icon = new(echo.Icon);
        container.Append(icon);

        UIText displayName = new(echo.DisplayName, 0.8f) {
            Left = StyleDimension.FromPixels(40),
            VAlign = 0.5f
        };
        container.Append(displayName);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch) {
        this.TryPreventMouseInteraction();
    }

    public override void LeftDoubleClick(UIMouseEvent evt) {
        // TODO: Need a better way of doing this!!
        EnscribedEchoUIList enscribedEchoList = Parent.Parent.Parent.Parent.Children.ElementAt(0).Children.FirstOrDefault(ele => ele is EnscribedEchoUIList) as EnscribedEchoUIList;
        enscribedEchoList.AddEchoUiElement(echo);
    }
}
