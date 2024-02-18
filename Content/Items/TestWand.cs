using System.Collections.Generic;
using SpellCrafting.ModTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpellCrafting.Content.Items;

public class TestWand : ModItem
{
    public List<Echo> ActiveSpell { get; set; } = new();

    public override string Texture => $"Terraria/Images/Item_{ItemID.WandofSparking}";

    public override void SetDefaults() {
        Item.width = 26;
        Item.height = 28;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(gold: 1);

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = Item.useAnimation = 32;
    }

    public override bool? UseItem(Player player) {
        EchoLoader.CastSpell(ActiveSpell, player);

        return true;
    }
}
