using Script.Runtime.Context.Game.Scripts.Enums;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe
{
  public enum DraggableObjectsEvents
  {
    ObjectPositionChanged
  }

  public class PipeMediator : EventMediator
  {
    [Inject]
    public PipeView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(DraggableObjectsEvents.ObjectPositionChanged, OnObjectPositionChanged);
    }

    private void OnObjectPositionChanged(IEvent evt)
    {
      Transform oldParent = evt.data as Transform;
      dispatcher.Dispatch(GameEvents.ObjectPositionChanged, oldParent);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(DraggableObjectsEvents.ObjectPositionChanged, OnObjectPositionChanged);
    }
  }
}
