using Script.Runtime.Context.Game.Scripts.Enums;
using Script.Runtime.Context.Game.Scripts.Models.Grid;
using Script.Runtime.Context.Game.Scripts.View.Pipe.ConnectionPipe;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe.EndPipe
{
  public enum EndPipeEvents
  {
    HitPipe
  }

  public class EndPipeMediator : EventMediator
  {
    [Inject]
    public EndPipeView view { get; set; }

    [Inject]
    public IGridModel gridModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(EndPipeEvents.HitPipe, OnHitPipe);

      dispatcher.AddListener(PipeEvents.PipeMoved, OnSendRay);
      dispatcher.AddListener(PipeEvents.PipeRotated, OnSendRay);
    }

    private void OnHitPipe(IEvent evt)
    {
      GameObject hitPipe = evt.data as GameObject;
      if (hitPipe == null)
      {
        return;
      }

      ConnectionPipeView connectionPipeView = hitPipe.GetComponent<ConnectionPipeView>();
      if (connectionPipeView == null)
      {
        return;
      }

      string hitPipePos = connectionPipeView.GetPosition();
      bool isHaveWater = gridModel.GetIsHaveWater(hitPipePos);
      if (isHaveWater)
      {
        dispatcher.Dispatch(GameEvents.GameFinished);
      }
    }

    private void OnSendRay()
    {
      view.CheckIsTouched();
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(EndPipeEvents.HitPipe, OnHitPipe);

      dispatcher.RemoveListener(PipeEvents.PipeMoved, OnSendRay);
      dispatcher.RemoveListener(PipeEvents.PipeRotated, OnSendRay);
    }
  }
}
