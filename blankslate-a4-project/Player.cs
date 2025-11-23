using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Player
    {
        public bool submerged = true;
        bool isPlayerMoving;

        public Vector2 startPosition = new Vector2(150, 400);

        public int state; // 1 swimming, 2 jumping
        int[] frame = new int[3];

        public Texture2D texture;
        public Vector2 textureSize;

        public Texture2D swimframe0;
        public Texture2D swimframe1;
        public Texture2D swimframe2;
        public Texture2D swimframe3;
        // i know, this method kinda sucks and is tedious. but im not fully sure how to make a proper array of frames yet so itll have to do

        public Vector2 position = new Vector2(0, 0);
        public Vector2 hitboxPosition;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        public int hitboxSize = 10;
        Vector2 gravity = new Vector2(0, 1000);

        public float pressure; // the closer you are to the bottom of the water, the more resistance the dolphin has in trying to go down
        public Vector2 maxSpeed = new Vector2(0, 200); // the player's speed limit

        public void Setup()
        {
            texture = Graphics.LoadTexture("Textures/dolphinswimss.png"); // spritesheet just incase

            swimframe0 = Graphics.LoadTexture("Textures/dolphinswim0.png");
            swimframe1 = Graphics.LoadTexture("Textures/dolphinswim1.png");
            swimframe2 = Graphics.LoadTexture("Textures/dolphinswim2.png");
            swimframe3 = Graphics.LoadTexture("Textures/dolphinswim3.png");
            textureSize = new Vector2(100, 100); // width is divided by 4 for flipbook subsetting
        }
        public void Update(Water water)
        {
            //Collisions();
            if (submerged) SwimPhysics(water);
            else AirPhysics();
            HardPhysics(water);
            FloatingPointFix();

            ProcessPlayerMovement();

            DrawPlayer();
            ProcessSpritesheet();
        }
        void ProcessSpritesheet()
        {
            if (submerged == true) state = 1;
            else state = 2;

            if (state == 1)
            {
                
            }    

        }
        void DrawPlayer()
        {
            // aligning the player's hitbox with the dolphin itself
            hitboxPosition.X = (int)position.X - (velocity.X / 12) + 5;
            hitboxPosition.Y = (int)position.Y - (velocity.Y / 12);

            Vector2 dolphinSpriteOffset = new Vector2(50, 50);
            float dolphinRotato1 = 0; // initial rotational value
            float dolphinRotato2 = 0;
            dolphinRotato1 = (((4 * dolphinRotato1) + velocity.Y) / 5); // initial rotational value
            dolphinRotato2 = (((3 * dolphinRotato2) + dolphinRotato1) / 4); // double rotational value so you get that sweet clean easing

            float radius = 50;
            float angle = (dolphinRotato2 / 360 * MathF.PI);

            dolphinSpriteOffset.X = (radius * MathF.Cos(angle)); 
            dolphinSpriteOffset.Y = (radius * MathF.Sin(angle));



            // draws path circle (original player)
            Draw.FillColor = new Color(0, 0, 0);
            Draw.Circle((int)position.X + 70, (int)position.Y, 5); //convert floats to int to fix subpixel movement

            //drawing hitbox (the actual player)
            Draw.FillColor = new Color(0, 0, 0);
            Draw.Circle(hitboxPosition, hitboxSize); //convert floats to int to fix subpixel movement

            Graphics.Scale = 1f;
            Graphics.Rotation = 0.0f + (int)dolphinRotato2;
            Graphics.Draw(swimframe0, (int)position.X - (swimframe0.Width / 2) + dolphinSpriteOffset.Y - (velocity.X / 8) + (velocity.Y / 32), (int)position.Y + (swimframe0.Height / 2) + dolphinSpriteOffset.X - (velocity.Y / 8) - 150); // a bunch of clustered math because i wanted to make sure it looked nice lol

            //Graphics.Scale = 1.5f;
            //Graphics.Rotation = 0.0f;
            //Graphics.DrawSubset(texture, position, position, textureSize); // neat subset stuff
        }
        void ProcessPlayerMovement()
        {
            bool isPlayerMovingUp = (Input.IsKeyboardKeyDown(KeyboardInput.W)) || (Input.IsKeyboardKeyDown(KeyboardInput.Up));
            bool isPlayerMovingLeft = (Input.IsKeyboardKeyDown(KeyboardInput.A)) || (Input.IsKeyboardKeyDown(KeyboardInput.Left));
            bool isPlayerMovingDown = (Input.IsKeyboardKeyDown(KeyboardInput.S)) || (Input.IsKeyboardKeyDown(KeyboardInput.Down));
            bool isPlayerMovingRight = (Input.IsKeyboardKeyDown(KeyboardInput.D)) || (Input.IsKeyboardKeyDown(KeyboardInput.Right));

            bool isPlayerOutOfWater = (Input.IsKeyboardKeyDown(KeyboardInput.Q)); //debug!!! no longer does anything

            isPlayerMoving = false;

            // horizontal accelleration for moving. balances out because of the speed limit
            if (isPlayerMoving == true)
            {
                acceleration.Y+= 1;
            }
            else
            {
                acceleration.Y-= 1;
            }
            if (acceleration.Y < 0) acceleration.Y = 0;

            // directional movement
            if (isPlayerMovingUp && isPlayerMovingDown && submerged == true) // bugfix for moving in 2 directions at a time
            {
                isPlayerMoving = false;
                isPlayerMovingUp = false;
                isPlayerMovingDown = false;
            }

            if (!isPlayerMoving && submerged == true)
            {
                velocity.Y *= 0.97f;
            }

            if (isPlayerMovingDown == true) // moving down
            {
                if (submerged)
                {
                    isPlayerMoving = true;
                    velocity.Y += acceleration.Y + 18;
                }
                else
                {
                    velocity.Y += 5;
                }
            }

            if (isPlayerMovingUp == true && submerged == true) // moving up
            {
                isPlayerMoving = true;
                velocity.Y -= acceleration.Y + 18 + ((Window.Height - position.Y) / 80);
            }

            if (isPlayerOutOfWater == true) // checks for if player is out of water
            {
                submerged = false; // BAM. sets submerged boolvalue to false
            }
            if (isPlayerOutOfWater == false)
            {
                submerged = true;
            }

            // player speed limit
            if (velocity.Y > maxSpeed.Y)
            {
                velocity.Y = (((8 * velocity.Y) + maxSpeed.Y) / 9);
            }
            if (velocity.Y < -maxSpeed.Y)
            {
                velocity.Y = (((8 * velocity.Y) + -maxSpeed.Y) / 9);
            }
            // basically increments the player velocity every frame slowly back to the speed limit
        }
        void HardPhysics(Water water)
        {
            if (position.Y > Window.Height - water.floorLevel - 5)
            {
                position.Y = Window.Height - water.floorLevel - 5;
                velocity.Y = 0;
            }
        }
        void SwimPhysics(Water water)
        {
            maxSpeed.Y = 550; // water maxspeed

            // value of the pressure
            pressure = 0;
            pressure = ((120 - water.floorLevel / 2) - position.Y * 0.15f) * 2.5f; // first value is the position of the pressure
            if (pressure > 0) pressure = 0;

            if (velocity.Y > 500) velocity.Y += pressure * 0.8f; // if player is going down fast, decrease pressure a tad
            else velocity.Y += pressure * 1.2f;

            position += 0.95f * velocity * Time.DeltaTime;
            velocity += 0.05f * gravity * Time.DeltaTime; // very small amount of gravity applied underwater
        }
        void AirPhysics()
        {
            maxSpeed.Y = 700; // air maxspeed

            position += 1.1f * velocity * Time.DeltaTime; // additional value represents velocity increase
            velocity += gravity * Time.DeltaTime; // velocity changes because of gravity, position changes because of velocity
        }
        void FloatingPointFix()
        {
            if (velocity.Y < 1 && velocity.Y > 0 && isPlayerMoving == false) velocity.Y = 0;
            if (velocity.Y > 1 && velocity.Y < 0 && isPlayerMoving == false) velocity.Y = 0;
        }
    }
}