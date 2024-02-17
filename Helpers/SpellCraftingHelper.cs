using SpellCrafting.Globals;
using Terraria;

namespace SpellCrafting.Helpers;

public static class SpellCraftingHelpers
{
    public static SpellCraftingPlayer SpellCraftingPlayer(this Player player) => player.GetModPlayer<SpellCraftingPlayer>();
}
