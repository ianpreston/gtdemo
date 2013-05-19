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
    delegate void MenuAction();

    struct MenuItem
    {
        public string Text;
        public MenuAction OnClick;
    }

    public class MainMenuState : GameState
    {
        SpriteFont menuFont;

        string headerText = "Ghost Trick Gameplay Demo";
        List<MenuItem> menuItems = new List<MenuItem>();
        int selectedMenuItem = 0;

        public MainMenuState(Game1 g) : base(g)
        {
            menuItems.Add(new MenuItem { Text = "Play Now", OnClick = OnPlayClicked });
            menuItems.Add(new MenuItem { Text = "Exit", OnClick = OnExitClicked });
        }

        public override void LoadContent(ContentManager content)
        {
            menuFont = content.Load<SpriteFont>("Arial");
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(menuFont, headerText, Vector2.Zero, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            Vector2 position = new Vector2(40, 50);
            foreach (MenuItem itm in menuItems)
            {
                Color color;
                if (menuItems.IndexOf(itm) == selectedMenuItem)
                    color = Color.Red;
                else
                    color = Color.White;

                spriteBatch.DrawString(menuFont, itm.Text, position, color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                position.Y += 30;
            }
        }

        public override void Update(GameTime gameTime)
        {
            var keystates = Keyboard.GetState();

            if (keystates.IsKeyDown(Keys.Up) && selectedMenuItem > 0)
                selectedMenuItem--;
            if (keystates.IsKeyDown(Keys.Down) && selectedMenuItem < (menuItems.Count - 1))
                selectedMenuItem++;

            if (keystates.IsKeyDown(Keys.Space) || keystates.IsKeyDown(Keys.Enter))
                menuItems[selectedMenuItem].OnClick();
        }

        protected void OnPlayClicked()
        {
            game.SwitchState(new PlayState(game));
        }

        protected void OnExitClicked()
        {
            game.Exit();
        }
    }
}
