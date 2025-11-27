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

        float[] obstacleColliderX1Positions = [];
        float[] obstacleColliderY1Positions = [];
        float[] obstacleColliderX2Positions = [];
        float[] obstacleColliderY2Positions = [];
        float[] obstacleOffsets = [];
        float[] obstacleImageYValues = [];
        Texture2D[] obstacleImages = [];
        public void Setup()
        {

        }

        public void Update()
        {
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

        }

        // if an obstacle is past the player, delete it
        void CullObstacle(int obstacleNumber)
        {

        }
    }
}
