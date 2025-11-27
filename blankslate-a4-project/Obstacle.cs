using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Obstacle
    {
        // all of the arrays the functions will access, would use vector2, but that'd take up way more lines than just separate arrays

        float[] colliderX1Positions = []; // always set this value to 0, unless the image doesn't line up perfectly with the hitbox
        float[] colliderY1Positions = []; // set this value to 0 if the obstacle starts at the ceiling
        float[] colliderX2Positions = []; // never set this value to 0, as the collider would simply be a line
        float[] colliderY2Positions = []; // same goes for this one
        float[] offsets = []; // set this value to 0 if you want it to start all the way on the left of the screen
        float[] imageYValues = []; // only needs a y value, as it's position is tied to the obstacle offset
        string[] images = []; // all images go here, file paths and what have you
        public void Setup()
        {
            // define all your obstacles here, for example:
            // Texture2D coralObstacle1;

            for(int i = 0; i < images.Length; i++)
            {
                // and then define them again here, using this format:
                // coralObstacle1 = Graphics.LoadTexture(images[i]);
            }
        }

        public void Update()
        {
            // how far the obstacles have travelled to the left from their initial position
            float globalOffset = Time.DeltaTime * 100.0f;

            // cycles through each defined obstacle
            for (int i = 0; i < (colliderX1Positions.Length); i++)
            {
                // if the obstacle is undefined, don't try to render it
                if (images[i] != null)
                {
                    CollideObstacle(colliderX1Positions[i], colliderY1Positions[i], colliderX2Positions[i], colliderY2Positions[i], offsets[i], globalOffset);
                    DrawObstacle(imageYValues[i], images[i], offsets[i], i, globalOffset); // this needs the index because it has dependencies for CullObstacle
                }
            }
        }

        // detects obstacles colliding with things
        void CollideObstacle(float colliderAreaX1, float colliderAreaY1, float colliderAreaX2, float colliderAreaY2, float obstacleOffset, float globalOffset)
        {
            Draw.FillColor = new Color(255, 0, 0, 50);
            // un-comment this line if you want to debug hitbox sizes
            // Draw.Rectangle((colliderAreaX1 + obstacleOffset) - globalOffset, colliderAreaY1, colliderAreaX2 - colliderAreaX1, colliderAreaY2 - colliderAreaY1);
        }

        // draws obstacles
        void DrawObstacle(float obstacleImageY, string obstacleImage, float obstacleOffset, int index, float globalOffset)
        {
            // actually draws the obstacle, relative to the offset.


            // detects if an obstacle is past the player by one game window size, as insurance to make sure it being removed isn't a visible action

            if ((obstacleOffset - globalOffset) < (Window.Width * -1))
            {
                CullObstacle(index);
            }
        }
        // if an obstacle is past the player, delete it
        void CullObstacle(int obstacleNumber)
        {
            // deletes by undefining, as that's what the "if" statement in the update() funciton is detecting
            images[obstacleNumber] = null;
        }
    }
}
