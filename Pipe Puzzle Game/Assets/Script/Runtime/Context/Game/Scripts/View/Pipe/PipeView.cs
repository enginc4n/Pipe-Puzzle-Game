using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Runtime.Context.Game.Scripts.View.Pipe
{
  public class PipeView : EventView, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
  {
    [Header("Settings For Draggable Objects")]
    [SerializeField]
    private float alpha;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;

    private Transform _beginParent;
    private Transform _endParent;

    protected override void Awake()
    {
      base.Awake();
      _rectTransform = GetComponent<RectTransform>();
      _canvasGroup = GetComponentInChildren<CanvasGroup>();
      _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      _canvasGroup.alpha = alpha;
      _canvasGroup.blocksRaycasts = false;
      _beginParent = transform.parent;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      _canvasGroup.alpha = 1f;
      _canvasGroup.blocksRaycasts = true;
      _rectTransform.anchoredPosition = Vector2.zero;
      _endParent = transform.parent;

      if (_beginParent != _endParent)
      {
        dispatcher.Dispatch(DraggableObjectsEvents.ObjectPositionChanged, _beginParent);
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      transform.Rotate(0f, 0f, 90f);
    }
  }
}
