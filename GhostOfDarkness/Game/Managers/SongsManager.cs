using Microsoft.Xna.Framework.Media;

namespace game;

internal static class SongsManager
{
    private static LoopedUnorderedQueue<Song> currentPlaylist;
    private static bool isPlayed;

    public static void StartPlaylist(LoopedUnorderedQueue<Song> playlist)
    {
        isPlayed = true;
        if (currentPlaylist == playlist)
            return;
        currentPlaylist = playlist;
        MediaPlayer.Stop();
        MediaPlayer.Play(currentPlaylist.GetNext());
    }

    public static void Update()
    {
        if (!isPlayed)
            return;
        if (MediaPlayer.State == MediaState.Stopped)
            MediaPlayer.Play(currentPlaylist.GetNext());
    }

    public static void Pause()
    {
        isPlayed = false;
        MediaPlayer.Pause();
    }

    public static void Resume()
    {
        if (MediaPlayer.State == MediaState.Paused)
        {
            isPlayed = true;
            MediaPlayer.Resume();
        }
    }

    public static void Stop()
    {
        isPlayed = false;
        MediaPlayer.Stop();
    }
}