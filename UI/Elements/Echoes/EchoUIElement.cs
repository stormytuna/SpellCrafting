using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.Systems;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.Echoes;

public class EchoUIElement : UIElement
{
    private readonly Echo echo;

    public EchoUIElement(Echo echo) {
        this.echo = echo;
    }

    public override void OnInitialize() {
        UIImage icon = new(echo.Icon) {
            Width = StyleDimension.FromPixels(echo.Icon.Width()),
            Height = StyleDimension.FromPixels(echo.Icon.Height()),
            VAlign = 0.5f
        };
        Append(icon);

        UIText displayName = new(echo.DisplayName, 0.8f) {
            Width = StyleDimension.FromPixelsAndPercent(-icon.Width.Pixels, 1f - icon.Width.Percent),
            Height = StyleDimension.Fill,
            Left = StyleDimension.FromPixels(40),
            TextOriginX = 0f,
            TextOriginY = 0.5f
        };
        Append(displayName);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        this.TryPreventMouseInteraction();
    }

    public override void LeftDoubleClick(UIMouseEvent evt) {
        base.LeftDoubleClick(evt);
        List<Echo> newEchoes = new();
        newEchoes.AddRange(WandInscriptionUISystem.InscribedEchoes);
        newEchoes.Add(echo.Clone());
        WandInscriptionUISystem.ModifyInscribedEchoes(newEchoes);
    }

    public override void MouseOver(UIMouseEvent evt) {
        WandInscriptionUISystem.HoveringEcho(echo);
    }

    public override void MouseOut(UIMouseEvent evt) {
        WandInscriptionUISystem.HoveringEcho(null);
    }
}
