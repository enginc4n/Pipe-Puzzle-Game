using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.GameBoard
{
  public class GameBoardMediator : EventMediator
  {
    [Inject]
    public GameBoardView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvents.GameMenuPanelOpened, OnGameMenuPanelOpened);
    }

    private void OnGameMenuPanelOpened()
    {
      int row = view.GetRow();
      int column = view.GetColumn();
      gridModel.CreateData(column, row).Then(() =>
      {
        view.SpawnItemSlots();
      });
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.GameMenuPanelOpened, OnGameMenuPanelOpened);
    }
  }
}
