using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{

    public class Clouds
    {
        Texture2D cloudTexture = Raylib.LoadTexture("MohawkGame2D/Assets/clouddd.png");

        public void Setup() 
        {
         
        }

        public void Update()
        {

        }
        public void DrawClouds()
        {
            Raylib.DrawTexture(cloudTexture, 1, 3, Color.White);
            Raylib.DrawTexture(cloudTexture, 300, 50, Color.White);
            Raylib.DrawTexture(cloudTexture, 600, 20, Color.White);
            Raylib.DrawTexture(cloudTexture, 900, 50, Color.White);

        }

    }
}
