using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OneChess
{
    public class Player
    {
        public Viewport Screen;
        //tokens
        /// <summary>
        /// Array  de ocho peones
        /// </summary>
        public Chess[] Pawn;
        /// <summary>
        /// Pieza del rey
        /// </summary>
        public Chess King;
        /// <summary>
        /// Pieza de la reina
        /// </summary>
        public Chess Queen;
        /// <summary>
        /// Array de dos torres
        /// </summary>
        public Chess[] Rook;
        /// <summary>
        /// Array de dos caballos
        /// </summary>
        public Chess[] Knight;
        /// <summary>
        /// Array de dos alfiles
        /// </summary>
        public Chess[] Bishop;
        //values
        public bool Rol;
        public char Id;
        public int width;
        public int height;
        public int X, Y, W;
        private float earn;
        /// <summary>
        /// Da un porcentage de cada ficha ganada en combate, según el grado de la ficha
        /// </summary>
        public float Earn
        {
            get { return earn; }
            set { earn = value; }
        }
        /// <summary>
        /// Instancia con tres parametros
        /// </summary>
        /// <param name="Screen">Viewport</param>
        /// <param name="Id">Identificación de la ficha</param>
        /// <param name="Rol">Turno del jugador</param>
        public Player(Viewport Screen, char Id, bool Rol)
        {
            this.Rol = Rol;
            this.Screen = Screen;
            this.Id = Id;
            Instance(Id);
        }
        /// <summary>
        /// Se instancian las Fichas
        /// </summary>
        /// <param name="Id">Identificación única del jugadoor</param>
        private void Instance(char Id)
        {
            width = Screen.Width / 15;
            height = Screen.Height / 9;
            X = Screen.Width / 11;
            Y = Screen.Width / 13;
            W = Screen.Width / 100;
            Bishop = new Chess[2];
            Pawn = new Chess[8];
            King = new Chess();
            King.ID = Id + "King";
            Queen = new Chess();
            Queen.ID = Id + "Queen";
            Knight = new Chess[2];
            Rook = new Chess[2];
            for (int i = 0; i < 2; i++)
            {
                Bishop[i] = new Chess();
                Knight[i] = new Chess();
                Rook[i] = new Chess();
                Bishop[i].ID = Id + "Bishop";
                Knight[i].ID = Id + "Knight";
                Rook[i].ID = Id + "Rook";
                Bishop[i].Visible = true;
                Knight[i].Visible = true;
                Rook[i].Visible = true;
            }
            for (int i = 0; i < 8; i++)
            {
                Pawn[i] = new Chess();
                Pawn[i].ID = Id + "Pawn";
            }
            Avaliable();
        }
        #region Sorting Tokens
        /// <summary>
        /// Posición Inicial de las fichas
        /// </summary>
        /// <param name="y">Posición vertical inicial</param>
        /// <param name="color">Color de las fichas</param>
        public void Origen(int y, Color color)
        {
            PlayerColor(color);
            //Filling Values X, Y
            for (int i = 0; i < 8; i++)
            {
                Pawn[i].X = i; Pawn[i].Y = y == 0 ? 1 : 6;
            }
            Rook[0].X = 0; Rook[0].Y = y;
            Knight[0].X = 1; Knight[0].Y = y;
            Bishop[0].X = 2; Bishop[0].Y = y;
            Queen.X = 3; Queen.Y = y;
            King.X = 4; King.Y = y;
            Bishop[1].X = 5; Bishop[1].Y = y;
            Knight[1].X = 6; Knight[1].Y = y;
            Rook[1].X = 7; Rook[1].Y = y;
            Localize();
        }
        /// <summary>
        /// Dimensiona las fichas con sus respectivos atributos
        /// </summary>
        public void Localize()
        {
            //Tokens sort
            for (int i = 0; i < 2; i++)
            {
                Rook[i].Size = new Rectangle(Rook[i].X * X + W, Y * Rook[i].Y, width, height);
                Knight[i].Size = new Rectangle(Knight[i].X * X + W, Y * Knight[i].Y, width, height);
                Bishop[i].Size = new Rectangle(Bishop[i].X * X + W, Y * Bishop[i].Y, width, height);
            }
            Queen.Size = new Rectangle(Queen.X * X + W, Y * Queen.Y, width, height);
            King.Size = new Rectangle(King.X * X + W, Y * King.Y, width, height);
            for (int i = 0; i < 8; i++)
                Pawn[i].Size = new Rectangle(Pawn[i].X * X + W, Y * Pawn[i].Y, width, height);
            Fill_Tokens();
        }
        /// <summary>
        /// Ubica las fichas en el tablero
        /// </summary>
        private void Fill_Tokens()
        {
            //filling regs 
            for (int i = 0; i < 8; i++)
            {
                if (Pawn[i].Visible)
                {
                    Ask.Dot.Board[Pawn[i].X][Pawn[i].Y].Token = Pawn[i];
                    Ask.Dot.Board[Pawn[i].X][Pawn[i].Y].ID = Id + "Pawn";
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (Rook[i].Visible)
                {
                    Ask.Dot.Board[Rook[i].X][Rook[i].Y].Token = Rook[i];
                    Ask.Dot.Board[Rook[i].X][Rook[i].Y].ID = Id + "Rook";
                }
                if (Knight[i].Visible)
                {
                    Ask.Dot.Board[Knight[i].X][Knight[i].Y].Token = Knight[i];
                    Ask.Dot.Board[Knight[i].X][Knight[i].Y].ID = Id + "Horse";
                }
                if (Bishop[i].Visible)
                {
                    Ask.Dot.Board[Bishop[i].X][Bishop[i].Y].Token = Bishop[i];
                    Ask.Dot.Board[Bishop[i].X][Bishop[i].Y].ID = Id + "Bishop";
                }
            }
            if (Queen.Visible)
            {
                Ask.Dot.Board[Queen.X][Queen.Y].Token = Queen;
                Ask.Dot.Board[Queen.X][Queen.Y].ID = Id + "Queen";
            }
            Ask.Dot.Board[King.X][King.Y].Token = King;
            Ask.Dot.Board[King.X][King.Y].ID = Id + "King";
        }
        /// <summary>
        /// Ajusta en verdedero la disponibilidad de las fichas y el enroque
        /// </summary>
        public void Avaliable()
        {
            for (int i = 0; i < 8; i++)
                Pawn[i].Visible = Pawn[i].initial = true;
            for (int i = 0; i < 2; i++)
                Bishop[i].Visible = Knight[i].Visible = Rook[i].Visible = Rook[i].castle = true;
            King.Visible = true;
            Queen.Visible = true;
            King.castle = true;
        }
        /// <summary>
        /// Cambia el color de la ficha
        /// </summary>
        /// <param name="color">Color de la ficha</param>
        public void PlayerColor(Color color)
        {
            for (int i = 0; i < 2; i++)
                Rook[i].Color = Knight[i].Color = Bishop[i].Color = color;

            King.Color = color;
            Queen.Color = color;
            for (int i = 0; i < 8; i++)
                Pawn[i].Color = color;
        }
        #endregion
        /// <summary>
        /// Carga las texturas de la fichas
        /// </summary>
        /// <param name="Content">Content</param>
        public void LoadTokens(ContentManager Content)
        {
            Queen.Texture = Content.Load<Texture2D>("Tokens/Queen");
            King.Texture = Content.Load<Texture2D>("Tokens/King");
            for (int i = 0; i < 8; i++)
                Pawn[i].Texture = Content.Load<Texture2D>("Tokens/Pawn");
            for (int i = 0; i < 2; i++)
            {
                Bishop[i].Texture = Content.Load<Texture2D>("Tokens/Bishop");
                Knight[i].Texture = Content.Load<Texture2D>("Tokens/Horse");
                Rook[i].Texture = Content.Load<Texture2D>("Tokens/Tower");
            }
        }
        /// <summary>
        /// Dibuja las fichas en el tablero
        /// </summary>
        /// <param name="sprite">Spritebatch</param>
        public void DrawTokens(SpriteBatch sprite)
        {
            if (Queen.Visible) sprite.Draw(Queen.Texture, Queen.Size, Queen.Color);
            if (King.Visible) sprite.Draw(King.Texture, King.Size, King.Color);
            for (int i = 0; i < 8; i++) if (Pawn[i].Visible)
                    sprite.Draw(Pawn[i].Texture, Pawn[i].Size, Pawn[i].Color);
            for (int i = 0; i < 2; i++)
            {
                if (Bishop[i].Visible) sprite.Draw(Bishop[i].Texture, Bishop[i].Size, Bishop[i].Color);
                if (Knight[i].Visible) sprite.Draw(Knight[i].Texture, Knight[i].Size, Knight[i].Color);
                if (Rook[i].Visible) sprite.Draw(Rook[i].Texture, Rook[i].Size, Rook[i].Color);
            }
        }
    }
}
