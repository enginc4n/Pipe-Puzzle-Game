using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Script.Runtime.Context.Game.Scripts.Config
{
  public class GameContext: MVCSContext
  {
    public GameContext(MonoBehaviour view) : base(view)
    {
    }

    public GameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();
    }
  }
}
