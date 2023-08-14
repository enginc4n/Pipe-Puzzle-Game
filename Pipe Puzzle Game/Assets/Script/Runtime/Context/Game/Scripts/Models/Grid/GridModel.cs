using System.Collections.Generic;
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
            isOccupied = false,
            position = new Vector2(x, y),
            isHaveWater = false
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

    public void SetIsHaveWater(string position, bool isWater)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return;
      }

      _gridMap[position].isHaveWater = isWater;
    }

    public bool GetIsHaveWater(string position)
    {
      if (!_gridMap.ContainsKey(position))
      {
        return false;
      }

      return _gridMap[position].isHaveWater;
    }
  }
}
