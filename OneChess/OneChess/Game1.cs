using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OneChess
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState Hold;
        bool d = true;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            Ask.White = new Player(GraphicsDevice.Viewport, '1', true);
            Ask.Black = new Player(GraphicsDevice.Viewport, '2', false);
            Ask.Dot = new Enviroment(GraphicsDevice.Viewport);
            Stage.view = GraphicsDevice.Viewport;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ask.Dot.LoadComponents(Content);
            Ask.White.LoadTokens(Content);
            Ask.Black.LoadTokens(Content);
            Stage.LoadComponents(Content);
            Ask.White.Origen(7, Color.LightGoldenrodYellow);
            Ask.Black.Origen(0, Color.Gray);
            Ask.Dot.BoardSort();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Stage.start)
            {
                //Keyboard Event
                Match.Slots();
                //Mouse Event
                MouseState mouse = Mouse.GetState();
                if (Hold != mouse)
                {
                    Ask.Player_Click(mouse, Ask.Turn ? Ask.White : Ask.Black, Ask.Turn ? '2' : '1', Ask.Turn ? Color.LightGoldenrodYellow : Color.Gray);
                }
                Hold = mouse;
                Switch();
                //Frame Space
                if (Ask.pal)
                {
                    Stage.Abegin(); Ask.pal = false;
                }
                else Stage.Animation();

                if (Stage.New)
                {
                    Ask.Dot.Cleanse();
                    Stage.Tokinst();
                    Ask.White.Earn = Ask.Black.Earn = 0;
                    Ask.White.Origen(7, Color.LightGoldenrodYellow);
                    Ask.Black.Origen(0, Color.Gray);
                    Ask.Dot.BoardSort();
                    Ask.pal = true;
                    Ask.White.Avaliable();
                    Ask.Black.Avaliable();
                    Stage.New = false; Ask.Turn = true;
                }
            }
            if (Stage.check) Stage.Animate_Check();
            if (Stage.exit) this.Exit();
            base.Update(gameTime);
        }

        void Switch()
        {
            if (Stage.switchframe)
            {
                if (d != Ask.Turn)
                    Ask.Switch();
                d = Ask.Turn;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Ask.Dot.DrawBoard(spriteBatch);
            Ask.White.DrawTokens(spriteBatch);
            Ask.Black.DrawTokens(spriteBatch);
            Stage.DrawComponents(spriteBatch);
            Ask.Dot.DrawPointer(spriteBatch);
            if (Stage.check) Stage.DrawCheck(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}