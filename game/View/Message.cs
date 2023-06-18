using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game;

internal class Message : IDrawable
{
    private readonly int width = 400;
    private readonly int height = 35;

    private int currentChoiceIndex;
    private readonly bool getNextFromChoice;
    private readonly bool getOnNextFromChoice;

    public readonly Text Text;
    public readonly List<Message> Choices;
    public SpriteFont Font {
        get => Text.Font;
        set => Text.Font = value;
    }
    public Message Next { get; set; }
    public Action OnNext { get; set; }

    public Message(string text, bool getNextFromChoice = true, bool getOnNextFromChoice = true, int numberChoice = 0)
    {
        Text = new Text(GetBounds(numberChoice), text, Align.Center, 0, Fonts.Common16);
        this.getNextFromChoice = getNextFromChoice;
        this.getOnNextFromChoice = getOnNextFromChoice;
    }

    public Message(string text, List<Message> choices, bool getNextFromChoice = true, bool getOnNextFromChoice = true)
        : this(text, getNextFromChoice, getOnNextFromChoice)
    {
        Choices = choices;
        TurnOnChoice(currentChoiceIndex);
    }

    private Rectangle GetBounds(int numberChoice)
    {
        if (numberChoice == 0)
            return new Rectangle(1920 / 2 - width / 2, 1000 - height, width, height);
        var top = 1000 - height * 3;
        for (int i = 1; i <= numberChoice; i++)
        {
            if (numberChoice == i)
                return new Rectangle(1920 / 2 + width / 2, top, width, height);
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

    private bool IsCorrect(int index)
    {
        return Choices is not null
            && Choices.Count > 0
            && index < Choices.Count
            && index >= 0;
    }

    private void TurnOffChoice(int index)
    {
        Choices[index].Font = Fonts.Common16;
    }

    private void TurnOnChoice(int index)
    {
        if (getNextFromChoice)
            Next = Choices[index].Next;
        if (getOnNextFromChoice)
            OnNext = Choices[index].OnNext;
        Choices[currentChoiceIndex].Font = Fonts.Common18;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        Text.Draw(spriteBatch, scale);
        if (Choices is not null)
        {
            for (int i = 0; i < Choices.Count; i++)
                Choices[i].Draw(spriteBatch, scale);
        }
    }
}