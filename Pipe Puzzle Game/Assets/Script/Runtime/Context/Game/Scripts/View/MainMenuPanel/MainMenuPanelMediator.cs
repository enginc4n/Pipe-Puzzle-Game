using Script.Runtime.Context.Game.Scripts.Models.Game;
using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.MainMenuPanel
{
  public enum MainMenuEvent
  {
    Play
  }

  public class MainMenuPanelMediator : EventMediator
  {
    [Inject]
    public MainMenuPanelView view { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(MainMenuEvent.Play, OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
      view.ToggleMainMenuPanel(false);
      gameModel.StartGame();
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(MainMenuEvent.Play, OnPlayButtonClicked);
    }
  }
}
