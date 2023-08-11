using System.Collections.Generic;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Script.Runtime.Context.Game.Scripts.View.GameBoard
{
  public class GameBoardView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    [Header("Settings")]
    [SerializeField]
    private int column;

    [SerializeField]
    private int row;

    [SerializeField]
    private Vector2 spacing;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject startPipePrefab;

    [SerializeField]
    private GameObject endPipePrefab;

    [Header("Item Slot Color")]
    [SerializeField]
    private Color offsetColor;

    [SerializeField]
    private Color normalColor;

    private Rect _gameBoardRect;
    private GridLayoutGroup _gridLayoutGroup;

    protected override void Awake()
    {
      base.Awake();
      _gameBoardRect = GetComponent<RectTransform>().rect;
      _gridLayoutGroup = container.GetComponent<GridLayoutGroup>();
    }

    protected override void Start()
    {
      base.Start();

      SetGridLayoutGroup();
    }

    private void SetGridLayoutGroup()
    {
      _gridLayoutGroup.spacing = spacing;
      _gridLayoutGroup.cellSize = CalculateItemSlotSize();

      if (column > row)
      {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = row;
      }
      else
      {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _gridLayoutGroup.constraintCount = column;
      }
    }

    public Vector2 CalculateItemSlotSize()
    {
      float availableWidth = _gameBoardRect.width - (row - 1) * spacing.x;
      float availableHeight = _gameBoardRect.height - (column - 1) * spacing.y;

      float itemSlotWidth = availableWidth / row;
      float itemSlotHeight = availableHeight / column;

      Vector2 itemSlotSize;
      itemSlotSize.x = Mathf.Min(itemSlotWidth, itemSlotHeight);
      itemSlotSize.y = Mathf.Min(itemSlotWidth, itemSlotHeight);

      return itemSlotSize;
    }

    private IPromise CreateItemSlots(int x, int y)
    {
      Promise promise = new();
      AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.InstantiateAsync("ItemSlot", container.transform);
      asyncOperationHandle.Completed += handle =>
      {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
          GameObject itemSlot = handle.Result;
          itemSlot.name = $"{x},{y}";
          bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
          Image itemSlotImage = itemSlot.GetComponent<Image>();
          itemSlotImage.color = isOffset ? offsetColor : normalColor;
          promise.Resolve();
        }
        else
        {
          promise.Reject(asyncOperationHandle.OperationException);
        }
      };

      return promise;
    }

    public void CreateStarterPipes()
    {
      Transform startPipeTransform = container.transform.GetChild(0);
      GameObject startPipe = Instantiate(startPipePrefab, startPipeTransform);
      startPipe.name = "StartPipe";
      dispatcher.Dispatch(GameBoardEvents.StarterPipesCreated, startPipe);
    }

    public void CreateEndPipes()
    {
      Transform endPipeTransform = container.transform.GetChild(container.transform.childCount - 1);
      GameObject endPipe = Instantiate(endPipePrefab, endPipeTransform);
      endPipe.name = "EndPipe";
      endPipe.transform.Rotate(0, 0, 180);
      dispatcher.Dispatch(GameBoardEvents.StarterPipesCreated, endPipe);
    }

    public void CreateGameBoard()
    {
      List<IPromise> promises = new();
      for (int x = 0; x < column; x++)
      {
        for (int y = 0; y < row; y++)
        {
          promises.Add(CreateItemSlots(x, y));
        }
      }

      Promise.All(promises).Then(() =>
      {
        CreateStarterPipes();
        CreateEndPipes();
      });
    }

    public int GetRow()
    {
      return row;
    }

    public int GetColumn()
    {
      return column;
    }
  }
}
