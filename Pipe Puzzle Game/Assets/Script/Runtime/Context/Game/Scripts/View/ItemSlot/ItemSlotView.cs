using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Runtime.Context.Game.Scripts.View.ItemSlot
{
  public class ItemSlotView : EventView, IDropHandler
  {
    private RectTransform _itemSlotRectTransform;

    protected override void Awake()
    {
      base.Awake();
      _itemSlotRectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
      GameObject droppedObject = eventData.pointerDrag;

      if (droppedObject != null)
      {
        dispatcher.Dispatch(ItemSlotEvents.ItemSlotFilled, droppedObject);
      }
    }

    public void SetItem(GameObject droppedItem)
    {
      droppedItem.transform.SetParent(transform);

      RectTransform droppedItemRectTransform = droppedItem.GetComponent<RectTransform>();
      droppedItemRectTransform.sizeDelta = _itemSlotRectTransform.sizeDelta;

      BoxCollider2D droppedItemBoxCollider2D = droppedItem.GetComponent<BoxCollider2D>();
      droppedItemBoxCollider2D.size = _itemSlotRectTransform.sizeDelta;
    }

    public string GetPosition()
    {
      return name;
    }
  }
}
