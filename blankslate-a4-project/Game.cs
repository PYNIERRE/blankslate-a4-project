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
        public Player player = new Player();
        public Water water = new Water();
        // self note. for future zach dont put a new game class here

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Dolph");
            Window.SetSize(1200, 700);
            Window.TargetFPS = 120;

            player.Setup();
            player.position = player.startPosition;
            water.waterLevelTarget = 420;
            water.waterLevel = 150;
            water.floorLevel = -50;
            water.floorLevelTarget = 20;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(new ColorF(1.0f, 1.0f));

            water.Update();

            //DebugVisuals(player); // putting values into visuals
            player.Update(water);
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