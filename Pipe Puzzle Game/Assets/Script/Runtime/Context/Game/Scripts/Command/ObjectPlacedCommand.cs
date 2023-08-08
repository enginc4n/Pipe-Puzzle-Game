using Script.Runtime.Context.Game.Scripts.Models.Grid;
using strange.extensions.command.impl;

namespace Script.Runtime.Context.Game.Scripts.Command
{
  public class ObjectPlacedCommand : EventCommand
  {
    [Inject]
    public IGridModel gridModel { get; set; }

    public override void Execute()
    {
      string position = evt.data as string;

      gridModel.SetIsOccupied(position, true);
    }
  }
}
