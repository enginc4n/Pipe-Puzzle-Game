using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe.EndPipe
{
  public class EndPipeView : EventView
  {
    [SerializeField]
    private List<Transform> connections;

    private RectTransform _rectTransform;

    private Image _image;

    protected override void Awake()
    {
      base.Awake();
      _rectTransform = GetComponent<RectTransform>();
      _image = GetComponent<Image>();
    }

    public void CheckIsTouched()
    {
      int targetMask = LayerMask.NameToLayer("Pipe");

      float distance = _rectTransform.rect.height / 4f;

      int connectionCount = connections.Count;
      if (connectionCount == 0)
      {
        Debug.LogWarning("Connections Are EMPTY");
      }

      foreach (Transform connect in connections)
      {
        Vector3 direction = connect.transform.up;
        Vector3 startPoint = connect.position + direction * distance;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, distance, -targetMask); // burada neden eksi yazıyorum anlamadım eksi olmadan ters çalışıyor.
        Debug.DrawLine(startPoint, startPoint + direction * distance, Color.yellow, 1f);

        if (!hit)
        {
          continue;
        }

        GameObject hitPipe = hit.transform.gameObject;

        dispatcher.Dispatch(EndPipeEvents.HitPipe, hitPipe);
      }
    }

    public void ChangeColorToBlue()
    {
      _image.color = Color.blue;
    }
  }
}
