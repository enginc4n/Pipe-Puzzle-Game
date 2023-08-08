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
        dispatcher.Dispatch(ItemSlotEvents.ItemDropped, droppedObject);
      }
    }

    public void SetDroppedItem(GameObject droppedItem)
    {
      droppedItem.transform.SetParent(transform);
      RectTransform droppedItemRectTransform = droppedItem.GetComponent<RectTransform>();
      droppedItemRectTransform.sizeDelta = _itemSlotRectTransform.sizeDelta;
    }

    public string GetPosition()
    {
      return transform.name;
    }
  }
}
