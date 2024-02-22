using SpellCrafting.DataStructures;
using SpellCrafting.Enums;
using SpellCrafting.ModTypes;
using Terraria;

namespace SpellCrafting.Content.Echoes;

public class EssenceOfProximity : Echo
{
    public override EchoCategory Category => EchoCategory.Essence;

    public override bool ApplyToStack(SpellStack spellStack, Player caster) {
        Entity closestEntity = null;
        float closestEntityDistance = float.PositiveInfinity;

        for (int i = 0; i < Main.maxPlayers; i++) {
            Player player = Main.player[i];

            if (player.active && player.whoAmI != caster.whoAmI && !player.dead && player.WithinRange(caster.Center, closestEntityDistance)) {
                closestEntity = player;
                closestEntityDistance = player.Distance(caster.Center);
            }
        }

        for (int i = 0; i < Main.maxNPCs; i++) {
            NPC npc = Main.npc[i];

            if (npc.active && npc.CanBeChasedBy() && npc.WithinRange(caster.Center, closestEntityDistance)) {
                closestEntity = npc;
                closestEntityDistance = npc.Distance(caster.Center);
            }
        }

        for (int i = 0; i < Main.maxProjectiles; i++) {
            Projectile projectile = Main.projectile[i];

            if (projectile.active && projectile.WithinRange(caster.Center, closestEntityDistance)) {
                closestEntity = projectile;
                closestEntityDistance = projectile.Distance(caster.Center);
            }
        }

        closestEntity ??= caster;
        spellStack.Push(Essence.FromEntity(closestEntity));
        return true;
    }
}
