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
    class Ship
    {
        //speed is divided by dt, which is about 60
        public int speed = 180;
        static public Vector2 startingPosition = new Vector2(480, 270);
        public Vector2 position = startingPosition;

        public void shipUpdate(GameTime gameTime, Controller gameController)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (gameController.inGame)
            {
                if (kState.IsKeyDown(Keys.Right) && position.X <960)
                {
                    position.X += speed * dt;
                }
                if (kState.IsKeyDown(Keys.Left) && position.X > 0)
                {
                    position.X -= speed * dt;
                }
                if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
                {
                    position.Y -= speed * dt;
                }
                if (kState.IsKeyDown(Keys.Down) && position.Y < 540)
                {
                    position.Y += speed * dt;
                }
            }
            
        }
    }
}
