using Script.Runtime.Context.Game.Scripts.Models.Grid;
using Script.Runtime.Context.Game.Scripts.View.PipeSpawn;
using strange.extensions.command.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Command
{
  public class ObjectPositionChangedCommand : EventCommand
  {
    [Inject]
    public IGridModel gridModel { get; set; }

    public override void Execute()
    {
      Transform oldParent = evt.data as Transform;

      if (oldParent == null)
      {
        return;
      }

      gridModel.SetIsOccupied(oldParent.name, false);

      bool isInMap = gridModel.GetIsInMap(oldParent.name);

      if (!isInMap)
      {
        dispatcher.Dispatch(PipeSpawnEvents.SpawnSlotEmpty, oldParent);
      }
    }
  }
}
