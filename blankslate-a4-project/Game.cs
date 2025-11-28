// Include the namespaces (code libraries) you need below.
using Raylib_cs;
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        float distance = 0;
        float distancePerSecond = 10;

        public Clouds clouds = new Clouds();
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Dolphin Game");
            Window.SetSize(1200, 700);
            Window.TargetFPS = 120;


        }
        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);
            distance += 1;

            Text.Size = 20;
            Text.Draw($"{distance}", 0, 0);

            clouds.DrawClouds();


        }
    }

}