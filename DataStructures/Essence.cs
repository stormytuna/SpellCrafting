using System;
using Terraria;

namespace SpellCrafting.DataStructures;

public struct Essence
{
    public EssenceType EssenceType { get; }
    public int WhoAmI { get; }

    public Essence(EssenceType essenceType, int whoAmI) {
        EssenceType = essenceType;
        WhoAmI = whoAmI;
    }

    public override string ToString() => $"{EssenceType} ({WhoAmI})";

    public static Essence FromPlayer(Player player) => new(EssenceType.Player, player.whoAmI);

    public static Essence FromNPC(NPC npc) => new(EssenceType.NPC, npc.whoAmI);

    public static Essence FromProjectile(Projectile projectile) => new(EssenceType.Projectile, projectile.whoAmI); // TODO: Won't work properly in MP, use projectile.identity

    public static Essence FromEntity(Entity entity) => entity switch {
        Player player => FromPlayer(player),
        NPC npc => FromNPC(npc),
        Projectile projectile => FromProjectile(projectile),
        _ => throw new ArgumentException($"Unsupported entity type '{entity.GetType().Name}'!", nameof(entity))
    };

    public static bool TryGetPlayerFromEssence(Essence essence, out Player player) {
        player = null;

        if (essence.EssenceType != EssenceType.Player || essence.WhoAmI < 0 || essence.WhoAmI >= Main.maxPlayers) {
            return false;
        }

        player = Main.player[essence.WhoAmI];
        return true;
    }

    public static bool TryGetNPCFromEssence(Essence essence, out NPC npc) {
        npc = null;

        if (essence.EssenceType != EssenceType.NPC || essence.WhoAmI < 0 || essence.WhoAmI >= Main.maxNPCs) {
            return false;
        }

        npc = Main.npc[essence.WhoAmI];
        return true;
    }

    public static bool TryGetProjectileFromEssence(Essence essence, out Projectile projectile) {
        projectile = null;

        if (essence.EssenceType != EssenceType.Projectile || essence.WhoAmI < 0 || essence.WhoAmI >= Main.maxProjectiles) {
            return false;
        }

        projectile = Main.projectile[essence.WhoAmI];
        return true;
    }
}

public enum EssenceType : byte
{
    Player, NPC, Projectile
}
