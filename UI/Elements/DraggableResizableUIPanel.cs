using Microsoft.Xna.Framework;
using SpellCrafting.Helpers;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.Elements;

public class DraggableUIPanel : UIPanel
{
    private bool dragging;
    private Vector2 mouseOffset;

    public override void LeftMouseDown(UIMouseEvent evt) {
        base.LeftMouseDown(evt);
        StartDragging(evt);
    }

    public override void LeftMouseUp(UIMouseEvent evt) {
        base.LeftMouseUp(evt);
        StopDragging(evt);
    }

    private void StartDragging(UIMouseEvent evt) {
        mouseOffset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
        dragging = true;
    }

    private void StopDragging(UIMouseEvent evt) {
        Vector2 endMousePosition = evt.MousePosition;
        Left.Set(endMousePosition.X - mouseOffset.X, 0f);
        Top.Set(endMousePosition.Y - mouseOffset.Y, 0f);
        Recalculate();

        dragging = false;
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

        this.TryPreventMouseInteraction();

        if (dragging) {
            Left.Set(Main.mouseX - mouseOffset.X, 0f);
            Top.Set(Main.mouseY - mouseOffset.Y, 0f);
            Recalculate();
        }

        Rectangle parentSpace = Parent.GetDimensions().ToRectangle();
        if (GetDimensions().ToRectangle().Intersects(parentSpace)) {
            return;
        }

        // Force this panel into the parent if we try dragging it outside
        Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
        Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
        Recalculate();
    }
}
