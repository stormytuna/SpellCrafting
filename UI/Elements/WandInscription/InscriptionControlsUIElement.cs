using System.Collections.Generic;
using SpellCrafting.Content.Items;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.Systems;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.WandInscription;

public class InscriptionControlsUIElement : UIElement
{
    public override void OnInitialize() {
        UIPanel mainPanel = new() {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        };
        Append(mainPanel);

        UIButton<LocalizedText> inscribeButton = new(Language.GetOrRegister("Mods.SpellCrafting.UI.Inscribe")) {
            Width = StyleDimension.FromPercent(0.25f),
            Height = StyleDimension.Fill
        };
        inscribeButton.OnLeftClick += Inscribe;
        mainPanel.Append(inscribeButton);
    }

    private void Inscribe(UIMouseEvent evt, UIElement listeningelement) {
        if (Main.LocalPlayer.HeldItem.ModItem is not TestWand wand) {
            return;
        }

        Main.NewText("Inscribed!");
        wand.ActiveSpell = new List<Echo>(WandInscriptionUISystem.InscribedEchoes);
        WandInscriptionUISystem.InscribedEchoes.Clear();
        WandInscriptionUISystem.Hide();
    }
}
