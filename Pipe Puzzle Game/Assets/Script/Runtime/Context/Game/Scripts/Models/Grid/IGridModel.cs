using Scripts.Runtime.Modules.Core.PromiseTool;

namespace Script.Runtime.Context.Game.Scripts.Models.Grid
{
  public interface IGridModel
  {
    IPromise CreateData(int column, int row);

    bool GetIsOccupied(string position);

    void SetIsOccupied(string position, bool isOccupied);

    bool GetIsInMap(string position);
  }
}
