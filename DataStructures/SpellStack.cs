using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace SpellCrafting.DataStructures;

public class SpellStack
{
    private readonly Stack<object> spellStack = new();

    public bool TryPopOptional<T1>(out T1 arg1, T1 default1 = default) {
        arg1 = default1;

        object peek = spellStack.Peek();
        if (peek is not T1) {
            return false;
        }

        spellStack.Pop();
        arg1 = (T1)peek;
        return true;
    }

    public bool TryPopAndParse<T1>(out T1 arg1) {
        arg1 = default;

        if (!spellStack.TryPop(out object result1) || result1 is not T1) {
            GarbleStack(0, 1);
            return false;
        }

        arg1 = (T1)result1;
        return true;
    }

    public bool TryPopAndParse<T1, T2>(out T1 arg1, out T2 arg2) {
        arg1 = default;
        arg2 = default;

        if (!spellStack.TryPop(out object result1) || result1 is not T1) {
            GarbleStack(1, 2);
            return false;
        }

        if (!spellStack.TryPop(out object result2) || result2 is not T2) {
            GarbleStack(0, 2);
            return false;
        }

        arg1 = (T1)result1;
        arg2 = (T2)result2;
        return true;
    }

    public void Push(params object[] args) {
        foreach (object arg in args) {
            spellStack.Push(arg);
        }
    }

    public void GarbleStack(int amountToPop, int amountToPush) {
        for (int i = 0; i < amountToPop; i++) {
            spellStack.TryPop(out _);
        }

        for (int i = 0; i < amountToPush; i++) {
            // TODO: Garble stack
        }
    }

    public void LogStack() {
        ModContent.GetInstance<SpellCrafting>().Logger.Info("Stack:");
        for (int i = 0; i < spellStack.Count; i++) {
            ModContent.GetInstance<SpellCrafting>().Logger.Info($"{i}: {spellStack.ElementAt(i)}");
        }

        ModContent.GetInstance<SpellCrafting>().Logger.Info("-----");
    }
}
