using strange.extensions.mediation.impl;

namespace Script.Runtime.Context.Game.Scripts.View.Background_Scroller
{
  public class BackgroundScrollerMediator : EventMediator
  {
    [Inject]
    public BackgroundScrollerView view { get; set; }

    public override void OnRegister()
    {
    }

    public override void OnRemove()
    {
    }
  }
}
