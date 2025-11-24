using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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


        public Texture2D spritesheet;

        Texture2D swimframe0;
        Texture2D swimframe1;
        Texture2D swimframe2;
        Texture2D swimframe3;

        Texture2D jumpframe0;
        Texture2D jumpframe1;
        Texture2D jumpframe2;
        Texture2D jumpframe3;
        Texture2D jumpframe4;
        Texture2D jumpframe5;

        public Texture2D dolphin;

        public Vector2 textureSize;

        // i know, this method kinda sucks and is tedious. but im not fully sure how to make a proper array of frames yet so itll have to do

        public Vector2 position = new Vector2(0, 0);
        public Vector2 hitboxPosition;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        public int hitboxSize = 10;
        Vector2 gravity = new Vector2(0, 1200);

        public float pressure; // the closer you are to the bottom of the water, the more resistance the dolphin has in trying to go down
        public Vector2 maxSpeed = new Vector2(0, 200); // the player's speed limit

        // animation stuff below

        int fps;
        int frame = 1; // frame counter
        float ticker = 0f; // this float is the thing that 'ticks' each frame

        // calculating frames per second with time.deltatime

        float frameTimeElapsed = 0;
        float timeElapsed = 0;
        int timeElapsedSeconds;

        // stuff to switch to different animations

        bool switch0 = false;

        public void Setup()
        {
            DefineSprites();

        }
        public void Update(Water water)
        {
            //Collisions();
            if (submerged) SwimPhysics(water);
            else AirPhysics();
            HardPhysics(water);
            FloatingPointFix();

            ProcessPlayerMovement();
            WaterCollision(water);

            DrawPlayer();
            AnimateSprites();
        }

        void DefineSprites()
        {
            spritesheet = Graphics.LoadTexture("Textures/dolphinswimss.png"); // spritesheet just incase

            //swim
            swimframe0 = Graphics.LoadTexture("Textures/dolphinswim0.png");
            swimframe1 = Graphics.LoadTexture("Textures/dolphinswim1.png");
            swimframe2 = Graphics.LoadTexture("Textures/dolphinswim2.png");
            swimframe3 = Graphics.LoadTexture("Textures/dolphinswim3.png");

            //jump
            jumpframe0 = Graphics.LoadTexture("Textures/dolphinjump0.png");
            jumpframe1 = Graphics.LoadTexture("Textures/dolphinjump1.png");
            jumpframe2 = Graphics.LoadTexture("Textures/dolphinjump2.png");
            jumpframe3 = Graphics.LoadTexture("Textures/dolphinjump3.png");
            jumpframe4 = Graphics.LoadTexture("Textures/dolphinjump4.png");
            jumpframe5 = Graphics.LoadTexture("Textures/dolphinjump5.png");

            textureSize = new Vector2(swimframe0.Width, swimframe0.Height); // width is divided by 4 for flipbook subsetting
        }
        void AnimateSprites()
        {
            // overall time calculation. probably will be used for other stuff
            timeElapsed += Time.DeltaTime;
            timeElapsedSeconds = (int)timeElapsed;


            if (submerged == true) state = 1;
            else if (submerged == false) state = 2;

            if (state == 1) // swimming
            {
                float tick = 0.15f; // this float tells the value of the time given before the next frame

                if (switch0 == false)
                {
                    frameTimeElapsed = 0;
                    frame = 1;
                    ticker = tick;
                    switch0 = true;
                }

                if (frame == 1) dolphin = swimframe0;
                if (frame == 2) dolphin = swimframe1;
                if (frame == 3) dolphin = swimframe2;
                if (frame == 4) dolphin = swimframe3;

                frameTimeElapsed += Time.DeltaTime;

                if (frameTimeElapsed > ticker && frame <= 4) // final frame before the loop
                {
                    ticker += tick;
                    frame++;
                    // goes to the next frame every 'tick'
                }
                if (frame > 4)
                {
                    frame = 1;
                }
            }    
            else if (state == 2) // jumping
            {
                float tick = 0.15f ; // this float tells the value of the time given before the next frame

                if (switch0 == true)
                {
                    frameTimeElapsed = 0;
                    frame = 1;
                    ticker = tick;
                    switch0 = false;
                }

                if (frame == 1) dolphin = jumpframe0;
                if (frame == 2) dolphin = jumpframe1;
                if (frame == 3) dolphin = jumpframe2;
                if (frame == 4) dolphin = jumpframe3;
                if (frame == 5) dolphin = jumpframe4;
                if (frame == 6) dolphin = jumpframe5;

                frameTimeElapsed += Time.DeltaTime;

                if (frameTimeElapsed > ticker && frame < 6) // final frame before the loop
                {
                    ticker += tick;
                    frame++;
                    // goes to the next frame every 'tick'
                }
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
            //Draw.LineSize = 0;
            //Draw.FillColor = new ColorF(0.0f, 0.05f);
            //Draw.Circle((int)position.X + 70, (int)position.Y, 5); //convert floats to int to fix subpixel movement

            //drawing hitbox (the actual player)
            //Draw.FillColor = new Color(0, 0, 0);
            //Draw.Circle(hitboxPosition, hitboxSize); //convert floats to int to fix subpixel movement

            Graphics.Scale = 1f;
            Graphics.Rotation = 0.0f + (int)dolphinRotato2;
            Graphics.Draw(dolphin, (int)position.X - (swimframe0.Width / 2) + dolphinSpriteOffset.Y - (velocity.X / 8) + (velocity.Y / 32), (int)position.Y + (swimframe0.Height / 2) + dolphinSpriteOffset.X - (velocity.Y / 8) - 150); // a bunch of clustered math because i wanted to make sure it looked nice lol

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
                    velocity.Y += acceleration.Y + 20;
                }
                else
                {
                    velocity.Y += 5;
                }
            }

            if (isPlayerMovingUp == true) // moving up
            {
                isPlayerMoving = true;
                if (submerged == true)
                {
                    velocity.Y -= 1 + acceleration.Y + 20 + ((Window.Height - position.Y) / 80) + (0.01f / (1 + velocity.Y));
                }
                else
                {
                    position.Y -= 1;
                }
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
            pressure = ((100 - water.floorLevel / 1.5f) - position.Y * 0.15f) * 2.5f; // first value is the position of the pressure
            if (pressure > 0) pressure = 0;

            if (velocity.Y > 500) velocity.Y += pressure * 0.8f; // if player is going down fast, decrease pressure a tad
            else velocity.Y += pressure * 1.2f;

            position += 0.95f * velocity * Time.DeltaTime;
            if (position.X > 2 * Window.Width || position.X < -Window.Width || position.Y > 2 * Window.Height || position.Y < -Window.Height) return;
            velocity += 0.05f * gravity * Time.DeltaTime; // very small amount of gravity applied underwater
        }
        void WaterCollision(Water water) 
        {
            Vector2 surface = new Vector2(position.X, Window.Height - water.waterLevel);
            submerged = false;

            // debug circle. also doubles as a place one can put a water splash texture appearing
            Draw.FillColor = new Color(0, 0, 0);
            Draw.Circle(surface, 2); // debug
            if (velocity.Y > 600 && submerged == true) velocity.Y *= 1.2f;

            if (position.Y < Window.Height - water.waterLevel)
            {
                submerged = false;
            }
            if (position.Y >= Window.Height - water.waterLevel)
            {
                submerged = true;
            }
            if (position.Y >= Window.Height - water.waterLevel && position.Y < Window.Height - water.waterLevel + 75 && velocity.Y < 150)
            {
                submerged = true;
                velocity.Y *= 1.01f;
            }
            if (position.Y > Window.Height - water.waterLevel && position.Y < Window.Height - water.waterLevel + 180 && velocity.Y < 600) // parameters, working on gravity snaps to the top
            {
                int surfaceDistance = (int)Vector2.Distance(position, surface);
                velocity.Y -= (180 - surfaceDistance) / 18; // calculates how close the player is to the surface and speeds them up the closer they get

                if (velocity.Y > -70 && velocity.Y < 70 && submerged == true && surfaceDistance < 8) velocity.Y *= Random.Float(1f, 1.5f); // makes player even out at water level
            }
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