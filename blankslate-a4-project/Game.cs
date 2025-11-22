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

            water.Update();
            WaterCollision(player, water);

            DebugVisuals(player); // putting values into visuals
            player.Update();
        }
        void WaterCollision(Player player, Water water) // screw it. putting this here because it causes a stack overflow in other classes
        {
            Vector2 surface = new Vector2(player.position.X, Window.Height - water.waterLevel);
            player.submerged = false;

            // debug circle. also doubles as a place one can put a water splash texture appearing
            Draw.FillColor = new Color(0, 0, 0);
            Draw.Circle(surface, 2); // debug
            if (player.velocity.Y > 600 && player.submerged == true) player.velocity.Y *= 1.2f;

            if (player.position.Y < Window.Height - water.waterLevel)
            {
                player.submerged = false;
            }
            if (player.position.Y >= Window.Height - water.waterLevel)
            {
                player.submerged = true;
            }
            if (player.position.Y > Window.Height - water.waterLevel && player.position.Y < Window.Height - water.waterLevel + 180) // parameters, working on gravity snaps to the top
            {
                int surfaceDistance = (int)Vector2.Distance(player.position, surface);
                Text.Draw($"surface distance: {surfaceDistance}", 20, 180);
                player.velocity.Y -= (180 - surfaceDistance) / 18; // calculates how close the player is to the surface and speeds them up the closer they get
            }
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