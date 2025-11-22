using System;
using System.Collections.Generic;
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

        public Vector2 position = new Vector2(0, 0);
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        Vector2 gravity = new Vector2(0, 950);

        public float pressure; // the closer you are to the bottom of the water, the more resistance the dolphin has in trying to go down
        public Vector2 maxSpeed = new Vector2(0, 200); // the player's speed limit

        public void Setup()
        {

        }
        public void Update()
        {
            //Collisions();
            if (submerged) SwimPhysics();
            else AirPhysics();
            FloatingPointFix();

            ProcessPlayerMovement();

            DrawPlayer();
        }
        void DrawPlayer()
        {
            // draws circle at player
            Draw.LineSize = 2;
            Draw.LineColor = new ColorF(0.0f, 1.0f);
            Draw.FillColor = new Color(0, 0, 0);
            Draw.Circle((int)position.X, (int)position.Y, 10); //convert floats to int to fix subpixel movement
        }
        void ProcessPlayerMovement()
        {
            bool isPlayerMovingUp = (Input.IsKeyboardKeyDown(KeyboardInput.W)) || (Input.IsKeyboardKeyDown(KeyboardInput.Up));
            bool isPlayerMovingLeft = (Input.IsKeyboardKeyDown(KeyboardInput.A)) || (Input.IsKeyboardKeyDown(KeyboardInput.Left));
            bool isPlayerMovingDown = (Input.IsKeyboardKeyDown(KeyboardInput.S)) || (Input.IsKeyboardKeyDown(KeyboardInput.Down));
            bool isPlayerMovingRight = (Input.IsKeyboardKeyDown(KeyboardInput.D)) || (Input.IsKeyboardKeyDown(KeyboardInput.Right));
            bool isPlayerOutOfWater = (Input.IsKeyboardKeyDown(KeyboardInput.Q)); //debug!!!
            isPlayerMoving = false;

            // horizontal accelleration for moving. balances out because of the speed limit
            if (isPlayerMoving == true)
            {
                acceleration.Y+= 1;
            }
            else if (!isPlayerMoving == false)
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
                velocity.Y *= 0.95f;
            }

            if (isPlayerMovingDown == true && submerged == true) // moving down
            {
                isPlayerMoving = true;
                velocity.Y += acceleration.Y + 0.05f * maxSpeed.Y;
            }

            if (isPlayerMovingUp == true && submerged == true) // moving up
            {
                isPlayerMoving = true;
                velocity.Y -= acceleration.Y + 0.05f * maxSpeed.Y;
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
                velocity.Y = (((6 * velocity.Y) + maxSpeed.Y) / 7);
            }
            if (velocity.Y < -maxSpeed.Y)
            {
                velocity.Y = (((6 * velocity.Y) + -maxSpeed.Y) / 7);
            }
            // basically increments the player velocity every frame slowly back to the speed limit
        }
        void SwimPhysics()
        {
            maxSpeed.Y = 550; // water maxspeed

            // value of the pressure
            pressure = 0;
            pressure = (80 - position.Y * 0.15f) * 2; // first value is the position of the pressure
            if (pressure > 0) pressure = 0;

            if (velocity.Y > 150) velocity.Y += pressure * 0.8f; // if player is going down fast, decrease pressure a tad
            else velocity.Y += pressure * 1.2f;

            position += velocity * Time.DeltaTime;
            velocity += 0.05f * gravity * Time.DeltaTime; // very small amount of gravity applied underwater
        }
        void AirPhysics()
        {
            maxSpeed.Y = 700; // air maxspeed

            position += 1.5f * velocity * Time.DeltaTime; // additional value represents velocity increase
            velocity += gravity * Time.DeltaTime; // velocity changes because of gravity, position changes because of velocity
        }
        void FloatingPointFix()
        {
            if (velocity.Y < 1 && velocity.Y > 0 && isPlayerMoving == false) velocity.Y = 0;
            if (velocity.Y > 1 && velocity.Y < 0 && isPlayerMoving == false) velocity.Y = 0;
        }
    }
}