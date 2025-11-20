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
        public Vector2 startPosition = new Vector2(100, 400);

        public Vector2 position;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        Vector2 gravity = new Vector2(0, 750);

        float waterPressure = 0.2f; // ideally the closer you are to the bottom of the water, the more resistance the dolphin has in trying to go down
        float maxSpeed = 80; // the player's speed limit
        public void Setup()
        {

        }
        public void Update()
        {
            //Collisions();
            SwimPhysics();
            ProcessPlayerMovement();

            DrawPlayer();
        }
        void DrawPlayer()
        {
            Draw.LineSize = 2;
            Draw.LineColor = new ColorF(0.0f, 1.0f);
            Draw.FillColor = new Color(0,0,0);
            Draw.Circle(position, 10); //drawing player rectangle
        }
        void ProcessPlayerMovement()
        {
            bool isPlayerMovingUp = (Input.IsKeyboardKeyDown(KeyboardInput.W)) || (Input.IsKeyboardKeyDown(KeyboardInput.Up));
            bool isPlayerMovingLeft = (Input.IsKeyboardKeyDown(KeyboardInput.A)) || (Input.IsKeyboardKeyDown(KeyboardInput.Left));
            bool isPlayerMovingDown = (Input.IsKeyboardKeyDown(KeyboardInput.S)) || (Input.IsKeyboardKeyDown(KeyboardInput.Down));
            bool isPlayerMovingRight = (Input.IsKeyboardKeyDown(KeyboardInput.D)) || (Input.IsKeyboardKeyDown(KeyboardInput.Right));
            bool isPlayerMoving = false;

                // horizontal accelleration for moving. balances out because of the speed limit
                if (isPlayerMoving == true)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        acceleration.Y = i * 10;
                    }
                }

                // directional movement
                if (isPlayerMovingUp && isPlayerMovingDown)
                {
                    isPlayerMoving = false;
                    isPlayerMovingUp = false;
                    isPlayerMovingDown = false;
                }

                if (!isPlayerMoving)
                {
                    velocity.Y *= 0.925f;
                }

                if (isPlayerMovingDown == true)
                {
                    isPlayerMoving = true;
                    velocity.Y += acceleration.Y + 0.65f * maxSpeed;
                }

                if (isPlayerMovingUp == true)
                {
                    isPlayerMoving = true;
                    velocity.Y -= acceleration.Y + 0.65f * maxSpeed;
                }

                // player speed limit
                if (velocity.X > maxSpeed)
                {
                    velocity.X = maxSpeed + 0.1f * velocity.X;
                }
                if (velocity.X < -maxSpeed)
                {
                    velocity.X = -(maxSpeed + 0.1f * -velocity.X);
                }
            }
        void SwimPhysics()
        {
            maxSpeed = 90;
            float pressure = 0;
            pressure = 45 - position.Y * 0.15f; // first value is the position of the thing
            if (pressure > 0) pressure = 0;
            velocity.Y += pressure * 1.2f;

            position += velocity * Time.DeltaTime;
        }
        void AirPhysics()
        {
            maxSpeed = 75;
            position += velocity * Time.DeltaTime;
            velocity += gravity * Time.DeltaTime; // velocity changes because of gravity, position changes because of velocity
        }
    }
}
