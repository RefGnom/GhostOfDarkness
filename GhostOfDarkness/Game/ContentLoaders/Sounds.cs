using Game.Objects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace game;

internal static class Sounds
{
    private static LoopedUnorderedQueue<Song> hallwaySongs;
    private static LoopedUnorderedQueue<Song> roomSongs;
    private static LoopedUnorderedQueue<Song> bossSongs;

    public static LoopedUnorderedQueue<Song> HallwaySongs => hallwaySongs.Shuffle();
    public static LoopedUnorderedQueue<Song> RoomSongs => roomSongs.Shuffle();
    public static LoopedUnorderedQueue<Song> BossSongs => bossSongs.Shuffle();
    public static Song VictorySong { get; private set; }

    public static void Load(ContentManager content)
    {
        hallwaySongs = new
        (
            content.Load<Song>("Sounds\\FortressOfDoom-ChadMossholder"),
            content.Load<Song>("Sounds\\BeastOfTheArena"),
            content.Load<Song>("Sounds\\PhobosSpace")
        );
        roomSongs = new
        (
            content.Load<Song>("Sounds\\TheBaronOfHell"),
            content.Load<Song>("Sounds\\DemonicCorruption"),
            content.Load<Song>("Sounds\\InfiltrateTheCult")
        );
        bossSongs = new
        (
            content.Load<Song>("Sounds\\CultistBase"),
            content.Load<Song>("Sounds\\TheOnlyThingTheyFearIsYou"),
            content.Load<Song>("Sounds\\TheSuperGoreNest")
        );
        VictorySong = content.Load<Song>("Sounds\\AlienWarfare");
    }
}