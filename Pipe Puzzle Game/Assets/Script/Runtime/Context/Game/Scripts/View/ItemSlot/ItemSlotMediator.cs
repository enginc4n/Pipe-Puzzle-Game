using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using Script.Runtime.Context.Game.Scripts.View.Pipe;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.ItemSlot
{
  public enum ItemSlotEvents
  {
    ItemSlotFilled
  }

  public class ItemSlotMediator : EventMediator
  {
    [Inject]
    public ItemSlotView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(ItemSlotEvents.ItemSlotFilled, OnItemPlaced);
    }

    private void OnItemPlaced(IEvent evt)
    {
      GameObject droppedObject = evt.data as GameObject;
      if (droppedObject == null)
      {
        return;
      }

      string position = view.GetPosition();
      PipeType pipeType = droppedObject.GetComponent<PipeView>().pipeType;

      bool isOccupied = gridModel.GetIsOccupied(position);
      if (isOccupied)
      {
        return;
      }

      gridModel.SetIsOccupied(position, true);
      gridModel.SetPipeType(position, pipeType);
      view.SetItem(droppedObject);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(ItemSlotEvents.ItemSlotFilled, OnItemPlaced);
    }
  }
}
