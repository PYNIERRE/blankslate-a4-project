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
        public float floorLevel; // the position of the floor relative to the bottom of the screen
        public int floorLevelTarget; // the position of the floor relative to the bottom of the screen
        public void Setup()
        {
            
        }
        public void Update()
        {
            LevelMovement();
            FloatingPointFix();

            DrawWater();
            WaterTest(); // debug for water
        }
        void LevelMovement()
        {
            waterLevel = ((25 * waterLevel) + waterLevelTarget) / 26; // slowly ease waterLevel to waterLevelTarget
            floorLevel = ((25 * floorLevel) + floorLevelTarget) / 26; // slowly ease waterLevel to waterLevelTarget
        }
        void DrawWater()
        {
            // floor
            Draw.LineSize = 1;
            Draw.LineColor = Color.Black;
            Draw.FillColor = new ColorF(0.0f, 0.2f);
            Draw.Rectangle(0, Window.Height - (int)floorLevel, Window.Width, Window.Height * 2);

            // water
            Draw.LineSize = 1;
            Draw.LineColor = Color.Black;
            Draw.FillColor = new ColorF(0.0f, 0.2f);
            Draw.Rectangle(0, Window.Height - (int)waterLevel, Window.Width, Window.Height * 2);
        }

        void WaterTest() // module to change the "water level target" to a random number
        {
            Text.Size = 14;

            Text.Draw($"waterLevel: {waterLevel}", 20, 140);
            Text.Draw($"waterLevelTarget: {waterLevelTarget}", 20, 160); // hastily put debug visuals into here
            bool waterDebug = (Input.IsKeyboardKeyPressed(KeyboardInput.E));
            if (waterDebug)
            {
                waterLevelTarget = Random.Integer(250, 600);
                floorLevelTarget = Random.Integer(50, 125);
            }
        }
        void FloatingPointFix()
        {
            if (waterLevel < waterLevelTarget + 0.5 && waterLevel > waterLevelTarget - 0.5) waterLevel = waterLevelTarget; // ik its such a small line of code but i wanted to keep it like this for organization sake just like the player cs
        }
    }
}
