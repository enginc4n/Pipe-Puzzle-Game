using Script.Runtime.Context.Game.Scripts.Enums;
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

    public PipeType pipeType;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;

    private Transform _beginParent;
    private Transform _endParent;
    private float _width;

    private void FixedUpdate()
    {
      SendRays();
    }

    private void SendRays()
    {
      switch (pipeType)
      {
        case PipeType.Start:
          ProcessStartPipe();
          break;
      }
    }

    private void ProcessStartPipe()
    {
      Vector2 start = transform.position + transform.up * -_width;
      Vector2 direction = -transform.up;
      RaycastHit2D hit = Physics2D.Raycast(start, direction, _width);

      if (!hit)
      {
        return;
      }

      if (hit.transform.CompareTag("Pipe"))
      {
          
      }
    }

    protected override void Awake()
    {
      base.Awake();
      _rectTransform = GetComponent<RectTransform>();
      _canvasGroup = GetComponent<CanvasGroup>();
      _canvas = GetComponentInParent<Canvas>();
    }

    protected override void Start()
    {
      base.Start();
      float _width = _rectTransform.rect.width / 4f;
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
        dispatcher.Dispatch(PipeEvents.PipePositionChanged, _beginParent);
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

    public float GetWidth()
    {
      return _rectTransform.rect.width;
    }
  }
}
