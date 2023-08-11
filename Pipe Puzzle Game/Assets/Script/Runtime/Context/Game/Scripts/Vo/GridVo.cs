using System;
using Script.Runtime.Context.Game.Scripts.Enums;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Vo
{
  [Serializable]
  public class GridVo
  {
    public Vector2 position;

    public PipeType pipeType;

    public bool isOccupied;
  }
}
