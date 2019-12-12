using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceship
{
    class Asteroid
    {
        public Vector2 position;
        public int speed;
        public int radius = 59;
        static Random rand = new Random();

        public Asteroid(int newSpeed)
        {
            speed = newSpeed;
            position = new Vector2(960 + radius, rand.Next(0, 541));
        }

        public void asteroidUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
