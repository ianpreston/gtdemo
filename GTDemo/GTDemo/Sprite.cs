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
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public float Scale = 1f;

        public float RotationRadians { get { return Rotation % ((float)Math.PI * 2); } }
        public Vector2 Origin { get { return new Vector2(Texture.Width / 2, Texture.Height / 2); } }

        public Rectangle Rect { get { return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Texture.Width, Texture.Height); } }
        public BoundingBox BoundingBox { get { return new BoundingBox(new Vector3(Rect.Left, Rect.Top, 0), new Vector3(Rect.Right, Rect.Bottom, 0)); } }

        public Sprite()
        {
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
        }

        public void DefaultDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, RotationRadians, Origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
