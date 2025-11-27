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
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Dolphin Game");
            Window.SetSize(800, 500);
            // Text.Font = ("Arial");
            Text.Size = 20;
            int distancecounter = 0;
            Text.Draw("Distance:" + distancecounter, new Vector2(1, 1));
            if (distancecounter == 0)
                distancecounter = 1;

        }
        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);
        }
    }

}
