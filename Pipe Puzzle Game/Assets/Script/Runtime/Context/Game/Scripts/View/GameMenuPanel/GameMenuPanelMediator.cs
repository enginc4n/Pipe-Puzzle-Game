using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.GameMenuPanel
{
  public class GameMenuPanelMediator : EventMediator
  {
    [Inject]
    public GameMenuPanelView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvents.GameMenuPanelOpened, OnGameStarted);
    }

    private void OnGameStarted()
    {
      view.ToggleGameMenuPanel(true);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.GameMenuPanelOpened, OnGameStarted);
    }
  }
}
