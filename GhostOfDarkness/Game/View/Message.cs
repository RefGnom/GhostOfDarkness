using System;
using System.Collections.Generic;
using Core.DependencyInjection;
using Game.ContentLoaders;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.View;

[DiIgnore]
internal class Message : IDrawable
{
    private readonly int width = 400;
    private readonly int height = 35;

    private int currentChoiceIndex;
    private readonly bool getNextFromChoice;
    private readonly bool getOnNextFromChoice;

    private readonly Text text;
    private readonly List<Message> choices;

    public SpriteFont Font
    {
        get => text.Font;
        set => text.Font = value;
    }

    public Message Next { get; set; }
    public Action OnNext { get; set; }

    public Message(string text, bool getNextFromChoice = true, bool getOnNextFromChoice = true, int numberChoice = 0)
    {
        this.text = new Text(GetBounds(numberChoice), text, Fonts.Common16);
        this.getNextFromChoice = getNextFromChoice;
        this.getOnNextFromChoice = getOnNextFromChoice;
    }

    public Message(string text, List<Message> choices, bool getNextFromChoice = true, bool getOnNextFromChoice = true)
        : this(text, getNextFromChoice, getOnNextFromChoice)
    {
        this.choices = choices;
        TurnOnChoice(currentChoiceIndex);
    }

    private Rectangle GetBounds(int numberChoice)
    {
        if (numberChoice == 0)
        {
            return new Rectangle(1920 / 2 - width / 2, 1000 - height, width, height);
        }

        var top = 1000 - height * 3;
        for (var i = 1; i <= numberChoice; i++)
        {
            if (numberChoice == i)
            {
                return new Rectangle(1920 / 2 + width / 2, top, width, height);
            }

            top -= height;
        }

        throw new Exception();
    }

    public void MoveNextChoice()
    {
        if (IsCorrect(currentChoiceIndex + 1))
        {
            TurnOffChoice(currentChoiceIndex++);
            TurnOnChoice(currentChoiceIndex);
        }
    }

    public void MoveBackChoice()
    {
        if (IsCorrect(currentChoiceIndex - 1))
        {
            TurnOffChoice(currentChoiceIndex--);
            TurnOnChoice(currentChoiceIndex);
        }
    }

    private bool IsCorrect(int index) => choices is not null
                                         && choices.Count > 0
                                         && index < choices.Count
                                         && index >= 0;

    private void TurnOffChoice(int index)
    {
        choices[index].Font = Fonts.Common16;
    }

    private void TurnOnChoice(int index)
    {
        if (getNextFromChoice)
        {
            Next = choices[index].Next;
        }

        if (getOnNextFromChoice)
        {
            OnNext = choices[index].OnNext;
        }

        choices[currentChoiceIndex].Font = Fonts.Common18;
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        text.Draw(spriteBatch, scale);
        if (choices is not null)
        {
            foreach (var choice in choices)
            {
                choice.Draw(spriteBatch, scale);
            }
        }
    }
}