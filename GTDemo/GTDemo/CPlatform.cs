using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GTDemo
{
    enum CPlatformDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public class CPlatform : CoreObject
    {
        int LEFT_BOUND = 200;
        int RIGHT_BOUND = 550;
        int TOP_BOUND = 100;
        int BOTTOM_BOUND = 400;
        bool axisX = true;
        CPlatformDirection direction = CPlatformDirection.Right;

        public CPlatform(Level l)
            : base(l)
        {
            Name = "Hover Platform";
            TrickText = "";

            Sprite.Position = new Vector2(LEFT_BOUND, TOP_BOUND);
        }

        public void SwitchDirection()
        {
            axisX = !axisX;
            if (direction == CPlatformDirection.Left || direction == CPlatformDirection.Right)
                direction = CPlatformDirection.Up;
            else if (direction == CPlatformDirection.Up || direction == CPlatformDirection.Down)
                direction = CPlatformDirection.Left;
        }

        public override void UpdateLiving(GameTime gameTime)
        {
            if (axisX)
            {
                if (Sprite.Position.X <= LEFT_BOUND)
                    direction = CPlatformDirection.Right;
                if (Sprite.Position.X >= RIGHT_BOUND)
                    direction = CPlatformDirection.Left;
            }
            else
            {
                if (Sprite.Position.Y <= TOP_BOUND)
                    direction = CPlatformDirection.Down;
                if (Sprite.Position.Y >= BOTTOM_BOUND)
                    direction = CPlatformDirection.Up;
            }

            if (direction == CPlatformDirection.Left)  Sprite.Position.X -= 6f;
            if (direction == CPlatformDirection.Right) Sprite.Position.X += 6f;
            if (direction == CPlatformDirection.Up)    Sprite.Position.Y -= 6f;
            if (direction == CPlatformDirection.Down)  Sprite.Position.Y += 6f;
        }
    }
}
