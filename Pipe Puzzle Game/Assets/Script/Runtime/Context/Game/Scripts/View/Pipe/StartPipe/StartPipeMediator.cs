using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.View.Pipe.ConnectionPipe;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe.StartPipe
{
  public enum StartPipeEvents
  {
    PipeHit
  }

  public class StartPipeMediator : EventMediator
  {
    [Inject]
    public StartPipeView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(StartPipeEvents.PipeHit, OnPipeHit);

      dispatcher.AddListener(PipeEvents.PipeMoved, OnSendRay);
      dispatcher.AddListener(PipeEvents.PipeRotated, OnSendRay);
    }

    private void OnPipeHit(IEvent evt)
    {
      GameObject hitPipe = evt.data as GameObject;
      if (hitPipe == null)
      {
        return;
      }

      ConnectionPipeView connectionPipeView = hitPipe.GetComponent<ConnectionPipeView>();
      connectionPipeView.SendRay();
    }

    private void OnSendRay()
    {
      view.SendRay();
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(StartPipeEvents.PipeHit, OnPipeHit);

      dispatcher.RemoveListener(PipeEvents.PipeMoved, OnSendRay);
      dispatcher.RemoveListener(PipeEvents.PipeRotated, OnSendRay);
    }
  }
}
