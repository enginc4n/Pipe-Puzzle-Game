using Script.Runtime.Context.Game.Scripts.Command;
using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Game;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using Script.Runtime.Context.Game.Scripts.View.Background_Scroller;
using Script.Runtime.Context.Game.Scripts.View.GameBoard;
using Script.Runtime.Context.Game.Scripts.View.GameMenuPanel;
using Script.Runtime.Context.Game.Scripts.View.ItemSlot;
using Script.Runtime.Context.Game.Scripts.View.MainMenuPanel;
using Script.Runtime.Context.Game.Scripts.View.Pipe.ConnectionPipe;
using Script.Runtime.Context.Game.Scripts.View.Pipe.EndPipe;
using Script.Runtime.Context.Game.Scripts.View.PipeSlot;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Config
{
  public class GameContext : MVCSContext
  {
    public GameContext(MonoBehaviour view) : base(view)
    {
    }

    public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();

      injectionBinder.Bind<IGridModel>().To<GridModel>().ToSingleton();
      injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();
      injectionBinder.Bind<GameControlFacade>().To<GameControlFacade>().ToSingleton();

      mediationBinder.Bind<ConnectionPipeView>().To<ConnectionPipeMediator>();
      mediationBinder.Bind<ItemSlotView>().To<ItemSlotMediator>();
      mediationBinder.Bind<BackgroundScrollerView>().To<BackgroundScrollerMediator>();
      mediationBinder.Bind<MainMenuPanelView>().To<MainMenuPanelMediator>();
      mediationBinder.Bind<GameMenuPanelView>().To<GameMenuPanelMediator>();
      mediationBinder.Bind<GameBoardView>().To<GameBoardMediator>();
      mediationBinder.Bind<PipeSlotView>().To<PipeSlotMediator>();
      mediationBinder.Bind<EndPipeView>().To<EndPipeMediator>();

      commandBinder.Bind(PipeEvents.PipeMoved).To<PipePositionChangedCommand>();
    }
  }
}
