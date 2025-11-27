using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Obstacle
    {
        // all of the arrays the functions will access

        float[] obstacleColliderX1Positions = []; // always set this value to 0, unless the image doesn't line up perfectly with the hitbox
        float[] obstacleColliderY1Positions = []; // set this value to 0 if the obstacle starts at the ceiling
        float[] obstacleColliderX2Positions = []; // never set this value to 0, as the collider would simply be a line
        float[] obstacleColliderY2Positions = []; // same goes for this one
        float[] obstacleOffsets = []; // set this value to 0 if you want it to start all the way on the left of the screen
        float[] obstacleImageYValues = []; // only needs a y value, as it's position is tied to the obstacle offset
        Texture2D[] obstacleImages = []; // all images go here
        public void Setup()
        {

        }

        public void Update()
        {
            for(i < 0)
            CollideObstacle();
            DrawObstacle();
        }

        // detects obstacles colliding with things
        void CollideObstacle(float colliderAreaX1, float colliderAreaY1, float colliderAreaX2, float colliderAreaY2, float obstacleOffset, int obstacleNumber)
        {
            
        }

        // draws obstacles
        void DrawObstacle(float obstacleImageY, Texture2D obstacleImage, float obstacleOffset, int obstacleNumber)
        {


            // detects if an obstacle is past the player

            if (obstacleOffset < 0)
            {
                CullObstacle();
            }
        }
        // if an obstacle is past the player, delete it
        void CullObstacle(int obstacleNumber)
        {

        }
    }
}
