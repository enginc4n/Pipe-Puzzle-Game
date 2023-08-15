using System.Collections.Generic;
using Scripts.Runtime.Modules.Core.PromiseTool;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe.ConnectionPipe
{
  public class ConnectionPipeView : EventView, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
  {
    [Header("Settings For Draggable Objects")]
    [SerializeField]
    private float alpha;

    [SerializeField]
    private List<Transform> connections;

    [HideInInspector]
    public bool rotatable;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;

    private Transform _beginParent;
    private Transform _endParent;

    protected override void Awake()
    {
      base.Awake();
      _rectTransform = GetComponent<RectTransform>();
      _canvasGroup = GetComponent<CanvasGroup>();
      _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      _canvasGroup.alpha = alpha;
      _canvasGroup.blocksRaycasts = false;
      _beginParent = transform.parent;
      gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      _canvasGroup.alpha = 1f;
      _canvasGroup.blocksRaycasts = true;
      _rectTransform.anchoredPosition = Vector2.zero;
      _endParent = transform.parent;
      gameObject.layer = LayerMask.NameToLayer("Pipe");

      if (_beginParent != _endParent)
      {
        dispatcher.Dispatch(ConnectionPipeEvents.PipeMoved, _beginParent);
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (!rotatable)
      {
        return;
      }

      transform.Rotate(0f, 0f, 90f);
      dispatcher.Dispatch(ConnectionPipeEvents.PipeRotated);
    }

    public IPromise SendRay()
    {
      int targetMask = LayerMask.NameToLayer("Pipe");

      float distance = _rectTransform.rect.height / 4f;

      int connectionsCount = connections.Count;
      int emptyHitCount = 0;

      foreach (Transform connect in connections)
      {
        Vector3 direction = connect.transform.up;
        Vector3 startPoint = connect.position + direction * distance;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, distance, -targetMask); // burada neden eksi yazıyorum anlamadım eksi olmadan ters çalışıyor.
        Debug.DrawLine(startPoint, startPoint + direction * distance, Color.yellow, 1f);

        if (!hit)
        {
          emptyHitCount++;

          if (emptyHitCount >= connectionsCount)
          {
            dispatcher.Dispatch(ConnectionPipeEvents.PipeDisconnected);
          }

          continue;
        }

        GameObject hitPipe = hit.transform.gameObject;

        if (hitPipe == gameObject)
        {
          continue;
        }

        dispatcher.Dispatch(ConnectionPipeEvents.PipeConnected, hitPipe);
      }

      return
        Promise.Resolved();
    }

    public void ChangePipeColor(bool isHaveWater)
    {
      Image image = gameObject.GetComponent<Image>();
      image.color = isHaveWater ? Color.blue : Color.white;
    }

    public string GetPosition()
    {
      return transform.parent.name;
    }
  }
}
