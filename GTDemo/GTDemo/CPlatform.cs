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
        int leftBound;
        int rightBound;
        int topBound;
        int bottomBound;
        bool axisX = true;
        CPlatformDirection direction = CPlatformDirection.Right;

        public CPlatform(Level l, int lb, int rb, int tb, int bb)
            : base(l)
        {
            Name = "Hover Platform";
            TrickText = "";

            leftBound = lb;
            rightBound = rb;
            topBound = tb;
            bottomBound = bb;

            Sprite.Position = new Vector2(leftBound, topBound);
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
                if (Sprite.Position.X <= leftBound)
                    direction = CPlatformDirection.Right;
                if (Sprite.Position.X >= rightBound)
                    direction = CPlatformDirection.Left;
            }
            else
            {
                if (Sprite.Position.Y <= topBound)
                    direction = CPlatformDirection.Down;
                if (Sprite.Position.Y >= bottomBound)
                    direction = CPlatformDirection.Up;
            }

            if (direction == CPlatformDirection.Left)  Sprite.Position.X -= 6f;
            if (direction == CPlatformDirection.Right) Sprite.Position.X += 6f;
            if (direction == CPlatformDirection.Up)    Sprite.Position.Y -= 6f;
            if (direction == CPlatformDirection.Down)  Sprite.Position.Y += 6f;
        }
    }
}
