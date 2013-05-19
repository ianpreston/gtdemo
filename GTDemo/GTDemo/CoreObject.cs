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
    /**
     * An ingame object posessing a "core" -- in other words, something Sissel can posess.
     */
    public class CoreObject : GameObject
    {
        public string Name;
        public string TrickText;

        public CoreObject(Level l)
            : base(l)
        {
        }

        public virtual void Trick()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            sprite.LoadContent(content, "GenericCore");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public virtual void UpdateLiving(GameTime gameTime)
        {
        }

        public virtual void UpdateGhost(GameTime gameTime)
        {
        }

        public virtual void DrawGhost(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Sprite.Texture, Sprite.Position, null, Color.Blue, Sprite.RotationRadians, Sprite.Origin, 1f, SpriteEffects.None, 0f);
        }

        public virtual void DrawLiving(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.DefaultDraw(spriteBatch);
        }
    }
}
