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
    public enum LOT
    {
        Nonplayable=0,
        Living,
        Ghosts,
    }

    public class Level
    {
        GameState state;
        LOT landOfThe = LOT.Living;

        Player player;
        List<CoreObject> coreObjects = new List<CoreObject>();

        Button ghostButton;
        Button livingButton;
        Button trickButton;

        public LOT LandOfThe { get { return landOfThe; } }
        public GameState State { get { return state; } }
        public List<CoreObject> CoreObjects { get { return coreObjects; } }

        public Level(GameState s)
        {
            state = s;

            player = new Player(this);

            ghostButton = new Button(this, "ButtonGhost0", SwitchToGhost, new Vector2(130, 450));
            livingButton = new Button(this, "ButtonGhost1", SwitchToLiving, new Vector2(130, 450));
            trickButton = new Button(this, "ButtonTrick", player.Trick, new Vector2(670, 450));

            coreObjects = (new LevelLoader(this)).LoadLevel("level0.json");

            player.Possessed = coreObjects.First<CoreObject>();
        }

        public void LoadContent(ContentManager content)
        {
            player.LoadContent(state.Game.GraphicsDevice, content);
            ghostButton.LoadContent(content);
            livingButton.LoadContent(content);
            trickButton.LoadContent(content);
            foreach (CoreObject c in coreObjects)
            {
                c.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (landOfThe == LOT.Living)
            {
                ghostButton.Update(gameTime);
                trickButton.Update(gameTime);
            }
            else
            {
                livingButton.Update(gameTime);
            }
            
            player.Update(gameTime);

            foreach (CoreObject c in coreObjects)
            {
                if (landOfThe == LOT.Living) c.UpdateLiving(gameTime);
                if (landOfThe == LOT.Ghosts) c.UpdateGhost(gameTime);
                c.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (landOfThe == LOT.Living)
            {
                ghostButton.Draw(spriteBatch, gameTime);
                trickButton.Draw(spriteBatch, gameTime);
            }
            else
            {
                livingButton.Draw(spriteBatch, gameTime);
            }

            player.Draw(spriteBatch, gameTime);
            foreach (CoreObject c in coreObjects)
            {
                if (landOfThe == LOT.Living) c.DrawLiving(spriteBatch, gameTime);
                if (landOfThe == LOT.Ghosts) c.DrawGhost(spriteBatch, gameTime);
                c.Draw(spriteBatch, gameTime);
            }
        }

        public CoreObject CoreObjectByName(string name)
        {
            return coreObjects.Where(x => x.Name == name).First<CoreObject>();
        }

        public void SwitchToGhost()
        {
            landOfThe = LOT.Ghosts;
        }
        public void SwitchToLiving()
        {
            landOfThe = LOT.Living;
        }
    }
}
