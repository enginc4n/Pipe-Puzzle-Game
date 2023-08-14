using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe.StartPipe
{
  public class StartPipeView : EventView
  {
    [Header("Settings For Start Pipe")]
    [SerializeField]
    private List<Transform> connections;

    private RectTransform _rectTransform;

    protected override void Awake()
    {
      base.Awake();
      _rectTransform = GetComponent<RectTransform>();
    }

    public void SendRay()
    {
      Debug.LogError("Ray Send");
      int targetMask = LayerMask.NameToLayer("Pipe");

      float distance = _rectTransform.rect.height / 4f;

      foreach (Transform connect in connections)
      {
        if (connections == null)
        {
          Debug.LogWarning("Connections are empty");
          return;
        }

        Vector3 direction = connect.transform.up;
        Vector3 startPoint = connect.position + direction * distance;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, distance, -targetMask); // burada neden eksi yazıyorum anlamadım eksi olmadan ters çalışıyor.

        if (!hit)
        {
          continue;
        }

        Debug.DrawLine(startPoint, startPoint + direction * distance, Color.red, 1f);

        GameObject hitPipe = hit.transform.gameObject;
        dispatcher.Dispatch(StartPipeEvents.PipeHit, hitPipe);
      }
    }
  }
}
