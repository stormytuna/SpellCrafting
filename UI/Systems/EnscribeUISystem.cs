using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SpellCrafting.Content.Items;
using SpellCrafting.UI.Elements;
using SpellCrafting.UI.States;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace SpellCrafting.UI.Systems;

[Autoload(Side = ModSide.Client)]
public class EnscribeUISystem : ModSystem
{
    private UserInterface enscribeInterface;
    public EnscribeUIState EnscribeState;
    private GameTime oldGameTime;
    public static EnscribeUISystem Instance => ModContent.GetInstance<EnscribeUISystem>();

    public override void Load() {
        enscribeInterface = new UserInterface();

        EnscribeState = new EnscribeUIState();
        EnscribeState.Activate();
    }

    public override void UpdateUI(GameTime gameTime) {
        oldGameTime = gameTime;
        if (enscribeInterface?.CurrentState is not null) {
            enscribeInterface.Update(gameTime);
        }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        int inventoryLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
        if (inventoryLayerIndex == -1) {
            Mod.Logger.Error("Failed to find 'Vanilla: Inventory' UI Layer!");
        }

        layers.Insert(inventoryLayerIndex, new LegacyGameInterfaceLayer(
            $"{nameof(SpellCrafting)}: {nameof(enscribeInterface)}",
            () => {
                if (oldGameTime is not null && enscribeInterface?.CurrentState is not null) {
                    enscribeInterface.Draw(Main.spriteBatch, oldGameTime);
                }

                return true;
            },
            InterfaceScaleType.UI
        ));
    }

    public static void Show() {
        Instance.enscribeInterface?.SetState(Instance.EnscribeState);
    }

    public static void Hide() {
        Instance.enscribeInterface?.SetState(null);
    }

    public static void Toggle() {
        if (Instance.enscribeInterface?.CurrentState is null) {
            Show();
            return;
        }

        Hide();
    }
}

public class TestToggleCommand : ModCommand
{
    public override string Command => "toggleui";
    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args) {
        EnscribeUISystem.Toggle();
    }
}

public class TestEnscribeCommand : ModCommand
{
    public override string Command => "enscribe";
    public override CommandType Type => CommandType.Chat;

    public override void Action(CommandCaller caller, string input, string[] args) {
        if (Main.LocalPlayer.HeldItem.ModItem is not TestWand wand) {
            Main.NewText("Not holding a wand!!");
            return;
        }

        EnscribedEchoUIList echoesUiList = EnscribeUISystem.Instance.EnscribeState.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0) as EnscribedEchoUIList;
        wand.ActiveSpell = echoesUiList.GetEnscribedEchoes();
    }
}
