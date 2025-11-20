// Include the namespaces (code libraries) you need below.
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
            Window.SetSize(800, 600);
           // Text.Font = ("Arial");
            Text.Size = 20;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {

        }
    }

}
