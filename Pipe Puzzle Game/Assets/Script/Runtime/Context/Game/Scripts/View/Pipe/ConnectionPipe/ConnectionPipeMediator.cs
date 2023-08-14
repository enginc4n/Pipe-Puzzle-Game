using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe.ConnectionPipe
{
  public enum ConnectionPipeEvents
  {
    PipeMoved,
    PipeRotated,
    PipeConnected
  }

  public class ConnectionPipeMediator : EventMediator
  {
    [Inject]
    public ConnectionPipeView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeMoved, OnPipeMoved);
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeRotated, OnPipeRotated);
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeConnected, OnPipeConnected);
    }

    private void OnPipeConnected(IEvent evt)
    {
      GameObject hitPipe = evt.data as GameObject;
      if (hitPipe == null)
      {
        return;
      }

      if (hitPipe.transform.CompareTag("Start"))
      {
        Debug.LogError("Get WATER");
      }

      ConnectionPipeView connectionPipeView = hitPipe.GetComponent<ConnectionPipeView>();

      if (connectionPipeView != null)
      {
        connectionPipeView.SendRay();
      }
    }

    private void OnPipeRotated()
    {
      dispatcher.Dispatch(PipeEvents.PipeRotated);
    }

    private void OnPipeMoved(IEvent evt)
    {
      Transform oldParent = evt.data as Transform;
      dispatcher.Dispatch(PipeEvents.PipeMoved, oldParent);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeMoved, OnPipeMoved);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeRotated, OnPipeRotated);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeConnected, OnPipeConnected);
    }
  }
}
