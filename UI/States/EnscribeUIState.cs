using Microsoft.Xna.Framework;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.Elements;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.States;

public class EnscribeUIState : UIState
{
    public override void OnInitialize() {
        UIPanel mainPanel = new();
        mainPanel.Width.Pixels = 900;
        mainPanel.Height.Pixels = 600;
        mainPanel.HAlign = 0.2f;
        mainPanel.VAlign = 0.4f;
        mainPanel.BackgroundColor = Color.CornflowerBlue;
        Append(mainPanel);

        UIPanel enscribedEchoesPanel = new() {
            Width = StyleDimension.FromPercent(0.5f),
            Height = StyleDimension.Fill
        };
        mainPanel.Append(enscribedEchoesPanel);

        EnscribedEchoUIList enscribedEchoesList = new() {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };
        enscribedEchoesPanel.Append(enscribedEchoesList);

        foreach (Echo echo in EchoLoader.Echoes) {
            enscribedEchoesList.AddEchoUiElement(echo);
        }

        UIPanel allEchoesPanel = new() {
            Width = StyleDimension.FromPercent(0.5f),
            Height = StyleDimension.Fill,
            Left = StyleDimension.FromPercent(0.5f)
        };
        mainPanel.Append(allEchoesPanel);

        UIList allEchoesList = new() {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };
        allEchoesPanel.Append(allEchoesList);

        foreach (Echo echo in EchoLoader.Echoes) {
            EchoUIElement echoUiElement = new(echo);
            echoUiElement.Width = StyleDimension.FromPixels(400);
            echoUiElement.Height = StyleDimension.FromPixels(50);
            allEchoesList.Add(echoUiElement);
        }
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
    }
}
