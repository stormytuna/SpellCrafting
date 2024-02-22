using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.States;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.WandInscription;

public class InscriptionPanel : UIPanel
{
    public const float InscribedEchoHeight = 50;
    public const float InscriptionListHeightPercent = 0.8f;

    public UIList InscribedEchoesList { get; private set; } = new();

    public override void OnInitialize() {
        InscribedEchoesList = new UIList {
            Width = StyleDimension.FromPixelsAndPercent(-WandInscriptionUIState.ScrollbarWidth, 1f),
            Height = StyleDimension.FromPercent(InscriptionListHeightPercent)
        };
        Append(InscribedEchoesList);

        UIScrollbar inscriptionUiListScrollbar = new() {
            Width = StyleDimension.FromPixels(WandInscriptionUIState.ScrollbarWidth),
            Height = StyleDimension.FromPercent(InscriptionListHeightPercent),
            Left = StyleDimension.FromPixelsAndPercent(-WandInscriptionUIState.ScrollbarWidth / 2, 1f)
        };
        Append(inscriptionUiListScrollbar);
        InscribedEchoesList.SetScrollbar(inscriptionUiListScrollbar);

        InscriptionControlsUIElement inscriptionControls = new() {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPercent(1f - InscriptionListHeightPercent),
            Top = StyleDimension.FromPercent(InscriptionListHeightPercent)
        };
        Append(inscriptionControls);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        this.TryPreventMouseInteraction();
        this.TryPreventScrollWheelInteraction();
    }

    public void RefreshInscribedEchoes(List<Echo> newEchoes) {
        InscribedEchoesList.Clear();
        foreach (Echo newEcho in newEchoes) {
            InscribedEchoUIElement inscribedEchoUiElement = new(newEcho) {
                Width = StyleDimension.Fill,
                Height = StyleDimension.FromPixels(InscribedEchoHeight)
            };
            inscribedEchoUiElement.Activate();
            InscribedEchoesList.Add(inscribedEchoUiElement);
        }
    }
}
