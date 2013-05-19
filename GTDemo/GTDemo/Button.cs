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
    class Button : GameObject
    {
        public delegate void ButtonClickedCallback();

        ButtonClickedCallback callback;
        bool mouseReleased = false;
        string resourceName;
 
        public Button(Level l, string rn, ButtonClickedCallback cb, Vector2 position)
            : base(l)
        {
            sprite.Position = position;
            callback = cb;
            resourceName = rn;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite.LoadContent(content, resourceName);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.DefaultDraw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && mouseReleased)
            {
                Rectangle mouseRect = new Rectangle(mouse.X, mouse.Y, 1, 1);
                if (mouseRect.Intersects(sprite.Rect))
                {
                    mouseReleased = false;
                    callback();
                }
            }
            else if (mouse.LeftButton == ButtonState.Released)
            {
                mouseReleased = true;
            }
        }
    }
}
