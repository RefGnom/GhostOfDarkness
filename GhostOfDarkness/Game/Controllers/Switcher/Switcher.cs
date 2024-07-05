using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Game.ContentLoaders;
using Game.Controllers.Buttons;
using Game.Enums;
using Game.Graphics;
using Game.Service;
using Game.View;
using Microsoft.Xna.Framework;
using IComponent = Game.Interfaces.IComponent;

namespace Game.Controllers.Switcher;

internal class Switcher : IComponent, IEnumerable<string>
{
    private readonly Vector2 backgroundPosition;
    private readonly Button leftArrow;
    private readonly Button rightArrow;
    private readonly Sprite background;
    private readonly List<SwitcherObject> options;
    private int currentOption;

    public Switcher(Vector2 position)
    {
        backgroundPosition = position.Shift(50, 0);
        leftArrow = new Button(Textures.SwitcherLeftArrow, position, Color.Black);
        leftArrow.OnClicked += Back;
        rightArrow = new Button(Textures.SwitcherRightArrow, position.Shift(360, 0), Color.Black);
        rightArrow.OnClicked += Next;
        background = new Sprite(Textures.SwitcherBackground, backgroundPosition, Layers.UiBackground);
        options = [];
    }

    public void Add(string value, Action onActivated = null, Action onDeactivated = null)
    {
        var option = new SwitcherObject(new Rectangle((int)backgroundPosition.X, (int)backgroundPosition.Y, 300, 40), value, Align.Left | Align.Right, 0, Fonts.Common16);
        option.OnActivated += onActivated;
        option.OnDeactivated += onDeactivated;
        options.Add(option);
    }

    public void Remove(string value)
    {
        for (var i = 0; i < options.Count; i++)
        {
            if (value == options[i].Value)
            {
                options.RemoveAt(i);
                break;
            }
        }
    }

    public void Start()
    {
        options[currentOption].SetActive(true);
    }

    public void Update(float deltaTime)
    {
        leftArrow.Update(deltaTime);
        rightArrow.Update(deltaTime);
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        leftArrow.Draw(spriteBatch, scale);
        rightArrow.Draw(spriteBatch, scale);
        background.Draw(spriteBatch, scale);
        options[currentOption].Draw(spriteBatch, scale);
    }

    private void Next()
    {
        Move(() => currentOption = (currentOption + 1) % options.Count);
    }

    private void Back()
    {
        Move(() => currentOption = (currentOption - 1 + options.Count) % options.Count);
    }

    private void Move(Action changeIndex)
    {
        options[currentOption].SetActive(false);
        changeIndex();
        options[currentOption].SetActive(true);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return options.Select(option => option.Value).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}