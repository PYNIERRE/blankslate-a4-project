using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Obstacle
    {
        public void Setup()
        {

        }

        public void Update()
        {
            CollideObstacle();
            DrawObstacle();
        }

        // detects obstacles colliding with things
        void CollideObstacle(float colliderAreaX, float colliderAreaY)
        {

        }

        // draws obstacles
        void DrawObstacle(float obstacleImageY, Texture2D obstacleImage)
        {

        }

        // if an obstacle is past the player, delete it
        void CullObstacle(int obstacleNumber)
        {

        }
    }
}
