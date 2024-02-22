using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.States;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace SpellCrafting.UI.Systems;

[Autoload(Side = ModSide.Client)]
public class WandInscriptionUISystem : ModSystem
{
    private GameTime oldGameTime;
    private UserInterface wandInscriptionInterface;
    private WandInscriptionUIState wandInscriptionState;

    public static WandInscriptionUISystem Instance => ModContent.GetInstance<WandInscriptionUISystem>();

    public static List<Echo> InscribedEchoes { get; private set; } = new();

    public override void Load() {
        wandInscriptionInterface = new UserInterface();

        wandInscriptionState = new WandInscriptionUIState();
        wandInscriptionState.Activate();
    }

    public override void UpdateUI(GameTime gameTime) {
        oldGameTime = gameTime;
        if (wandInscriptionInterface?.CurrentState is not null) {
            wandInscriptionInterface.Update(gameTime);
        }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        int inventoryLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
        if (inventoryLayerIndex == -1) {
            Mod.Logger.Error("Failed to find 'Vanilla: Inventory' UI Layer!");
            return;
        }

        layers.Insert(inventoryLayerIndex, new LegacyGameInterfaceLayer(
            $"{nameof(SpellCrafting)}: {nameof(WandInscriptionUISystem)}",
            () => {
                if (oldGameTime is not null && wandInscriptionInterface?.CurrentState is not null) {
                    wandInscriptionInterface.Draw(Main.spriteBatch, oldGameTime);
                }

                return true;
            },
            InterfaceScaleType.UI
        ));
    }

    public static void Show() {
        Instance.wandInscriptionInterface?.SetState(Instance.wandInscriptionState);
        Instance.wandInscriptionState.InscriptionPanel.RefreshInscribedEchoes(InscribedEchoes);
    }

    public static void Hide() {
        Instance.wandInscriptionInterface?.SetState(null);
    }

    public static void Toggle() {
        if (Instance.wandInscriptionInterface?.CurrentState is null) {
            Show();
            return;
        }

        Hide();
    }

    public static void ModifyInscribedEchoes(List<Echo> newEchoes) {
        InscribedEchoes = newEchoes;
        Instance.wandInscriptionState.InscriptionPanel.RefreshInscribedEchoes(newEchoes);
    }
}

public class TestToggleCommand : ModCommand
{
    public override string Command => "toggleui";
    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args) {
        WandInscriptionUISystem.Toggle();
    }
}
