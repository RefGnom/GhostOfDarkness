using System;
using Game.Controllers.InputServices;
using Microsoft.Xna.Framework;
using IUpdateable = Game.Interfaces.IUpdateable;

namespace Game.Controllers;

public class ScrollBounds : IUpdateable
{
    private readonly IMouseService mouseService;
    private readonly Rectangle scrollBounds;

    public Action<int> OnScroll;
    private float scale = 1;

    public ScrollBounds(IMouseService mouseService, Rectangle scrollBounds)
    {
        this.mouseService = mouseService;
        this.scrollBounds = scrollBounds;
        OnScroll = _ => { };
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
    }

    public void Update(float deltaTime)
    {
        var mousePosition = mouseService.GetWindowPosition() / scale;

        // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
        if (scrollBounds.Contains(mousePosition))
        {
            var scrollValue = mouseService.GetScrollValue();
            if (scrollValue != 0)
            {
                OnScroll(scrollValue);
            }
        }
    }
}