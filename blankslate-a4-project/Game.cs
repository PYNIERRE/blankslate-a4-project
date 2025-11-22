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
        Player player = new Player();
        Water water = new Water();

        public int floorLevel; // the position of the floor relative to the bottom of the screen

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Dolph");
            Window.SetSize(1200, 700);
            Window.TargetFPS = 120;

            player.position = player.startPosition;
            water.waterLevelTarget = 400;
            water.waterLevel = 50;
            floorLevel = 75;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);

            DebugVisuals(player); // putting values into visuals

            water.Update();
            player.Update();
        }

        void DebugVisuals(Player player)
        {
            Text.Size = 14;

            Text.Draw($"pressure: {player.pressure}", 20, 100);
            Text.Draw($"acceleration: {player.acceleration}", 20, 80);
            Text.Draw($"maxspeed: {player.maxSpeed}", 20, 60);
            Text.Draw($"position: {player.position}", 20, 40);
            Text.Draw($"velocity: {player.velocity}", 20, 20);
        }
    }

}