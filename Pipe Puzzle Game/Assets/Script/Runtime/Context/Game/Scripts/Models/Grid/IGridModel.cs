using Script.Runtime.Context.Game.Scripts.Enums;
using Scripts.Runtime.Modules.Core.PromiseTool;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Models.Grid
{
  public interface IGridModel
  {
    IPromise CreateData(int column, int row);

    bool GetIsOccupied(string position);

    void SetIsOccupied(string position, bool isOccupied);

    bool GetIsInMap(string position);

    PipeType GetPipeType(string position);

    void SetPipeType(string position, PipeType pipeType);

    Vector2 GetPosition(string position);
  }
}
