using SpellCrafting.ModTypes;
using SpellCrafting.UI.Elements.Echoes;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.WandInscription;

public class InscribedEchoUIElement : UIElement
{
    public readonly Echo Echo;

    public InscribedEchoUIElement(Echo echo) {
        Echo = echo;
    }

    public override void OnInitialize() {
        EchoUIElement echoUiElement = new(Echo) {
            Width = StyleDimension.FromPixelsAndPercent(-InscribedEchoControlsUIElement.PanelWidth, 1f),
            Height = StyleDimension.FromPixels(InscriptionPanel.InscribedEchoHeight)
        };
        Append(echoUiElement);

        InscribedEchoControlsUIElement controls = new(this) {
            Width = StyleDimension.FromPixels(InscribedEchoControlsUIElement.PanelWidth),
            Height = StyleDimension.Fill,
            Left = StyleDimension.FromPixelsAndPercent(-InscribedEchoControlsUIElement.PanelWidth, 1f)
        };
        Append(controls);
    }
}
