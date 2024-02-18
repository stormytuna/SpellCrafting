using System.Collections.Generic;
using System.Linq;
using SpellCrafting.ModTypes;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace SpellCrafting.UI.Elements;

public class EnscribedEchoUIList : UIList
{
    public List<Echo> GetEnscribedEchoes() => _items.Select(ele => ((EnscribedEchoUIElement)ele).Echo).ToList();

    public void RemoveEnscribedEchoUiElement(UIElement echoUiElement) {
        Remove(echoUiElement);
        Recalculate();
    }

    public void AddEchoUiElement(Echo echo) {
        EnscribedEchoUIElement echoUiElement = new(echo, _items.Count) {
            Width = StyleDimension.FromPixels(400),
            Height = StyleDimension.FromPixels(50)
        };
        echoUiElement.Activate();

        Add(echoUiElement);
    }

    public void MoveEchoUiElementUp(UIElement element) {
        int index = _items.IndexOf(element);
        if (_items.IndexInRange(index) && _items.IndexInRange(index - 1)) {
            SwapEchoUiElements(element, _items[index - 1]);
        }
    }

    public void MoveEchoUiElementDown(UIElement element) {
        int index = _items.IndexOf(element);
        if (_items.IndexInRange(index) && _items.IndexInRange(index + 1)) {
            SwapEchoUiElements(element, _items[index + 1]);
        }
    }

    public void SwapEchoUiElements(UIElement first, UIElement second) {
        int firstIndex = _items.IndexOf(first);
        int secondIndex = _items.IndexOf(second);
        if (!_items.IndexInRange(firstIndex) || !_items.IndexInRange(secondIndex)) {
            Main.NewText("Uhhhhmmm"); // TODO: Remove this!
            return;
        }

        (_items[firstIndex], _items[secondIndex]) = (_items[secondIndex], _items[firstIndex]);
        UpdateOrder();
        Recalculate();
    }
}
