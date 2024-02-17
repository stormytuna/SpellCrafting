using SpellCrafting.DataStructures;
using Terraria;

namespace SpellCrafting.Content.Echoes;

public class EssenceOfSelf : Echo
{
    public override bool ApplyToStack(SpellStack spellStack, Player caster) {
        spellStack.Push(Essence.FromPlayer(caster));
        return true;
    }
}
