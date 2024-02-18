using System;
using SpellCrafting.DataStructures;
using SpellCrafting.ModTypes;
using Terraria;
using Terraria.ID;

namespace SpellCrafting.Content.Echoes;

public class Ignite : Echo
{
    public override bool ApplyToStack(SpellStack spellStack, Player caster) {
        spellStack.TryPopOptional(out float rawMagnitude);
        int magnitude = Math.Clamp((int)MathF.Floor(rawMagnitude), 1, 10);

        if (!spellStack.TryPopAndParse(out Essence essenceToIgnite)) {
            return false;
        }

        if (Essence.TryGetPlayerFromEssence(essenceToIgnite, out Player player)) {
            player.AddBuff(GetBuffForMagnitude(magnitude), GetTimeForMagnitude(magnitude)); // TODO: Change time to pull from stack
        } else if (Essence.TryGetNPCFromEssence(essenceToIgnite, out NPC npc)) {
            npc.AddBuff(GetBuffForMagnitude(magnitude), GetTimeForMagnitude(magnitude));
        } else if (Essence.TryGetProjectileFromEssence(essenceToIgnite, out Projectile projectile)) {
            // TODO: Implement on projectiles
        }

        return true;
    }

    // TODO: Separate methods for player, npc, projectile
    private int GetBuffForMagnitude(int magnitude) => magnitude switch {
        < 4 => BuffID.OnFire,
        < 7 => BuffID.OnFire3,
        < 10 => BuffID.ShadowFlame,
        10 => BuffID.Daybreak
    };

    private int GetTimeForMagnitude(int magnitude) => magnitude switch {
        < 3 => 90,
        < 5 => 120,
        < 7 => 140,
        < 9 => 150,
        < 11 => 160
    };
}
