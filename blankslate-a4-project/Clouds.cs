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

        public void Setup() 
        {
            
        }

        public void Update()
        {

        }
        public void DrawClouds()
        {

           Texture2D cloudTexture = Raylib.LoadTexture("MohawkGame2D/Assets/clouddd.png");
            Raylib.DrawTexture(cloudTexture, 3, 3, Color.White);

        }

    }
}
