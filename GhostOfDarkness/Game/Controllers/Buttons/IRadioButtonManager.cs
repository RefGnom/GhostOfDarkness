using Core.DependencyInjection;

namespace Game.Controllers.Buttons;

[DiUsage]
public interface IRadioButtonManager
{
    public void LinkRadioButtons(RadioButton[] radioButtons);
}