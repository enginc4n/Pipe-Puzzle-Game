using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.GameMenuPanel
{
  public class GameMenuPanelView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    public void ToggleGameMenuPanel(bool isActive)
    {
      container.SetActive(isActive);
    }
  }
}
