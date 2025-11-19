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
        public Vector2 startPosition = new Vector2(100,250);
        public Vector2 position;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        public Vector2 gravity = new Vector2(0, 750);

        float waterPressure = 0.2f; // ideally the closer you are to the bottom of the water, the more resistance the dolphin has in trying to go down
        float maxSpeed = 400; // the player's speed limit
        public void Setup()
        {

        }
        public void Update()
        {
            //Collisions();
            if (submerged == true)
            {
                SwimPhysics();
            }
            else
            {
                AirPhysics();
            }
            DrawPlayer();
            ProcessPlayerMovement();
        }
        void DrawPlayer()
        {
            Draw.LineSize = 2;
            Draw.LineColor = new ColorF(0.0f, 1.0f);
            Draw.FillColor = new Color(255, 255, 255);
            Draw.Circle(0, 0, 35); //drawing player rectangle
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
                        acceleration.X = i * 10;
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
                    velocity.Y *= 0.85f;
                }

                if (isPlayerMovingDown == true)
                {
                    isPlayerMoving = true;
                    velocity.Y += acceleration.Y + 75.0f * maxSpeed;
                }

                if (isPlayerMovingUp == true)
                {
                    isPlayerMoving = true;
                    velocity.Y -= acceleration.Y + 75.0f * maxSpeed;
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
            position += -(position * waterPressure) * Time.DeltaTime; // water pressure
            position += velocity * Time.DeltaTime;
        }
        void AirPhysics()
        {
            velocity += gravity * Time.DeltaTime; // velocity changes because of gravity, position changes because of velocity
        }
    }
}
