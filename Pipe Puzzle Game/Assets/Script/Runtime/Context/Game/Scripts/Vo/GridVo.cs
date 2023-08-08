using System;
using Script.Runtime.Context.Game.Scripts.Enums;

namespace Script.Runtime.Context.Game.Scripts.Vo
{
  [Serializable]
  public class GridVo
  {
    public string position;

    public PipeType pipeType;

    public bool isOccupied;
  }
}
