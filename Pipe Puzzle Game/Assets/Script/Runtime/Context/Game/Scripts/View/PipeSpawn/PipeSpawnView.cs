using System;
using System.Collections.Generic;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.mediation.impl;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Runtime.Context.Game.Scripts.View.PipeSpawn
{
  public class PipeSpawnView : EventView
  {
    [Header("Pipe Spawn Settings")]
    [SerializeField]
    private int pipeSpawnCount = 3;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject spawnSlotPrefab;

    [SerializeField]
    private List<GameObject> pipePrefab;

    public IPromise<GameObject> CreateSpawnSlot()
    {
      Promise<GameObject> promise = new();

      GameObject spawnSlot = Instantiate(spawnSlotPrefab, transform);

      if (spawnSlot != null)
      {
        promise.Resolve(spawnSlot);
      }
      else
      {
        promise.Reject(new SystemException("Spawn slot is not created properly."));
      }

      return promise;
    }

    public IPromise CreateRandomPipe(Transform parent)
    {
      Promise promise = new();
      int randomNumber = Random.Range(0, pipePrefab.Count);
      GameObject pipe = Instantiate(pipePrefab[randomNumber], parent);

      if (pipe != null)
      {
        promise.Resolve();
      }
      else
      {
        promise.Reject(new SystemException("Pipe is not created properly."));
      }

      return promise;
    }

    public int GetPipeSpawnCount()
    {
      return pipeSpawnCount;
    }
  }
}
