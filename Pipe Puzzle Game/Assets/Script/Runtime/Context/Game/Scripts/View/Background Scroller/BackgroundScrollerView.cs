using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.Context.Game.Scripts.View.Background_Scroller
{
  public class BackgroundScrollerView : EventView
  {
    private RawImage _rawImage;

    [Header("Settings For Scroller")]
    [SerializeField]
    private float scrollSpeed;

    protected override void Awake()
    {
      base.Awake();
      _rawImage = GetComponentInChildren<RawImage>();
    }

    private void Update()
    {
      Scroll();
    }

    private void Scroll()
    {
      Vector2 rawImagePosition = _rawImage.uvRect.position;
      Vector2 scrollingSpeed = new Vector2(scrollSpeed, scrollSpeed) * Time.deltaTime;
      _rawImage.uvRect = new Rect(rawImagePosition + scrollingSpeed, _rawImage.uvRect.size);
    }
  }
}
