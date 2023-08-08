using strange.extensions.mediation.impl;
using UnityEngine;
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

    [Header("Item Slot")]
    [SerializeField]
    private GameObject itemSlotPrefab;

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

    public void SpawnItemSlots()
    {
      for (int x = 0; x < column; x++)
      {
        for (int y = 0; y < row; y++)
        {
          GameObject itemSlot = Instantiate(itemSlotPrefab, container.transform);
          itemSlot.name = $"{x},{y}";

          bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

          Color offsetColor = new(0.5f, 0.5f, 0.5f, 0.25f);
          Color normalColor = new(1f, 1f, 1f, 0.25f);
          Image itemSlotImage = itemSlot.GetComponent<Image>();
          itemSlotImage.color = isOffset ? offsetColor : normalColor;
        }
      }
    }

    private Vector2 CalculateItemSlotSize()
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
