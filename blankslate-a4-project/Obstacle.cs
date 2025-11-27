using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Obstacle
    {
        // all of the arrays the functions will access, would use vector2, but that'd take up way more lines than just four separate arrays

        float[] colliderX1Positions = []; // always set this value to 0, unless the image doesn't line up perfectly with the hitbox
        float[] colliderY1Positions = []; // set this value to 0 if the obstacle starts at the ceiling
        float[] colliderX2Positions = []; // never set this value to 0, as the collider would simply be a line
        float[] colliderY2Positions = []; // same goes for this one
        float[] offsets = []; // set this value to 0 if you want it to start all the way on the left of the screen
        float[] imageYValues = []; // only needs a y value, as it's position is tied to the obstacle offset
        Texture2D[] images = []; // all images go here
        public void Setup()
        {

        }

        public void Update()
        {
            // cycles through each defined obstacle
            for(int i = 0; i < (colliderX1Positions.Length); i++)
            {
                // if the obstacle is undefined, don't try to render it
                if(images[i] != null)
                {
                    CollideObstacle(colliderX1Positions[i], colliderY1Positions[i], colliderX2Positions[i], colliderY2Positions[i], offsets[i]);
                    DrawObstacle(imageYValues[i], images[i], offsets[i], i);
                }
            }
        }

        // detects obstacles colliding with things
        void CollideObstacle(float colliderAreaX1, float colliderAreaY1, float colliderAreaX2, float colliderAreaY2, float obstacleOffset)
        {
            
        }

        // draws obstacles
        void DrawObstacle(float obstacleImageY, Texture2D obstacleImage, float obstacleOffset, int index)
        {


            // detects if an obstacle is past the player

            if (obstacleOffset < 0)
            {
                CullObstacle(index);
            }
        }
        // if an obstacle is past the player, delete it
        void CullObstacle(int obstacleNumber)
        {

        }
    }
}
