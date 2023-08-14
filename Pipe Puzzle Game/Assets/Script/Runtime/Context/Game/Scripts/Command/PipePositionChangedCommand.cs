using Script.Runtime.Context.Game.Scripts.Config;
using strange.extensions.command.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Command
{
  public class PipePositionChangedCommand : EventCommand
  {
    [Inject]
    public GameControlFacade gameControlFacade { get; set; }

    public override void Execute()
    {
      Transform oldParent = evt.data as Transform;
      gameControlFacade.AdjustOldParent(oldParent);
      gameControlFacade.SpawnPipeForPipeSlot(oldParent);
    }
  }
}
