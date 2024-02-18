using SpellCrafting.DataStructures;
using SpellCrafting.ModTypes;
using Terraria;

namespace SpellCrafting.Content.Echoes;

public class Number : Echo
{
    private readonly float number;

    public Number(float number) {
        this.number = number;
    }

    public override bool ApplyToStack(SpellStack spellStack, Player caster) {
        spellStack.Push(number);
        return true;
    }
}
