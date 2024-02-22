using SpellCrafting.UI.Elements.Echoes;
using SpellCrafting.UI.Elements.WandInscription;
using Terraria.UI;

namespace SpellCrafting.UI.States;

public class WandInscriptionUIState : UIState
{
    public const float ScrollbarWidth = 20;
    private const float LeftSidePadding = 200;
    private const float TopSidePadding = 200;
    private const float InscriptionPanelWidth = 400;
    private const float InscriptionPanelHeight = 500;

    public InscriptionPanel InscriptionPanel { get; private set; }
    public EchoesPanel EchoesPanel { get; private set; }

    public override void OnInitialize() {
        InscriptionPanel = new InscriptionPanel {
            Width = StyleDimension.FromPixels(InscriptionPanelWidth),
            Height = StyleDimension.FromPixels(InscriptionPanelHeight),
            Left = StyleDimension.FromPixels(LeftSidePadding),
            Top = StyleDimension.FromPixels(TopSidePadding)
        };
        Append(InscriptionPanel);

        EchoesPanel = new EchoesPanel {
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(500),
            Left = StyleDimension.FromPixels(LeftSidePadding + InscriptionPanelWidth + 20),
            Top = StyleDimension.FromPixels(TopSidePadding)
        };
        Append(EchoesPanel);
    }
}
