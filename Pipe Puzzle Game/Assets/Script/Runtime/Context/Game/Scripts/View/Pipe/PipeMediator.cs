using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe
{
  public enum PipeEvents
  {
    PipePositionChanged
  }

  public class PipeMediator : EventMediator
  {
    [Inject]
    public PipeView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(PipeEvents.PipePositionChanged, OnObjectPositionChanged);
    }

    private void OnObjectPositionChanged(IEvent evt)
    {
      Transform oldParent = evt.data as Transform;

      dispatcher.Dispatch(GameEvents.ObjectPositionChanged, oldParent);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(PipeEvents.PipePositionChanged, OnObjectPositionChanged);
    }
  }
}
