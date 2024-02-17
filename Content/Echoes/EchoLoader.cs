using System.Collections.Generic;
using SpellCrafting.DataStructures;
using Terraria;
using Terraria.ModLoader;

namespace SpellCrafting.Content.Echoes;

public static class EchoLoader
{
    public static readonly List<Echo> Echoes = new();

    public static int Add(Echo echo) {
        Echoes.Add(echo);
        return Echoes.Count - 1;
    }

    public static void CastSpell(List<Echo> echoes, Player caster) {
        SpellStack spellStack = new();

        foreach (Echo echo in echoes) {
            bool successful = echo.ApplyToStack(spellStack, caster);

            string logMessage = successful ? $"Casted Echo '{echo.Name}'!" : $"Failed to cast echo '{echo.Name}'!";
            ModContent.GetInstance<SpellCrafting>().Logger.Info(logMessage);
            spellStack.LogStack();
        }
    }
}
