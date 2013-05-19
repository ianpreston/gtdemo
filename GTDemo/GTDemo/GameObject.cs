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
    public abstract class GameObject
    {
        protected Level level;
        protected Sprite sprite;

        public Sprite Sprite { get { return sprite; } }

        public GameObject(Level l)
        {
            level = l;
            sprite = new Sprite();
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}
