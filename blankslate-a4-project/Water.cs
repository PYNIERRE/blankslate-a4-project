using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Water
    {
        public float waterLevel; // the position of the floor relative to the bottom of the screen
        public int waterLevelTarget; // target that the water level tweens to
        public void Setup()
        {

        }
        public void Update()
        {
            WaterLevelMovement();
            FloatingPointFix();

            DrawWater();
            // WaterTest(); // debug for water
        }
        void WaterLevelMovement()
        {
            waterLevel = ((25 * waterLevel) + waterLevelTarget) / 26; // slowly ease waterLevel to waterLevelTarget
        }
        void DrawWater()
        {
            Draw.LineSize = 1;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.Gray;
            Draw.Rectangle(0, Window.Height - (int)waterLevel, Window.Width, Window.Height * 2);
        }

        /* void WaterTest() // module to change the "water level target" to a random number
        {
            Text.Draw($"waterLevel: {waterLevel}", 20, 140);
            Text.Draw($"waterLevelTarget: {waterLevelTarget}", 20, 160); // hastily put debug visuals into here
            bool waterDebug = (Input.IsKeyboardKeyPressed(KeyboardInput.E));
            if (waterDebug) waterLevelTarget = Random.Integer(250, 600);
        } */
        void FloatingPointFix()
        {
            if (waterLevel < waterLevelTarget + 0.5 && waterLevel > waterLevelTarget - 0.5) waterLevel = waterLevelTarget; // ik its such a small line of code but i wanted to keep it like this for organization sake just like the player cs
        }
    }
}
