using System.Collections.Generic;
using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Vo;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Models.Grid
{
  public class GridModel : IGridModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private Dictionary<string, GridVo> _gridMap;

    [PostConstruct]
    public void OnPostConstruct()
    {
      Init();
    }

    private void Init()
    {
      _gridMap = new Dictionary<string, GridVo>();
    }

    public IPromise CreateData(int column, int row)
    {
      Promise promise = new();
      for (int x = 0; x < column; x++)
      {
        for (int y = 0; y < row; y++)
        {
          GridVo gridVo = new()
          {
            pipeType = PipeType.None,
            isOccupied = false,
            position = new Vector2(x, y)
          };
          _gridMap.Add($"{x},{y}", gridVo);
        }
      }

      if (_gridMap.Count == column * row)
      {
        promise.Resolve();
      }
      else
      {
        promise.Reject(new System.Exception("Grid data is not created properly."));
      }

      return promise;
    }

    public bool GetIsOccupied(string position)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return true;
      }

      GridVo gridVo = _gridMap[position];
      return gridVo.isOccupied;
    }

    public void SetIsOccupied(string position, bool isOccupied)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return;
      }

      _gridMap[position].isOccupied = isOccupied;
    }

    public bool GetIsInMap(string position)
    {
      return _gridMap.ContainsKey(position);
    }

    public void SetPipeType(string position, PipeType pipeType)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return;
      }

      _gridMap[position].pipeType = pipeType;
    }

    public PipeType GetPipeType(string position)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return PipeType.None;
      }

      return _gridMap[position].pipeType;
    }

    public Vector2 GetPosition(string position)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return Vector2.zero;
      }

      return _gridMap[position].position;
    }
  }
}
