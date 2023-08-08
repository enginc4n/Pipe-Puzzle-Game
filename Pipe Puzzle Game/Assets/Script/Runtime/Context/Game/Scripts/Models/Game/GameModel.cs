using Script.Runtime.Context.Game.Scripts.Enums;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace Script.Runtime.Context.Game.Scripts.Models.Game
{
  public class GameModel : IGameModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    public void StartGame()
    {
      dispatcher.Dispatch(GameEvents.GameMenuPanelOpened);
    }
  }
}
