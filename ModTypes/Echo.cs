using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SpellCrafting.Content.Echoes;
using SpellCrafting.DataStructures;
using SpellCrafting.Enums;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpellCrafting.ModTypes;

public abstract class Echo : ModType, ILocalizedModType
{
    public static Echo EssenceOfSelf { get; private set; } = new EssenceOfSelf();

    public int Type { get; private set; }

    public abstract EchoCategory Category { get; }

    public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);
    public virtual LocalizedText Description => this.GetLocalization(nameof(Description), () => "This is a description, pretty cool right!");
    public virtual LocalizedText PopPushInfo => this.GetLocalization(nameof(PopPushInfo), () => "(null -> null)");
    public LocalizedText CategoryText => Language.GetOrRegister($"Mods.SpellCrafting.EchoCategories.{Category}");
    public virtual Asset<Texture2D> Icon => ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/EchoIconTemplate", AssetRequestMode.ImmediateLoad);

    public string LocalizationCategory => "Echoes";

    protected sealed override void Register() {
        _ = DisplayName;
        _ = Description;
        _ = PopPushInfo;
        _ = CategoryText;

        ModTypeLookup<Echo>.Register(this);
        Type = EchoLoader.Add(this);
    }

    public sealed override void SetupContent() {
        SetStaticDefaults();
    }

    public override string ToString() => Name;

    public virtual bool ApplyToStack(SpellStack spellStack, Player caster) => true;

    public virtual Echo Clone() => (Echo)MemberwiseClone();
}
