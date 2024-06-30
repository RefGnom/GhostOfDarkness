﻿namespace Game.Controllers.Buttons;

public class RadioButtonManager : IRadioButtonManager
{
    public void LinkRadioButtons(RadioButton[] radioButtons)
    {
        var count = radioButtons.Length;
        for (var i = 0; i < count; i++)
        {
            var buttonForLink = radioButtons[i];
            for (var j = 0; j < count; j++)
            {
                buttonForLink.OnEnabled += radioButtons[(i + j + 1) % count].Disable;
            }
        }
    }
}