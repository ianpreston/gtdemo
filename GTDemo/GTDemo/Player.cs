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
    class Player : GameObject
    {
        const int RANGE = 180;

        bool jumping = false;

        Texture2D line;
        SpriteFont spriteFont;

        public CoreObject Possessed;

        public Player(Level l)
            : base(l)
        {
        }

        public void Trick()
        {
            Possessed.Trick();
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            sprite.LoadContent(content, "Player");
            spriteFont = content.Load<SpriteFont>("Arial");

            line = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            line.SetData<Int32>(new Int32[] { 0xFFFFFF }, 0, line.Width * line.Height);
        }

        public override void Update(GameTime gameTime)
        {
            // Get the mouse's position and convert it from screen coordinates to game coordinates
            Vector2 mouse = level.State.GameCamera.TransformRealPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Vector2 sissel = Sprite.Position;

            // Get the distance from the player to the mouse
            float mouse_distance = (float)Math.Sqrt(Math.Pow(mouse.X - sissel.X, 2) + Math.Pow(mouse.Y - sissel.Y, 2));

            // If the player presses the mouse over Sissel, start "jumping"
            if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                level.LandOfThe == LOT.Ghosts &&
                mouse_distance <= Sprite.Rect.Width)
            {
                jumping = true;
            }

            // If the player releases the mouse, stop jumping
            if (Mouse.GetState().LeftButton == ButtonState.Released &&
                level.LandOfThe == LOT.Ghosts)
            {
                jumping = false;
            }

            // Jump
            if (jumping)
            {
                // Get the angle, heading vector, and a ray from the player to the mouse
                float angle = (float)Math.Atan2(mouse.Y - sissel.Y, mouse.X - sissel.X);
                Vector2 heading = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Ray ray = new Ray(
                    new Vector3(sissel.X, sissel.Y, 0),
                    new Vector3(heading.X, heading.Y, 0));

                foreach (CoreObject c in level.CoreObjects)
                {
                    // Ignore the object we are currently possessing
                    if (c.Equals(Possessed)) continue;

                    // Get the distance from the player to this CoreObject
                    float distance = (float)Math.Sqrt(Math.Pow(c.Sprite.Position.X - sissel.X, 2) + Math.Pow(c.Sprite.Position.Y - sissel.Y, 2));

                    // If this CoreObject is farther than the mouse (how far the player wants to jump) or the player's
                    // RANGE (how far the player is capable of jumping), don't jump
                    if (distance > RANGE) continue;
                    if (distance > mouse_distance) continue;

                    // Given those constraints, if the player's jump hits the CoreObject, possess it
                    if (ray.Intersects(c.Sprite.BoundingBox) != null && c.Equals(Possessed) == false)
                    {
                        Possessed = c;
                        jumping = false;
                        break;
                    }
                }
            }

            Sprite.Position = Possessed.Sprite.Position;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (level.LandOfThe == LOT.Ghosts)
                sprite.DefaultDraw(spriteBatch);

            if (jumping)
            {
                Vector2 mouse = level.State.GameCamera.TransformRealPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                Vector2 sissel = Sprite.Position;

                DrawJumpLightning(spriteBatch, sissel, mouse);
            }

            string hudText = Possessed.Name + "\n" + Possessed.TrickText;
            spriteBatch.DrawString(spriteFont, hudText, level.State.GameCamera.TransformRealPosition(Vector2.Zero), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        protected void DrawJumpLightning(SpriteBatch spriteBatch, Vector2 sissel, Vector2 mouse)
        {
            float angle = (float)Math.Atan2(sissel.Y - mouse.Y, sissel.X - mouse.X);
            float distance = (float)Math.Sqrt(Math.Pow(sissel.X - mouse.X, 2) + Math.Pow(sissel.Y - mouse.Y, 2));

            spriteBatch.Draw(line, new Rectangle((int)sissel.X, (int)sissel.Y, (int)Math.Min(RANGE, distance), 1), null, Color.White, angle, new Vector2(1, 1), SpriteEffects.None, 0f);
        }
    }
}
