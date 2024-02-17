using SpellCrafting.DataStructures;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpellCrafting.Content.Echoes;

public abstract class Echo : ModType, ILocalizedModType
{
    public static Echo EssenceOfSelf { get; private set; } = new EssenceOfSelf();

    public int Type { get; private set; }

    public virtual LocalizedText DisplayName => this.GetLocalization("", PrettyPrintName);

    public string LocalizationCategory => "Echoes";

    protected sealed override void Register() {
        ModTypeLookup<Echo>.Register(this);
        Type = EchoLoader.Add(this);
    }

    public sealed override void SetupContent() {
        SetStaticDefaults();
    }

    public override string ToString() => Name;

    public virtual bool ApplyToStack(SpellStack spellStack, Player caster) => true;
}
