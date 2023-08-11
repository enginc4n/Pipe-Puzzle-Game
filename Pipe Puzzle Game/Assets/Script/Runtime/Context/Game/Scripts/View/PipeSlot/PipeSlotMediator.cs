using Script.Runtime.Context.Game.Scripts.Enums;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.PipeSpawn
{
  public enum PipeSpawnEvents
  {
    SpawnSlotEmpty
  }

  public class PipeSlotMediator : EventMediator
  {
    [Inject]
    public PipeSlotView view { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(GameEvents.GameMenuPanelOpened, OnGameMenuPanelOpened);
      dispatcher.AddListener(PipeSpawnEvents.SpawnSlotEmpty, OnSpawnSlotEmpty);
    }

    private void OnSpawnSlotEmpty(IEvent evt)
    {
      Transform spawnSlot = evt.data as Transform;
      view.CreateRandomPipe(spawnSlot);
    }

    private void OnGameMenuPanelOpened()
    {
      int pipeSpawnCount = view.GetPipeSpawnCount();
      for (int i = 0; i < pipeSpawnCount; i++)
      {
        view.CreateSpawnSlot().Then(itemSlot =>
        {
          itemSlot.name = i.ToString();

          view.CreateRandomPipe(itemSlot.transform);
        }).Catch(exception =>
        {
          Debug.LogError("Exception: " + exception.Message);
        });
      }
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(GameEvents.GameMenuPanelOpened, OnGameMenuPanelOpened);
      dispatcher.RemoveListener(PipeSpawnEvents.SpawnSlotEmpty, OnSpawnSlotEmpty);
    }
  }
}
