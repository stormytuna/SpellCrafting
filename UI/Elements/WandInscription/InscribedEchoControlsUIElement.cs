using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SpellCrafting.Helpers;
using SpellCrafting.ModTypes;
using SpellCrafting.UI.Systems;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace SpellCrafting.UI.Elements.WandInscription;

public class InscribedEchoControlsUIElement : UIElement
{
    public const float PanelWidth = 50f;

    private static readonly Asset<Texture2D> deleteIcon = ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/DeleteIcon", AssetRequestMode.ImmediateLoad);
    private static readonly Asset<Texture2D> moveUpIcon = ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/MoveUpIcon", AssetRequestMode.ImmediateLoad);
    private static readonly Asset<Texture2D> moveDownIcon = ModContent.Request<Texture2D>("SpellCrafting/Assets/Textures/MoveDownIcon", AssetRequestMode.ImmediateLoad);

    private readonly InscribedEchoUIElement parentElement;

    public InscribedEchoControlsUIElement(InscribedEchoUIElement parentElement) {
        this.parentElement = parentElement;
    }

    public override void OnInitialize() {
        UIPanel mainPanel = new() {
            Width = StyleDimension.FromPixels(PanelWidth),
            Height = StyleDimension.Fill,
            Left = StyleDimension.FromPixelsAndPercent(-PanelWidth, 1f)
        };
        Append(mainPanel);

        UIImageButton deleteButton = new(deleteIcon) {
            HAlign = 0.75f,
            VAlign = 0.25f
        };
        deleteButton.OnLeftClick += DeleteEcho;
        Append(deleteButton);

        UIImageButton moveUpButton = new(moveUpIcon) {
            HAlign = 0.25f,
            VAlign = 0.25f
        };
        moveUpButton.OnLeftClick += MoveUp;
        Append(moveUpButton);

        UIImageButton moveDownButton = new(moveDownIcon) {
            HAlign = 0.25f,
            VAlign = 0.75f
        };
        moveDownButton.OnLeftClick += MoveDown;
        Append(moveDownButton);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        base.Draw(spriteBatch);
        this.TryPreventMouseInteraction();
    }

    // TODO: Fix these!
    private void DeleteEcho(UIMouseEvent evt, UIElement listeningelement) {
        InscribedEchoControlsUIElement controls = listeningelement.RecursivelyFindParent<InscribedEchoControlsUIElement>();
        IEnumerable<Echo> newEchoes = WandInscriptionUISystem.InscribedEchoes.Where(echo => echo != controls.parentElement.Echo);
        WandInscriptionUISystem.ModifyInscribedEchoes(newEchoes.ToList());
    }

    private void MoveUp(UIMouseEvent evt, UIElement listeningelement) {
        InscribedEchoControlsUIElement controls = listeningelement.RecursivelyFindParent<InscribedEchoControlsUIElement>();
        int index = WandInscriptionUISystem.InscribedEchoes.IndexOf(controls.parentElement.Echo);
        int nextIndex = index - 1;
        if (WandInscriptionUISystem.InscribedEchoes.AnyNotInRange(index, nextIndex)) {
            return;
        }

        List<Echo> newEchoes = WandInscriptionUISystem.InscribedEchoes.SwapIndexes(index, nextIndex);
        WandInscriptionUISystem.ModifyInscribedEchoes(newEchoes.ToList());
    }

    private void MoveDown(UIMouseEvent evt, UIElement listeningelement) {
        InscribedEchoControlsUIElement controls = listeningelement.RecursivelyFindParent<InscribedEchoControlsUIElement>();
        int index = WandInscriptionUISystem.InscribedEchoes.IndexOf(controls.parentElement.Echo);
        int nextIndex = index + 1;
        if (WandInscriptionUISystem.InscribedEchoes.AnyNotInRange(index, nextIndex)) {
            return;
        }

        List<Echo> newEchoes = WandInscriptionUISystem.InscribedEchoes.SwapIndexes(index, nextIndex);
        WandInscriptionUISystem.ModifyInscribedEchoes(newEchoes.ToList());
    }
}
