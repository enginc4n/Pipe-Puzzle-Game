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
    PipeTouched,
    PipeNotTouched,
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
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeTouched, OnPipeTouched);
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeNotTouched, OnPipeNotTouched);
      view.dispatcher.AddListener(ConnectionPipeEvents.PipeConnected, OnPipeConnected);
    }

    private void OnPipeConnected(IEvent evt)
    {
      GameObject originalPipe = evt.data as GameObject;
      if (originalPipe == null)
      {
        return;
      }

      ConnectionPipeView connectionPipeView = originalPipe.GetComponent<ConnectionPipeView>();
      if (connectionPipeView == null)
      {
        Debug.LogError("Connection Pipe View not Found");
        return;
      }

      string originalPos = connectionPipeView.GetPosition();
      bool isHaveWaterInOriginalPipe = gridModel.GetIsHaveWater(originalPos);

      if (!isHaveWaterInOriginalPipe)
      {
        string hitPipePos = view.GetPosition();
        bool isHaveWaterInHitPipe = gridModel.GetIsHaveWater(hitPipePos);
        gridModel.SetIsHaveWater(originalPos, isHaveWaterInHitPipe);
        connectionPipeView.ChangePipeColor(isHaveWaterInHitPipe);
      }
      else
      {
        string hitPipePos = view.GetPosition();
        bool isHaveWater = gridModel.GetIsHaveWater(originalPos);
        gridModel.SetIsHaveWater(hitPipePos, isHaveWater);
        view.ChangePipeColor(isHaveWater);
      }
    }

    private void OnPipeNotTouched()
    {
      string position = view.GetPosition();
      gridModel.SetIsHaveWater(position, false);
      ChangeColorByWater(position);
    }

    private void OnPipeTouched(IEvent evt)
    {
      GameObject hitPipe = evt.data as GameObject;
      if (hitPipe == null)
      {
        return;
      }

      ConnectionPipeView connectionPipeView = hitPipe.GetComponent<ConnectionPipeView>();
      if (connectionPipeView == null)
      {
        Debug.LogError("Null geldi connection Pipe view OnPipeTouch'da");
        return;
      }

      connectionPipeView.CheckIsTouched(gameObject);
    }

    private void OnPipeRotated()
    {
      ConnectionPipeView[] connectionPipeViews = FindObjectsOfType<ConnectionPipeView>();
      foreach (ConnectionPipeView pipeView in connectionPipeViews)
      {
        if (!pipeView.rotatable)
        {
          continue;
        }

        string position = pipeView.GetPosition();
        pipeView.CheckIsTouching()
          .Then(() =>
          {
            ChangeColorByWater(position);
            dispatcher.Dispatch(PipeEvents.PipeRotated);
          })
          .Catch(exception =>
          {
            Debug.LogWarning("Ex: " + exception);
          });
      }
    }

    private void OnPipeMoved(IEvent evt)
    {
      string position = view.GetPosition();
      view.CheckIsTouching()
        .Then(() =>
        {
          Transform oldParent = evt.data as Transform;
          dispatcher.Dispatch(PipeEvents.PipeMoved, oldParent);

          ChangeColorByWater(position);
        })
        .Catch(exception =>
        {
          Debug.LogWarning("Ex: " + exception);
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
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeTouched, OnPipeTouched);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeNotTouched, OnPipeNotTouched);
      view.dispatcher.RemoveListener(ConnectionPipeEvents.PipeConnected, OnPipeConnected);
    }
  }
}
