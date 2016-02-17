using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OneChess
{
    public static class Stage
    {
        public static string user1 = "White", user2 = "Black";
        private static SpriteFont font, Fporcent;
        private static Texture2D Back, porcent, shade, frame, avatar;
        public static List<Texture2D>[] token = new List<Texture2D>[2];
        private static int[,] amount = new int[2, 5];
        private static int[,] Pox = new int[2, 5];
        private static int row;
        private static Texture2D[] shelf;
        public static Viewport view;
        private static char[] cad;
        private static Vector2[] pos;
        private static Vector2 vuser;
        private static Rectangle Fsize, Asize;
        private static Stopwatch count, count2;
        //Variables reservadas
        public static bool New = false, exit = false,
         start = true, check = false, starman = true,
         showturn = true, switchframe = true, style = false,
         showname = true;

        //check
        #region check

        /// <summary>
        /// Inicio de la animación para una cadena cuando ocurra el evento Check o Check Mate
        /// </summary>
        /// <param name="Checkmode">Cadena del  mensaje en un vector de caracteres</param>
        public static void Begin(char[] Checkmode)
        {
            cad = new char[Checkmode.Length];
            pos = new Vector2[Checkmode.Length];

            for (int c = 0; c < Checkmode.Length; c++)
                cad[c] = Checkmode[c];

            for (int i = 0; i < Checkmode.Length; i++)
                pos[i] = new Vector2(820 + (i * 100), 200);

            count = new Stopwatch(); count = Stopwatch.StartNew();
        }
        /// <summary>
        /// Transcurre la animación
        /// </summary>
        public static void Animate_Check()
        {
            for (int i = 0; i < cad.Length; i++) if (pos[i].X > view.Width / 2 - 130 + i * 20)
                    pos[i].X -= 10;
            
            if (count.Elapsed.Seconds > 2)
            {
                for (int i = 0; i < cad.Length; i++)
                    if (pos[i].X > -30)
                        pos[i].X -= 35.5f / (i + 1);
                if (count.Elapsed.Seconds > 6) check = false;
            }
        }
        /// <summary>
        /// Imprime la animación en la pantalla
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public static void DrawCheck(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < cad.Length; i++)
                spriteBatch.DrawString(font, cad[i].ToString(), pos[i], i % 2 == 0 ? Color.White : Color.Black);
        }
        #endregion
        
        public static void Tokinst()
        {
            token[0] = new List<Texture2D>();
            token[1] = new List<Texture2D>();
            amount = new int[2, 5];
        }
        public static void LoadComponents(ContentManager Content)
        {
            Tokinst();
            avatar = Ask.White.King.Texture;
            Back = Content.Load<Texture2D>("Back");
            frame = Content.Load<Texture2D>("Frame");
            porcent = shade = Content.Load<Texture2D>("porcent");
            shelf = new Texture2D[2];
            Fporcent = Content.Load<SpriteFont>("Fporcent");
            font = Content.Load<SpriteFont>("Font");
            for (int n = 0; n < 2; n++)
                shelf[n] = Content.Load<Texture2D>("Shelf_" + n);
        }
        public static void Abegin()
        {
            for (int n = 0; n < 2; n++) vuser = new Vector2(-frame.Width + 50, frame.Height / 2 + 15);
            Fsize = new Rectangle(-frame.Width, 0, frame.Width, frame.Height);
            Asize = new Rectangle(-avatar.Width - 20, frame.Height / 10, avatar.Width / 2, avatar.Height / 4);
            count2 = new Stopwatch();
            count2 = Stopwatch.StartNew();
        }
        public static void Animation()
        {
            if (count2.Elapsed.Seconds < 1 && Fsize.X < 0) { Fsize.X += 5; Asize.X += 5; vuser.X += 5; }
            else if (count2.Elapsed.Seconds > 0 && Fsize.X > -Fsize.Width) { Fsize.X -= 5; Asize.X -= 5; vuser.X -= 5; }
        }
        public static void EarnedToken(Texture2D tok, int m)
        {
            if (tracing(tok, row = m)) token[m].Add(tok);
        }
        public static void DrawComponents(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ask.Dot.Token.Texture, Ask.Dot.Token.Size, Ask.Dot.Token.Color);
            spriteBatch.Draw(Back, new Rectangle(575, 0, Back.Width - 20, Back.Height), Color.Red);
            for (int n = 0; n < 2; n++) spriteBatch.Draw(shelf[n], new Rectangle(583, 340 + (n * 70), shelf[n].Width, shelf[n].Height), Color.White);
            for (int r = 0; r < 2; r++)
                if (token[r].Count > 0)
                    for (int c = 0; c < token[r].Count; c++)
                        spriteBatch.Draw(token[r][c], new Rectangle(620 + (c * 30), 430 - (r * 70), 20, 37), r == 1 ? Color.LightGoldenrodYellow : Color.Gray);
            Drawamount(spriteBatch);
            spriteBatch.Draw(porcent, new Rectangle(635, 410, 100, porcent.Height - 3), Color.Black);
            spriteBatch.Draw(porcent, new Rectangle(635, 410, (int)Ask.Black.Earn, porcent.Height - 3), Color.Red);
            spriteBatch.Draw(porcent, new Rectangle(635, 410, (int)Ask.White.Earn, porcent.Height - 3), Color.White);
            spriteBatch.Draw(porcent, new Rectangle(635, 341, 100, porcent.Height - 3), Color.Black);
            spriteBatch.Draw(porcent, new Rectangle(635, 341, (int)Ask.White.Earn, porcent.Height - 3), Color.Red);
            spriteBatch.Draw(porcent, new Rectangle(635, 341, (int)Ask.Black.Earn, porcent.Height - 3), Color.White);
            spriteBatch.DrawString(Fporcent, (int)Ask.White.Earn + "%", new Vector2(680, 410), Color.White);
            spriteBatch.DrawString(Fporcent, (int)Ask.Black.Earn + "%", new Vector2(680, 341), Color.White);
            if (showturn)
            {
                spriteBatch.Draw(frame, Fsize, Ask.Turn ? Color.Red : Color.SlateBlue);
                spriteBatch.Draw(avatar, Asize, Ask.Turn ? Color.LightGoldenrodYellow : Color.Gray);
                spriteBatch.DrawString(font, Ask.Turn ? user1 : user2, vuser, Color.White);
            }
        }
        private static void Drawamount(SpriteBatch spriteBatch)
        {
            for (int r = 0; r < 2; r++)
                for (int n = 0; n < token[r].Count; n++)
                    if (amount[r, n] > 0)
                    {
                        spriteBatch.Draw(frame, new Rectangle(635 + (Pox[r, n] * 30), 425 - (r * 70), 14, 13), Color.Black);
                        spriteBatch.DrawString(Fporcent, "x" + (amount[r, n] + 1), new Vector2(635 + (Pox[r, n] * 30), 425 - (r * 70)), Color.White);
                    }
        }
        public static bool tracing(Texture2D tok, int row)
        {
            for (int c = 0; c < token[row].Count; c++)
                if (tok.Equals(token[row][c]))
                {
                    Pox[row, c] = c;
                    amount[row, c]++; return false;
                }
            return true;
        }
    }
}
