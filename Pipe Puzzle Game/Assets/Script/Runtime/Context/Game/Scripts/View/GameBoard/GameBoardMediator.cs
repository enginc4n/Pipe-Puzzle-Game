using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
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
      if (pipe == null)
      {
        return;
      }

      string position = pipe.transform.parent.name;
      gridModel.SetIsOccupied(position, true);
      if (pipe.transform.CompareTag("Start"))
      {
        gridModel.SetIsHaveWater(position, true);
      }
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
