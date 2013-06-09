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
    public class CSwitch : CoreObject
    {
        bool closed;

        public CSwitch(Level l, Vector2 position)
            : base(l)
        {
            TrickText = "Throw";

            throwSwitch(false);
            Sprite.Position = position;
        }

        public override void Trick()
        {
            throwSwitch(!closed);
            ((CPlatform)level.CoreObjectByName("Hover Platform")).SwitchDirection();
        }

        private void throwSwitch(bool c)
        {
            closed = c;
            if (closed) Name = "Switch (Up)";
            else        Name = "Switch (Down)";
        }
    }
}
