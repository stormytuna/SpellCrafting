using Microsoft.Xna.Framework.Graphics;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.States;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.Echoes;

public class EchoesPanel : UIPanel
{
    public override void OnInitialize() {
        // TODO: Add category tabs

        UIList allEchoesList = new() {
            Width = StyleDimension.FromPixelsAndPercent(-WandInscriptionUIState.ScrollbarWidth, 1f),
            Height = StyleDimension.Fill
        };

        foreach (Echo echo in EchoLoader.Echoes) {
            EchoUIElement echoUiElement = new(echo) {
                Width = StyleDimension.Fill,
                Height = StyleDimension.FromPixels(50)
            };
            allEchoesList.Add(echoUiElement);
        }

        Append(allEchoesList);

        UIScrollbar inscriptionUiListScrollbar = new() {
            Width = StyleDimension.FromPixels(WandInscriptionUIState.ScrollbarWidth),
            Height = StyleDimension.Fill,
            Left = StyleDimension.FromPixelsAndPercent(-WandInscriptionUIState.ScrollbarWidth / 2, 1f)
        };
        Append(inscriptionUiListScrollbar);
        allEchoesList.SetScrollbar(inscriptionUiListScrollbar);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        this.TryPreventMouseInteraction();
        this.TryPreventScrollWheelInteraction();
    }
}
