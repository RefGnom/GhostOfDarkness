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
        message.Next = new Message("Я сделаю всё,чтобы вернуть свою любимую Эбигейл");
        message = message.Next;
        message.Next = new Message("Что это за записка на стене?..");
        return parent;
    }

    public static Message GetFinalDialog()
    {
        var parent = new Message("*К сожалению,спасение Эбегейл не реализовано на данный момент.");
        var message = parent;
        message.Next = new Message("*Но вы можете пофантазировать и представить эту сцену и её образ у себя в голове:)");
        message = message.Next;
        message.Next = new Message("Фууух..Я смог одолеть призрака,но где же Эбигейл??");
        message = message.Next;
        message.Next = new Message("Эбигейл:Нори,это ты? Ты спас меня? Как тебе это удалось?");
        message = message.Next;
        message.Next = new Message("Продолженние следует...");
        message = message.Next;
        message.OnNext = () =>
        {
            Debug.Log("Запускается музыка");
        };
        return parent;
    }
}