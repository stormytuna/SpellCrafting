using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.Systems;
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
            VAlign = 0.5f
        };
        Append(icon);

        UIText displayName = new(echo.DisplayName, 0.8f) {
            Left = StyleDimension.FromPixels(40),
            VAlign = 0.5f
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
}
