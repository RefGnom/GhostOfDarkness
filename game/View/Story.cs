using System.Collections.Generic;

namespace game;

internal static class Story
{
    public static string DungeonDescription => @"Если кто-то будет читать эту записку,то лучше
бегите отсюда как можно скорее.Для самых смелых
и отчаянных скажу,что комната призрака тьмы
находится ровно по коридору вперёд,но убить
просто так его не получится.Сначала вам придётся
убить всех его преслужников в этом подземелье,
потому что призрак питает силу от них.

Удачи тебе,храбрый воин.";

    public static Message GetPrologue()
    {
        var parent = new Message("Наконец-то я добрался до этого места");
        var message = parent;
        message.Next = new Message("Я сделаю всё,чтобы вернуть свою любимую");
        message = message.Next;
        message.Next = new Message("Что это за записка на стене?..");
        return parent;
    }
}