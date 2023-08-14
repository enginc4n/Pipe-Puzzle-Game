using Script.Runtime.Context.Game.Scripts.Models.Grid;
using Script.Runtime.Context.Game.Scripts.View.PipeSlot;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Config
{
  public class GameControlFacade
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public void SpawnPipeForPipeSlot(Transform oldParent)
    {
      string position = oldParent.name;
      bool isInMap = gridModel.GetIsInMap(position);
      if (!isInMap)
      {
        dispatcher.Dispatch(PipeSpawnEvents.SpawnSlotEmpty, oldParent);
      }
    }

    public void AdjustOldParent(Transform oldParent)
    {
      string position = oldParent.name;
      gridModel.SetIsOccupied(position, false);
      gridModel.SetIsHaveWater(position, false);
    }
  }
}
