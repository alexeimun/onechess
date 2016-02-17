using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OneChess
{
    public class Enviroment
    {
        private Viewport Screen;
        public Chess point, Token;
        public Chess[] pointcollect;
        public Chess[][] Board;
        /// <summary>
        /// Primera instancia
        /// </summary>
        /// <param name="Screen">Viewport o deimensiones de la pantalla</param>
        public Enviroment(Viewport Screen)
        {
            this.Screen = Screen;
            point = new Chess();
            pointcollect = new Chess[3];
            for (int i = 0; i < 3; i++)
                pointcollect[i] = new Chess();
            Token = Ask.White.King;
            InitializeComponents();
        }
        #region instance objects
        /// <summary>
        /// Instancian del tablero de fichas
        /// </summary>
        private void InitializeComponents()
        {
            //board instace
            for (int i = 0; i <= 8; i++)
                Board = new Chess[i][];
            for (int j = 0; j < 8; j++)
                Board[j] = new Chess[8];
            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
                    Board[i][j] = new Chess();
            //filling disposing
            Cleanse();
        }
        /// <summary>
        /// Rellena las ID´s de las casillas del tablero  como vació
        /// </summary>
        public void Cleanse()
        {
            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
                    Board[i][j].ID = "";
        }
        #endregion
        /// <summary>
        /// Organiza el tablero con sus dimensiones y colores
        /// </summary>
        public void BoardSort()
        {
            int width = Screen.Width / 11;
            int height = Screen.Height / 8;
            int zip = 0;
            Color color1, color2;
            if (Stage.style)
            {
                color1 = Color.Black;
                color2 = Color.White;
            }
            else
            {
                color1 = Color.SaddleBrown;
                color2 = Color.SandyBrown;
            }
            for (int i = 0; i < 8; i++)
            {
                int X = i * width;
                zip++; if (zip > 2) zip = 1;
                for (int j = 0; j < 8; j++)
                {
                    Board[i][j].Color = zip % 2 == 0 ? color1 : color2;
                    zip++; if (zip > 2) zip = 1;
                    int Y = j * height;
                    Board[i][j].Size = new Rectangle(X, Y, width, height);
                }
            }
        }
        /// <summary>
        /// Carga los componentes adicionales como texturas del mause y las casillas
        /// </summary>
        /// <param name="Content"></param>
        public void LoadComponents(ContentManager Content)
        {
            for (int i = 0; i < 2; i++)
                pointcollect[i].Texture = Content.Load<Texture2D>("Mouse/pick" + i);
            point.Texture = pointcollect[0].Texture;
            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
                    Board[i][j].Texture = Content.Load<Texture2D>("Frame");
            point.Size = new Rectangle(-30, -30, point.Texture.Width, point.Texture.Height);
            point.Color = Color.White;
        }

        /// <summary>
        /// Cambia la posición del tablero, de abajo hacia arriva 
        /// </summary>
        /// <param name="player">Jugador actual</param>
        public void SwitchTable(Player player)
        {
            for (int i = 0; i < 2; i++)
            {
                player.Rook[i].Y = 7 - player.Rook[i].Y;
                player.Knight[i].Y = 7 - player.Knight[i].Y;
                player.Bishop[i].Y = 7 - player.Bishop[i].Y;
            }
            player.Queen.Y = 7 - player.Queen.Y;
            player.King.Y = 7 - player.King.Y;
            for (int i = 0; i < 8; i++) player.Pawn[i].Y = 7 - player.Pawn[i].Y;
            player.Localize();
        }
        /// <summary>
        /// Dibuja el tablero en la pantalla
        /// </summary>
        /// <param name="sprite">Spritebatch</param>
        public void DrawBoard(SpriteBatch sprite)
        {
            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++)
                    sprite.Draw(Board[i][j].Texture, Board[i][j].Size, Board[i][j].Color);
        }
        /// <summary>
        /// Dibuaja el puntero en la pantalla
        /// </summary>
        /// <param name="sprite">Spritebach</param>
        public void DrawPointer(SpriteBatch sprite)
        {
            sprite.Draw(point.Texture, point.Size, point.Color);
        }
    }
}