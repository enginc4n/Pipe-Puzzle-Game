using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using Script.Runtime.Context.Game.Scripts.View.Pipe;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.GameBoard
{
  public enum GameBoardEvents
  {
    StarterPipesCreated
  }

  public class GameBoardMediator : EventMediator
  {
    [Inject]
    public GameBoardView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(GameBoardEvents.StarterPipesCreated, OnStarterPipeCreated);

      dispatcher.AddListener(GameEvents.GameMenuPanelOpened, OnGameMenuPanelOpened);
    }

    private void OnStarterPipeCreated(IEvent evt)
    {
      GameObject pipe = evt.data as GameObject;

      string position = pipe.transform.parent.name;
      gridModel.SetIsOccupied(position, true);

      PipeView pipeView = pipe.GetComponent<PipeView>();
      gridModel.SetPipeType(position, pipeView.pipeType);

      RectTransform pipeRectTransform = pipe.GetComponent<RectTransform>();
      pipeRectTransform.sizeDelta = view.CalculateItemSlotSize();

      BoxCollider2D pipeBoxCollider2D = pipe.GetComponent<BoxCollider2D>();
      pipeBoxCollider2D.size = view.CalculateItemSlotSize();
    }

    private void OnGameMenuPanelOpened()
    {
      int row = view.GetRow();
      int column = view.GetColumn();
      gridModel.CreateData(column, row);

      view.CreateGameBoard();
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.GameMenuPanelOpened, OnGameMenuPanelOpened);
    }
  }
}
