using SpellCrafting.DataStructures;
using SpellCrafting.Enums;
using SpellCrafting.ModTypes;
using Terraria;

namespace SpellCrafting.Content.Echoes;

public class EssenceOfSelf : Echo
{
    public override EchoCategory Category => EchoCategory.Essence;

    public override bool ApplyToStack(SpellStack spellStack, Player caster) {
        spellStack.Push(Essence.FromPlayer(caster));
        return true;
    }
}
