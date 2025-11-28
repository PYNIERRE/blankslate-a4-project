using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MohawkGame2D
{
    public class Obstacle
    {
        // all of the arrays the functions will access, would use vector2, but that'd take up way more lines than just separate arrays

        float[] colliderX1Positions = [0]; // always set this value to 0, unless the image doesn't line up perfectly with the hitbox
        float[] colliderY1Positions = [403]; // set this value to 0 if the obstacle starts at the ceiling
        float[] colliderX2Positions = [197,]; // never set this value to 0, as the collider would simply be a line
        float[] colliderY2Positions = [700,]; // same goes for this one
        float[] offsets = [1000,]; // set this value to 0 if you want it to start all the way on the left of the screen
        float[] imageYValues = [403,]; // only needs a y value, as it's position is tied to the obstacle offset
        string[] images = ["Texures/obstacles/coralsprite.png",]; // all images go here, file paths and what have you

        // define all your obstacles here, for example:
        private Texture2D coralObstacle1;
        public void Setup()
        {
            // and then define them again here, using this format:
            coralObstacle1 = Graphics.LoadTexture(images[0]);
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
            Graphics.Scale = 1.0f;
            Graphics.Rotation = 0.0f;

            // define your obstacle's image, then replace "OBSTACLE" with the one you defined, you'll need to do this for each obstacle:
            Graphics.Draw(coralObstacle1, obstacleOffset + globalOffset, obstacleImageY);

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
