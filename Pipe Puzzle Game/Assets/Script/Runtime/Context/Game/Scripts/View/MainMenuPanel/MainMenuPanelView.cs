using strange.extensions.mediation.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.View.MainMenuPanel
{
  public class MainMenuPanelView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    public void ToggleMainMenuPanel(bool isActive)
    {
      container.SetActive(isActive);
    }

    public void OnPlayButtonClicked()
    {
      dispatcher.Dispatch(MainMenuEvent.Play);
    }
  }
}
