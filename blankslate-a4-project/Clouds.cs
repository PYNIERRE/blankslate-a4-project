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
            Raylib.DrawTexture(cloudTexture, 3, 3, Color.White);

        }

    }
}
