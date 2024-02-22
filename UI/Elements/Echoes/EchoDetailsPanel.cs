using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SpellCrafting.ModTypes;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.Echoes;

public class EchoDetailsPanel : UIPanel
{
    private const float TitleHeightPixels = 32;
    private const float CategoryHeightPercent = 0.1f;
    private const float PopPushInfoHeightPercent = 0.1f;

    private static readonly Asset<Texture2D> BlankIcon = ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/EchoIconTemplate", AssetRequestMode.ImmediateLoad);
    private UIText category;
    private UIText description;
    private UIText displayName;
    private UIImage icon;
    private UIText popPushInfo;

    public bool Visible { get; set; }

    public override void OnInitialize() {
        icon = new UIImage(BlankIcon) {
            Width = StyleDimension.FromPixels(TitleHeightPixels),
            Height = StyleDimension.FromPixels(TitleHeightPixels),
            ScaleToFit = true
        };
        Append(icon);

        displayName = new UIText(LocalizedText.Empty, 1.2f) {
            Width = StyleDimension.FromPixelsAndPercent(-icon.Width.Pixels, 1f),
            Height = StyleDimension.FromPixels(TitleHeightPixels),
            Left = StyleDimension.FromPixels(icon.Width.Pixels),
            TextOriginX = 0f,
            TextOriginY = 0.5f,
            PaddingLeft = 5
        };
        Append(displayName);

        category = new UIText(LocalizedText.Empty, 0.8f) {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPercent(CategoryHeightPercent),
            Top = StyleDimension.FromPixels(TitleHeightPixels),
            TextOriginX = 0f,
            TextOriginY = 0.5f
        };
        Append(category);

        popPushInfo = new UIText(LocalizedText.Empty, 0.8f) {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPercent(PopPushInfoHeightPercent),
            Top = StyleDimension.FromPixelsAndPercent(TitleHeightPixels, CategoryHeightPercent),
            TextOriginX = 0f,
            TextOriginY = 0.5f
        };
        Append(popPushInfo);

        description = new UIText(LocalizedText.Empty) {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPixelsAndPercent(-TitleHeightPixels, 1f - CategoryHeightPercent - PopPushInfoHeightPercent),
            Top = StyleDimension.FromPixelsAndPercent(TitleHeightPixels, CategoryHeightPercent + PopPushInfoHeightPercent),
            TextOriginX = 0f,
            TextOriginY = 0f,
            IsWrapped = true
        };
        Append(description);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (!Visible) {
            return;
        }

        base.Draw(spriteBatch);
    }

    public void DisplayEchoDetails(Echo echo) {
        icon.SetImage(echo.Icon);
        displayName.SetText(echo.DisplayName);
        category.SetText(echo.CategoryText);
        popPushInfo.SetText(echo.PopPushInfo);
        description.SetText(echo.Description);
        Visible = true;
    }

    public void HideEchoDetails() {
        icon.SetImage(BlankIcon);
        category.SetText(LocalizedText.Empty);
        popPushInfo.SetText(LocalizedText.Empty);
        displayName.SetText(LocalizedText.Empty);
        description.SetText(LocalizedText.Empty);
        //Visible = false;
    }
}
