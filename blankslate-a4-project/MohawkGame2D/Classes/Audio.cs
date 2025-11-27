using MohawkGame2D;
/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

using Raylib_cs;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace MohawkGame2D
{

    public static class Audio
    {
        public static Music bgSound;
        public static void InitBgAudio()
        {
            if (!Raylib.IsAudioDeviceReady())
            {
                Raylib.InitAudioDevice();
            }

            bgSound = Raylib.LoadMusicStream("C:\\Users\\Barrington\\Source\\Repos\\blankslate-a4-project\\blankslate-a4-project\\MohawkGame2D\\Assets\\pixel-party-218705.wav");
            Raylib.PlayMusicStream(bgSound);
            Raylib.SetMusicVolume(bgSound, 0.5f);
            Raylib.SetMusicPitch(bgSound, 0.9f);
        }

        public static void UnloadMusic(Music music)
        {

            bool isPlaying = Raylib.IsMusicStreamPlaying(music);
            Raylib.UnloadMusicStream(music);
        }

        public static void StopBgAudio()
        {
            Raylib.StopMusicStream(bgSound);
            Raylib.UnloadMusicStream(bgSound);
        }

    }

}


