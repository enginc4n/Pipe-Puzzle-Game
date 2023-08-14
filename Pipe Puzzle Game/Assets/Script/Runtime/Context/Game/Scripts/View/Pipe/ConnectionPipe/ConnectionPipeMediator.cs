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
    PipeConnected,
    PipeDisconnected
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
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeDisconnected, OnPipeDisconnected);
    }

    private void OnPipeDisconnected()
    {
      string position = view.GetPosition();
      gridModel.SetIsHaveWater(position, false);

      ChangeColorByWater(position);
    }

    private void OnPipeConnected(IEvent evt)
    {
      GameObject hitPipe = evt.data as GameObject;
      if (hitPipe == null)
      {
        return;
      }

      string position = view.GetPosition();
      string hitPipePosition = hitPipe.transform.parent.name;
      bool isHaveWater = gridModel.GetIsHaveWater(hitPipePosition);
      view.ChangePipeColor(isHaveWater);
      gridModel.SetIsHaveWater(position, isHaveWater);
      if (hitPipe.CompareTag("Finish"))
      {
        CheckWin();
      }
    }

    private void CheckWin()
    {
      Debug.LogError("CheckWin");
      string position = view.GetPosition();
      bool isHaveWater = gridModel.GetIsHaveWater(position);
      if (!isHaveWater)
      {
        return;
      }

      Debug.LogError("Win");
      dispatcher.Dispatch(GameEvents.GameFinished);
    }

    private void OnPipeRotated()
    {
      view.SendRay().Then(() =>
      {
        string position = view.GetPosition();
        ChangeColorByWater(position);
      });
    }

    private void OnPipeMoved(IEvent evt)
    {
      view.SendRay().Then(() =>
      {
        string position = view.GetPosition();
        ChangeColorByWater(position);
        Transform oldParent = evt.data as Transform;
        dispatcher.Dispatch(PipeEvents.PipeMoved, oldParent);
      });
    }

    private void ChangeColorByWater(string pos)
    {
      bool isHaveWater = gridModel.GetIsHaveWater(pos);
      view.ChangePipeColor(isHaveWater);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeMoved, OnPipeMoved);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeRotated, OnPipeRotated);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeConnected, OnPipeConnected);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeDisconnected, OnPipeConnected);
    }
  }
}
