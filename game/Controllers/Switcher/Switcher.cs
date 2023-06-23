using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace game;

internal class Switcher : IComponent, IEnumerable<string>
{
    private readonly Vector2 leftArrowPosition;
    private readonly Vector2 rightArrowPosition;
    private readonly Vector2 backgroundPosition;
    private readonly Button leftArrow;
    private readonly Button rightArrow;
    private readonly Sprite background;
    private readonly List<SwitcherObject> options;
    private int currentOption;

    public Switcher(Vector2 position)
    {
        leftArrowPosition = position;
        rightArrowPosition = position.Shift(360, 0);
        backgroundPosition = position.Shift(50, 0);
        leftArrow = new Button(Textures.SwitcherLeftArrow, leftArrowPosition, "");
        leftArrow.OnClicked += Back;
        rightArrow = new Button(Textures.SwitcherRightArrow, rightArrowPosition, "");
        rightArrow.OnClicked += Next;
        background = new Sprite(Textures.SwitcherBackground, backgroundPosition, Layers.UIBackground);
        options = new();
    }

    public void Add(string value, Action onActivated = null, Action onDeactivated = null)
    {
        var option = new SwitcherObject(new Rectangle((int)backgroundPosition.X, (int)backgroundPosition.Y, 300, 40), value, Align.Center, 0, Fonts.Common16);
        option.OnActivated += onActivated;
        option.OnDeactivated += onDeactivated;
        options.Add(option);
    }

    public void Remove(string value)
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (value == options[i].Value)
            {
                options.RemoveAt(i);
                break;
            }
        }
    }

    public void Update(float deltaTime)
    {
        if (!options[currentOption].Active)
            options[currentOption].SetActive(true);
        leftArrow.Update(deltaTime);
        rightArrow.Update(deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
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
        for (int i = 0; i < options.Count; i++)
        {
            yield return options[i].Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}