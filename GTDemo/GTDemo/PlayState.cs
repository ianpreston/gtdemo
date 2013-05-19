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
    public class PlayState : GameState
    {
        Level level;

        public PlayState(Game1 g)
            : base(g)
        {
            level = new Level(this);
        }

        public override void LoadContent(ContentManager content)
        {
            level.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            level.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            level.Update(gameTime);
        }
    }
}
