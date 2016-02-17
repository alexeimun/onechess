using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace OneChess
{
    static class Ask
    {
        static public Player White, Black;
        private static Player Player;
        public static Enviroment Dot;
        private static int id, Z, eat, diago;
        private static bool intersect = false;
        private static Chess Asist;
        private static Color Color;
        private static char enemy;
        public static bool Turn = true, pal = true, move = false;

        static public void Player_Click(MouseState mouse, Player player, char Enemy, Color color)
        {
            Color = color; Player = player; enemy = Enemy;

            if (!move)
            {
                Clear(Color);
                #region King
                if (Dot.point.Size.Intersects(player.King.Size) && player.King.Visible && !intersect)
                {
                    intersect = true;
                    if (DrawKingCells(player.King) && mouse.LeftButton == ButtonState.Pressed)
                    {
                        Dot.Token = player.King;
                        Dot.point.Texture = Dot.pointcollect[1].Texture;
                        Dot.Board[player.King.X][player.King.Y].ID = "";
                        move = true; id = 5; Asist = player.King;
                    }
                    else Dot.point.Texture = Dot.pointcollect[0].Texture;
                }
                #endregion
                if (checKing(player.King.X, player.King.Y, 7))
                {
                    #region pawns

                    for (int i = 0; i < 8; i++)
                    {
                        if (Dot.point.Size.Intersects(player.Pawn[i].Size) && player.Pawn[i].Visible && !intersect)
                        {
                            intersect = true;

                            bool Pass = false;
                            if (player.Rol) Pass = DrawUpPawnCells(Player.Pawn[i]);
                            else Pass = DrawDownPawnCells(Player.Pawn[i]);

                            if (Pass && mouse.LeftButton == ButtonState.Pressed)
                            {
                                Dot.Token = player.Pawn[i];
                                Dot.point.Texture = Dot.pointcollect[1].Texture;
                                Dot.Board[player.Pawn[i].X][player.Pawn[i].Y].ID = "";
                                move = true;
                                id = 0; Asist = player.Pawn[i];
                            }
                            else Dot.point.Texture = Dot.pointcollect[0].Texture;
                            break;
                        }
                    }
                    #endregion

                    #region Rook
                    for (int i = 0; i < 2; i++)
                    {
                        if (Dot.point.Size.Intersects(player.Rook[i].Size) && player.Rook[i].Visible && !intersect)
                        {
                            intersect = true;
                            if (DrawCellsinCrux(player.Rook[i]) && mouse.LeftButton == ButtonState.Pressed)
                            {
                                if (mouse.LeftButton == ButtonState.Pressed)
                                {
                                    Dot.Token = player.Rook[i];
                                    Dot.point.Texture = Dot.pointcollect[1].Texture;
                                    Dot.Board[player.Rook[i].X][player.Rook[i].Y].ID = "";
                                    move = true; id = 2; Asist = player.Rook[i];
                                }
                                else Dot.point.Texture = Dot.pointcollect[0].Texture;
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Knight
                    for (int i = 0; i < 2; i++)
                    {
                        if (Dot.point.Size.Intersects(player.Knight[i].Size) && player.Knight[i].Visible && !intersect)
                        {
                            intersect = true;
                            if (DrawKnightCells(player.Knight[i]) && mouse.LeftButton == ButtonState.Pressed)
                            {
                                Dot.Token = player.Knight[i];
                                Dot.point.Texture = Dot.pointcollect[1].Texture;
                                Dot.Board[player.Knight[i].X][player.Knight[i].Y].ID = "";
                                move = true; id = 4; Asist = player.Knight[i]; break;
                            }
                            else Dot.point.Texture = Dot.pointcollect[0].Texture;
                            break;
                        }
                    }
                    #endregion

                    #region Bishop
                    for (int i = 0; i < 2; i++)
                    {
                        if (Dot.point.Size.Intersects(player.Bishop[i].Size) && player.Bishop[i].Visible && !intersect)
                        {
                            intersect = true;
                            if (DrawCellsinDiagonal(player.Bishop[i]) && mouse.LeftButton == ButtonState.Pressed)
                            {
                                Dot.Token = player.Bishop[i];
                                Dot.point.Texture = Dot.pointcollect[1].Texture;
                                Dot.Board[player.Bishop[i].X][player.Bishop[i].Y].ID = "";
                                move = true; id = 3; Asist = player.Bishop[i]; break;
                            }
                            else Dot.point.Texture = Dot.pointcollect[0].Texture;
                            break;
                        }
                    }
                    #endregion

                    #region Queen
                    if (Dot.point.Size.Intersects(player.Queen.Size) && player.Queen.Visible && !intersect)
                    {
                        intersect = true;
                        bool Crux = DrawCellsinCrux(player.Queen);
                        bool Diagonal = DrawCellsinDiagonal(player.Queen);
                        bool Pass = Diagonal || Crux;

                        if (mouse.LeftButton == ButtonState.Pressed && Pass)
                        {
                            Dot.Token = player.Queen;
                            Dot.point.Texture = Dot.pointcollect[1].Texture;
                            Dot.Board[player.Queen.X][player.Queen.Y].ID = "";
                            move = true; id = 1; Asist = player.Queen;
                        }
                        else Dot.point.Texture = Dot.pointcollect[0].Texture;
                    }
                    #endregion
                }

                #region Check Mate
                if (!checKing(player.King.X, player.King.Y, 7))
                {
                    if (!Stage.check)
                        Stage.Begin("CHECK...".ToCharArray());
                    Stage.check = true;
                }
                #endregion /Animation/
                intersect = false;
            }
            //If the player has any Token 
            else Move(Asist, mouse, id);
            Dot.point.Size.X = mouse.X;
            Dot.point.Size.Y = mouse.Y;
        }
        private static void Move(Chess Token, MouseState mouse, int model)
        {
            Token.Size.X = mouse.X - Player.width / 2;
            Token.Size.Y = mouse.Y - Player.height / 2;
            switch (model)
            {
                case 0:
                    #region pawns move
                    if (Player.Rol)
                    {
                        for (int a = Token.Y - 1; a >= Token.Y - (Token.initial ? 2 : 1) && Dot.Board[Token.X][a].ID == ""; a--)
                        {
                            if (Dot.point.Size.Intersects(Dot.Board[Token.X][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                            {
                                Token.Size = new Rectangle(Token.X * Player.X + Player.W, a * Player.Y, Player.width, Player.height);
                                if (mouse.LeftButton == ButtonState.Pressed)
                                {
                                    process(Token.X, Token.Y = a, Token);
                                    Token.initial = false;
                                }
                                break;
                            }
                        }
                        //Chewing token X - 1, Y - 1
                        if (Token.X - 1 > -1 && Token.Y - 1 > -1 && Dot.point.Size.Intersects(Dot.Board[Token.X - 1][Token.Y - 1].Size)
                            && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size) && Dot.Board[Token.X - 1][Token.Y - 1].ID.Contains(enemy))
                        {
                            Token.Size = new Rectangle((Token.X - 1) * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                            if (mouse.LeftButton == ButtonState.Pressed)
                            {
                                Token.Y--; Token.X--;
                                Dot.Board[Token.X][Token.Y].Token.Visible = false;
                                Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                                Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                                process(Token.X, Token.Y, Token);
                            }
                        }
                        //Chewing token X + 1, Y - 1
                        if (Token.X + 1 < 8 && Token.Y - 1 > -1 && Dot.point.Size.Intersects(Dot.Board[Token.X + 1][Token.Y - 1].Size)
                            && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size) && Dot.Board[Token.X + 1][Token.Y - 1].ID.Contains(enemy))
                        {
                            Token.Size = new Rectangle((Token.X + 1) * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                            if (mouse.LeftButton == ButtonState.Pressed)
                            {
                                Token.Y--; Token.X++;
                                Dot.Board[Token.X][Token.Y].Token.Visible = false;
                                Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                                Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                                process(Token.X, Token.Y, Token);
                            }
                        }
                    }
                    else
                    {
                        //black pawns
                        for (int a = Token.Y + 1; a <= Token.Y + (Token.initial ? 2 : 1) && Dot.Board[Token.X][a].ID == ""; a++)
                        {
                            if (Dot.point.Size.Intersects(Dot.Board[Token.X][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                            {
                                Token.Size = new Rectangle(Token.X * Player.X + Player.W, a * Player.Y, Player.width, Player.height);
                                if (mouse.LeftButton == ButtonState.Pressed)
                                {
                                    process(Token.X, Token.Y = a, Token);
                                    Token.initial = false;
                                }
                                break;
                            }
                        }
                        //Chewing token X + 1, Y + 1
                        if (Token.X + 1 < 8 && Token.Y + 1 < 8 && Dot.point.Size.Intersects(Dot.Board[Token.X + 1][Token.Y + 1].Size)
                            && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size) && Dot.Board[Token.X + 1][Token.Y + 1].ID.Contains(enemy))
                        {
                            Token.Size = new Rectangle((Token.X + 1) * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                            if (mouse.LeftButton == ButtonState.Pressed)
                            {
                                Token.Y++; Token.X++;
                                Dot.Board[Token.X][Token.Y].Token.Visible = false;
                                Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                                Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                                process(Token.X, Token.Y, Token);
                            }
                        }
                        //Chewing token X - 1, Y + 1
                        if (Token.X - 1 > -1 && Token.Y + 1 < 8 && Dot.point.Size.Intersects(Dot.Board[Token.X - 1][Token.Y + 1].Size)
                            && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size) && Dot.Board[Token.X - 1][Token.Y + 1].ID.Contains(enemy))
                        {
                            Token.Size = new Rectangle((Token.X - 1) * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                            if (mouse.LeftButton == ButtonState.Pressed)
                            {
                                Token.Y++; Token.X--;
                                Dot.Board[Token.X][Token.Y].Token.Visible = false;
                                Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                                Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                                process(Token.X, Token.Y, Token);
                            }
                        }
                    }
                    break;
                    #endregion
                case 1:
                    #region queen move
                    //seeking in Crux
                    Crux(Token, mouse);
                    //seeking in Diagonal
                    Diagonal(Token, mouse);
                    break;
                    #endregion
                case 2:
                    #region Rook move
                    //seeking in Crux
                    Crux(Token, mouse);
                    break;
                    #endregion
                case 3:
                    #region bishop move
                    //seeking in Diagonal
                    Diagonal(Token, mouse);
                    break;
                    #endregion
                case 4:
                    #region Knight move
                    //seeking in Knight
                    Knight(Token, mouse);
                    break;
                    #endregion
                case 5:
                    #region king move
                    //seeking in King
                    King(Token, mouse);
                    break;
                    #endregion
            }
        }
        #region Movements
        private static bool DrawCellsinCrux(Chess Token)
        {
            bool Pass = false;
            //seeking up
            eat = Token.Y - 1;
            for (int a = Token.Y - 1; a > -1 && Dot.Board[Token.X][a].ID == ""; a--)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][a].Color = Color.SpringGreen;
                eat = a - 1;
            }
            //if it'll eat
            if (eat > -1 && Dot.Board[Token.X][eat].ID.Contains(enemy)) { Pass = true; Dot.Board[Token.X][eat].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }
            //seeking down
            eat = Token.Y + 1;
            for (int a = Token.Y + 1; a < 8 && Dot.Board[Token.X][a].ID == ""; a++)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][a].Color = Color.SpringGreen;
                eat = a + 1;
            }
            //if it'll eat
            if (eat < 8 && Dot.Board[Token.X][eat].ID.Contains(enemy)) { Pass = true; Dot.Board[Token.X][eat].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }
            //seeking left
            eat = Token.X - 1;
            for (int a = Token.X - 1; a > -1 && Dot.Board[a][Token.Y].ID == ""; a--)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[a][Token.Y].Color = Color.SpringGreen;
                eat = a - 1;
            }
            //if it'll eat
            if (eat > -1 && Dot.Board[eat][Token.Y].ID.Contains(enemy)) { Pass = true; Dot.Board[eat][Token.Y].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }
            //seeking right
            eat = Token.X + 1;
            for (int a = Token.X + 1; a < 8 && Dot.Board[a][Token.Y].ID == ""; a++)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[a][Token.Y].Color = Color.SpringGreen;
                eat = a + 1;
            }
            //if it'll eat
            if (eat < 8 && Dot.Board[eat][Token.Y].ID.Contains(enemy)) { Pass = true; Dot.Board[eat][Token.Y].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }

            return Pass;
        }
        private static bool DrawCellsinDiagonal(Chess Token)
        {
            bool Pass = false;
            //seeking diagonal top left
            diago = Z = Token.X - 1; eat = Token.Y - 1;
            for (int a = Token.Y - 1; a > -1 && Z > -1 && Dot.Board[Z][a].ID == ""; a--)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Z][a].Color = Color.SpringGreen;
                Z--; eat = a - 1; diago = Z;
            }
            //if it'll eat
            if (diago > -1 && eat > -1 && Dot.Board[diago][eat].ID.Contains(enemy)) { Pass = true; Dot.Board[diago][eat].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }

            //seeking diagonal bottom left
            diago = Z = Token.X - 1; eat = Token.Y + 1;
            for (int a = Token.Y + 1; a < 8 && Z > -1 && Dot.Board[Z][a].ID == ""; a++)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Z][a].Color = Color.SpringGreen;
                Z--; eat = a + 1; diago = Z;
            }
            //if it'll eat
            if (diago > -1 && eat < 8 && Dot.Board[diago][eat].ID.Contains(enemy)) { Pass = true; Dot.Board[diago][eat].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }

            //seeking diagonal top right
            diago = Z = Token.X + 1; eat = Token.Y - 1;
            for (int a = Token.Y - 1; a > -1 && Z < 8 && Dot.Board[Z][a].ID == ""; a--)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Z][a].Color = Color.SpringGreen;
                Z++; eat = a - 1; diago = Z;
            }
            //if it'll eat
            if (diago < 8 && eat > -1 && Dot.Board[diago][eat].ID.Contains(enemy)) { Pass = true; Dot.Board[diago][eat].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }

            //seeking diagonal bottom right
            diago = Z = Token.X + 1; eat = Token.Y + 1;
            for (int a = Token.Y + 1; a < 8 && Z < 8 && Dot.Board[Z][a].ID == ""; a++)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Z][a].Color = Color.SpringGreen;
                Z++; eat = a + 1; diago = Z;
            }
            //if it'll eat
            if (diago < 8 && eat < 8 && Dot.Board[diago][eat].ID.Contains(enemy)) { Pass = true; Dot.Board[diago][eat].Color = Color.Red; Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue; }
            return Pass;
        }
        private static bool DrawUpPawnCells(Chess Token)
        {
            bool Pass = false;
            if (Token.X - 1 > -1 && Token.Y - 1 > -1 && Dot.Board[Token.X - 1][Token.Y - 1].ID.Contains(enemy))
            {
                Pass = true; Dot.Board[Token.X - 1][Token.Y - 1].Color = Color.Red;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
            }
            if (Token.X + 1 < 8 && Token.Y - 1 > -1 && Dot.Board[Token.X + 1][Token.Y - 1].ID.Contains(enemy))
            {
                Pass = true; Dot.Board[Token.X + 1][Token.Y - 1].Color = Color.Red;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
            }

            for (int a = Token.Y - 1; a > -1 && a >= Token.Y - (Token.initial ? 2 : 1) && Dot.Board[Token.X][a].ID == ""; a--)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][a].Color = Color.SpringGreen;
            }
            return Pass;
        }
        private static bool DrawDownPawnCells(Chess Token)
        {
            bool Pass = false;
            if (Token.X - 1 > -1 && Token.Y + 1 < 8 && Dot.Board[Token.X - 1][Token.Y + 1].ID.Contains(enemy))
            {
                Pass = true; Dot.Board[Token.X - 1][Token.Y + 1].Color = Color.Red;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
            }
            if (Token.X + 1 < 8 && Token.Y + 1 < 8 && Dot.Board[Token.X + 1][Token.Y + 1].ID.Contains(enemy))
            {
                Pass = true; Dot.Board[Token.X + 1][Token.Y + 1].Color = Color.Red;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
            }
            for (int a = Token.Y + 1; a < 8 && a <= Token.Y + (Token.initial ? 2 : 1) && Dot.Board[Token.X][a].ID == ""; a++)
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][a].Color = Color.SpringGreen;
            }
            return Pass;
        }
        private static bool DrawKnightCells(Chess Token)
        {
            bool Pass = false;
            //X top left
            if (Token.X - 2 > -1 && Token.Y - 1 > -1 && Dot.Board[Token.X - 2][Token.Y - 1].ID == "" | Dot.Board[Token.X - 2][Token.Y - 1].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X - 2][Token.Y - 1].ID.Contains(enemy)) Dot.Board[Token.X - 2][Token.Y - 1].Color = Color.Red;
                else Dot.Board[Token.X - 2][Token.Y - 1].Color = Color.SpringGreen;
            }
            //Y top left
            if (Token.X - 1 > -1 && Token.Y - 2 > -1 && Dot.Board[Token.X - 1][Token.Y - 2].ID == "" | Dot.Board[Token.X - 1][Token.Y - 2].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X - 1][Token.Y - 2].ID.Contains(enemy)) Dot.Board[Token.X - 1][Token.Y - 2].Color = Color.Red;
                else Dot.Board[Token.X - 1][Token.Y - 2].Color = Color.SpringGreen;
            }
            //X top right
            if (Token.X + 2 < 8 && Token.Y - 1 > -1 && Dot.Board[Token.X + 2][Token.Y - 1].ID == "" | Dot.Board[Token.X + 2][Token.Y - 1].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X + 2][Token.Y - 1].ID.Contains(enemy)) Dot.Board[Token.X + 2][Token.Y - 1].Color = Color.Red;
                else Dot.Board[Token.X + 2][Token.Y - 1].Color = Color.SpringGreen;
            }
            //Y top right
            if (Token.X + 1 < 8 && Token.Y - 2 > -1 && Dot.Board[Token.X + 1][Token.Y - 2].ID == "" | Dot.Board[Token.X + 1][Token.Y - 2].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X + 1][Token.Y - 2].ID.Contains(enemy)) Dot.Board[Token.X + 1][Token.Y - 2].Color = Color.Red;
                else Dot.Board[Token.X + 1][Token.Y - 2].Color = Color.SpringGreen;
            }
            //Y bottom left
            if (Token.X - 1 > -1 && Token.Y + 2 < 8 && Dot.Board[Token.X - 1][Token.Y + 2].ID == "" | Dot.Board[Token.X - 1][Token.Y + 2].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X - 1][Token.Y + 2].ID.Contains(enemy)) Dot.Board[Token.X - 1][Token.Y + 2].Color = Color.Red;
                else Dot.Board[Token.X - 1][Token.Y + 2].Color = Color.SpringGreen;
            }
            //X bottom left
            if (Token.X - 2 > -1 && Token.Y + 1 < 8 && Dot.Board[Token.X - 2][Token.Y + 1].ID == "" | Dot.Board[Token.X - 2][Token.Y + 1].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X - 2][Token.Y + 1].ID.Contains(enemy)) Dot.Board[Token.X - 2][Token.Y + 1].Color = Color.Red;
                else Dot.Board[Token.X - 2][Token.Y + 1].Color = Color.SpringGreen;
            }
            //Y bottom right
            if (Token.X + 1 < 8 && Token.Y + 2 < 8 && Dot.Board[Token.X + 1][Token.Y + 2].ID == "" | Dot.Board[Token.X + 1][Token.Y + 2].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X + 1][Token.Y + 2].ID.Contains(enemy)) Dot.Board[Token.X + 1][Token.Y + 2].Color = Color.Red;
                else Dot.Board[Token.X + 1][Token.Y + 2].Color = Color.SpringGreen;
            }
            //X bottom right
            if (Token.X + 2 < 8 && Token.Y + 1 < 8 && Dot.Board[Token.X + 2][Token.Y + 1].ID == "" | Dot.Board[Token.X + 2][Token.Y + 1].ID.Contains(enemy))
            {
                Pass = true;
                Token.Color = Color.Wheat;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                if (Dot.Board[Token.X + 2][Token.Y + 1].ID.Contains(enemy)) Dot.Board[Token.X + 2][Token.Y + 1].Color = Color.Red;
                else Dot.Board[Token.X + 2][Token.Y + 1].Color = Color.SpringGreen;
            }
            return Pass;
        }
        private static bool DrawKingCells(Chess Token)
        {
            bool Pass = false;
            //right
            for (int i = Token.X + 1; i <= (Castling(Token.Y, 1) ? Token.X + 2 : Token.X + 1); i++)
                if (i < 8 && Dot.Board[i][Token.Y].ID == "" | Dot.Board[i][Token.Y].ID.Contains(enemy) && checKing(i, Token.Y, 7))
                {
                    Pass = true;
                    Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                    Token.Color = Color.Wheat;
                    if (Dot.Board[i][Token.Y].ID.Contains(enemy)) Dot.Board[i][Token.Y].Color = Color.Red;
                    else Dot.Board[i][Token.Y].Color = Color.SpringGreen;
                }
            //left
            for (int i = Token.X - 1; i >= (Castling(Token.Y, 0) ? Token.X - 2 : Token.X - 1); i--)
                if (i > -1 && Dot.Board[i][Token.Y].ID == "" | Dot.Board[i][Token.Y].ID.Contains(enemy) && checKing(i, Token.Y, 7))
                {
                    Pass = true;
                    Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                    Token.Color = Color.Wheat;
                    if (Dot.Board[i][Token.Y].ID.Contains(enemy)) Dot.Board[i][Token.Y].Color = Color.Red;
                    else Dot.Board[i][Token.Y].Color = Color.SpringGreen;
                }
            //down
            if (Token.Y + 1 < 8 && Dot.Board[Token.X][Token.Y + 1].ID == "" | Dot.Board[Token.X][Token.Y + 1].ID.Contains(enemy) && checKing(Token.X, Token.Y + 1, 7))
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                if (Dot.Board[Token.X][Token.Y + 1].ID.Contains(enemy)) Dot.Board[Token.X][Token.Y + 1].Color = Color.Red;
                else Dot.Board[Token.X][Token.Y + 1].Color = Color.SpringGreen;
            }
            //up
            if (Token.Y - 1 > -1 && Dot.Board[Token.X][Token.Y - 1].ID == "" | Dot.Board[Token.X][Token.Y - 1].ID.Contains(enemy) && checKing(Token.X, Token.Y - 1, 7))
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                if (Dot.Board[Token.X][Token.Y - 1].ID.Contains(enemy)) Dot.Board[Token.X][Token.Y - 1].Color = Color.Red;
                else Dot.Board[Token.X][Token.Y - 1].Color = Color.SpringGreen;
            }
            //diagonal bottom left
            if (Token.X - 1 > -1 && Token.Y + 1 < 8 && Dot.Board[Token.X - 1][Token.Y + 1].ID == "" | Dot.Board[Token.X - 1][Token.Y + 1].ID.Contains(enemy) && checKing(Token.X - 1, Token.Y + 1, 7))
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                if (Dot.Board[Token.X - 1][Token.Y + 1].ID.Contains(enemy)) Dot.Board[Token.X - 1][Token.Y + 1].Color = Color.Red;
                else Dot.Board[Token.X - 1][Token.Y + 1].Color = Color.SpringGreen;
            }
            //diagonal top right
            if (Token.X + 1 < 8 && Token.Y - 1 > -1 && Dot.Board[Token.X + 1][Token.Y - 1].ID == "" | Dot.Board[Token.X + 1][Token.Y - 1].ID.Contains(enemy) && checKing(Token.X + 1, Token.Y - 1, 7))
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                if (Dot.Board[Token.X + 1][Token.Y - 1].ID.Contains(enemy)) Dot.Board[Token.X + 1][Token.Y - 1].Color = Color.Red;
                else Dot.Board[Token.X + 1][Token.Y - 1].Color = Color.SpringGreen;
            }
            //diagonal bottom right
            if (Token.X + 1 < 8 && Token.Y + 1 < 8 && Dot.Board[Token.X + 1][Token.Y + 1].ID == "" | Dot.Board[Token.X + 1][Token.Y + 1].ID.Contains(enemy) && checKing(Token.X + 1, Token.Y + 1, 7))
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                if (Dot.Board[Token.X + 1][Token.Y + 1].ID.Contains(enemy)) Dot.Board[Token.X + 1][Token.Y + 1].Color = Color.Red;
                else Dot.Board[Token.X + 1][Token.Y + 1].Color = Color.SpringGreen;
            }
            //diagonal top left
            if (Token.X - 1 > -1 && Token.Y - 1 > -1 && Dot.Board[Token.X - 1][Token.Y - 1].ID == "" | Dot.Board[Token.X - 1][Token.Y - 1].ID.Contains(enemy) && checKing(Token.X - 1, Token.Y - 1, 7))
            {
                Pass = true;
                Dot.Board[Token.X][Token.Y].Color = Color.SkyBlue;
                Token.Color = Color.Wheat;
                if (Dot.Board[Token.X - 1][Token.Y - 1].ID.Contains(enemy)) Dot.Board[Token.X - 1][Token.Y - 1].Color = Color.Red;
                else Dot.Board[Token.X - 1][Token.Y - 1].Color = Color.SpringGreen;
            }
            return Pass;
        }
        private static void Crux(Chess Token, MouseState mouse)
        {
            //seeking up
            eat = Token.Y - 1;
            for (int a = Token.Y - 1; a > -1 && Dot.Board[Token.X][a].ID == ""; a--)
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Token.X * Player.X + Player.W, a * Player.Y, Player.width, Player.height);
                    intersect = true;
                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X, Token.Y = a, Token);
                    return;
                }
                eat = a - 1;
            }
            // eating up
            if (eat > -1 && Dot.Board[Token.X][eat].ID.Contains(enemy) && Dot.point.Size.Intersects(Dot.Board[Token.X][eat].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(Token.X * Player.X + Player.W, eat * Player.Y, Player.width, Player.height); ;
                intersect = true;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Dot.Board[Token.X][eat].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[Token.X][eat].Token.Texture, Dot.Board[Token.X][eat].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[Token.X][eat].ID);
                    process(Token.X, Token.Y = eat, Token);
                }
                return;
            }
            //seeking down
            eat = Token.Y + 1;
            for (int a = Token.Y + 1; a < 8 && Dot.Board[Token.X][a].ID == ""; a++)
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Token.X * Player.X + Player.W, a * Player.Y, Player.width, Player.height);
                    intersect = true;
                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X, Token.Y = a, Token);
                    return;
                }
                eat = a + 1;
            }
            // eating  down
            if (eat < 8 && Dot.Board[Token.X][eat].ID.Contains(enemy) && Dot.point.Size.Intersects(Dot.Board[Token.X][eat].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(Token.X * Player.X + Player.W, eat * Player.Y, Player.width, Player.height); ;
                intersect = true;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Dot.Board[Token.X][eat].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[Token.X][eat].Token.Texture, Dot.Board[Token.X][eat].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[Token.X][eat].ID);
                    process(Token.X, Token.Y = eat, Token);
                }
                return;
            }
            //seeking right
            eat = Token.X + 1;
            for (int a = Token.X + 1; a < 8 && Dot.Board[a][Token.Y].ID == ""; a++)
            {
                if (Dot.point.Size.Intersects(Dot.Board[a][Token.Y].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(a * Player.X + Player.W, Player.Y * Token.Y, Player.width, Player.height);
                    intersect = true;
                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X = a, Token.Y, Token);
                    return;
                }
                eat = a + 1;
            }
            // eating right
            if (eat < 8 && Dot.Board[eat][Token.Y].ID.Contains(enemy) && Dot.point.Size.Intersects(Dot.Board[eat][Token.Y].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(eat * Player.X + Player.W, Token.Y * Player.Y, Player.width, Player.height);

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Dot.Board[eat][Token.Y].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[eat][Token.Y].Token.Texture, Dot.Board[eat][Token.Y].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[eat][Token.Y].ID);
                    process(Token.X = eat, Token.Y, Token);
                }
                return;
            }
            //seeking left
            eat = Token.X - 1;
            for (int a = Token.X - 1; a > -1 && Dot.Board[a][Token.Y].ID == ""; a--)
            {
                if (Dot.point.Size.Intersects(Dot.Board[a][Token.Y].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(a * Player.X + Player.W, Player.Y * Token.Y, Player.width, Player.height);
                    intersect = true;
                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X = a, Token.Y, Token);
                    return;
                }
                eat = a - 1;
            }
            // eating left
            if (eat > -1 && Dot.Board[eat][Token.Y].ID.Contains(enemy) && Dot.point.Size.Intersects(Dot.Board[eat][Token.Y].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(eat * Player.X + Player.W, Token.Y * Player.Y, Player.width, Player.height);
                intersect = true;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Dot.Board[eat][Token.Y].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[eat][Token.Y].Token.Texture, Dot.Board[eat][Token.Y].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[eat][Token.Y].ID);
                    process(Token.X = eat, Token.Y, Token);
                }
                return;
            }
        }
        private static void Diagonal(Chess Token, MouseState mouse)
        {
            //seeking diagonal top right
            diago = Z = Token.X + 1; eat = Token.Y - 1;
            for (int a = Token.Y - 1; a > -1 && Z < 8 && Dot.Board[Z][a].ID == ""; a--)
            {
                if (Dot.point.Size.Intersects(Dot.Board[Z][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Z * Player.X + Player.W, a * Player.Y, Player.width, Player.height);

                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X = Z, Token.Y = a, Token);
                    return;
                }
                Z++; diago = Z; eat = a - 1;
            }
            //eating diagonal top right
            if (diago < 8 && eat > -1 && Dot.Board[diago][eat].ID.Contains(enemy) &&
            Dot.point.Size.Intersects(Dot.Board[diago][eat].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(diago * Player.X + Player.W, eat * Player.Y, Player.width, Player.height);

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Token.Y = eat; Token.X = diago;
                    Dot.Board[Token.X][Token.Y].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                    process(Token.X, Token.Y, Token);
                }
                return;
            }
            //seeking diagonal bottom right
            diago = Z = Token.X + 1; eat = Token.Y + 1;
            for (int a = Token.Y + 1; a < 8 && Z < 8 && Dot.Board[Z][a].ID == ""; a++)
            {
                if (Dot.point.Size.Intersects(Dot.Board[Z][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Z * Player.X + Player.W, a * Player.Y, Player.width, Player.height);

                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X = Z, Token.Y = a, Token);
                    return;
                }
                Z++; diago = Z; eat = a + 1;
            }
            //eating diagonal bottom right
            if (diago < 8 && eat < 8 && Dot.Board[diago][eat].ID.Contains(enemy) &&
            Dot.point.Size.Intersects(Dot.Board[diago][eat].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(diago * Player.X + Player.W, eat * Player.Y, Player.width, Player.height);

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Token.Y = eat; Token.X = diago;
                    Dot.Board[Token.X][Token.Y].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                    process(Token.X, Token.Y, Token);
                }
                return;
            }
            //seeking diagonal top left
            diago = Z = Token.X - 1; eat = Token.Y - 1;
            for (int a = Token.Y - 1; a > -1 && Z > -1 && Dot.Board[Z][a].ID == ""; a--)
            {
                if (Dot.point.Size.Intersects(Dot.Board[Z][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Z * Player.X + Player.W, a * Player.Y, Player.width, Player.height);

                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X = Z, Token.Y = a, Token);
                    return;
                }
                Z--; diago = Z; eat = a - 1;
            }
            //eating diagonal top left
            if (diago > -1 && eat > -1 && Dot.Board[diago][eat].ID.Contains(enemy) &&
            Dot.point.Size.Intersects(Dot.Board[diago][eat].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(diago * Player.X + Player.W, eat * Player.Y, Player.width, Player.height);

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Token.Y = eat; Token.X = diago;
                    Dot.Board[Token.X][Token.Y].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                    process(Token.X, Token.Y, Token);
                }
                return;
            }
            //seeking diagonal bottom left
            diago = Z = Token.X - 1; eat = Token.Y + 1;
            for (int a = Token.Y + 1; a < 8 && Z > -1 && Dot.Board[Z][a].ID == ""; a++)
            {
                if (Dot.point.Size.Intersects(Dot.Board[Z][a].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Z * Player.X + Player.W, a * Player.Y, Player.width, Player.height);

                    if (mouse.LeftButton == ButtonState.Pressed)
                        process(Token.X = Z, Token.Y = a, Token);
                    return;
                }
                Z--; diago = Z; eat = a + 1;
            }
            //eating diagonal bottom left
            if (diago > -1 && eat < 8 && Dot.Board[diago][eat].ID.Contains(enemy) &&
            Dot.point.Size.Intersects(Dot.Board[diago][eat].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
            {
                Token.Size = new Rectangle(diago * Player.X + Player.W, eat * Player.Y, Player.width, Player.height);

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Token.Y = eat; Token.X = diago;
                    Dot.Board[Token.X][Token.Y].Token.Visible = false;
                    Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                    Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                    process(Token.X, Token.Y, Token);
                }
                return;
            }
        }
        private static void Knight(Chess Token, MouseState mouse)
        {
            //X buttom right
            if (Token.X + 2 < 8 && Token.Y + 1 < 8 && Dot.Board[Token.X + 2][Token.Y + 1].ID == "" | Dot.Board[Token.X + 2][Token.Y + 1].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X + 2][Token.Y + 1].Size))
                {
                    Token.Size = new Rectangle((Token.X + 2) * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X += 2; Token.Y++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //Y bottom right
            if (Token.X + 1 < 8 && Token.Y + 2 < 8 && Dot.Board[Token.X + 1][Token.Y + 2].ID == "" | Dot.Board[Token.X + 1][Token.Y + 2].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X + 1][Token.Y + 2].Size))
                {
                    Token.Size = new Rectangle((Token.X + 1) * Player.X + Player.W, (Token.Y + 2) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.Y += 2; Token.X++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //X bottom left
            if (Token.X - 2 > -1 && Token.Y + 1 < 8 && Dot.Board[Token.X - 2][Token.Y + 1].ID == "" | Dot.Board[Token.X - 2][Token.Y + 1].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X - 2][Token.Y + 1].Size))
                {
                    Token.Size = new Rectangle((Token.X - 2) * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X -= 2; Token.Y++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //Y bottom left
            if (Token.X - 1 > -1 && Token.Y + 2 < 8 && Dot.Board[Token.X - 1][Token.Y + 2].ID == "" | Dot.Board[Token.X - 1][Token.Y + 2].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X - 1][Token.Y + 2].Size))
                {
                    Token.Size = new Rectangle((Token.X - 1) * Player.X + Player.W, (Token.Y + 2) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.Y += 2; Token.X--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //X top right
            if (Token.X + 2 < 8 && Token.Y - 1 > -1 && Dot.Board[Token.X + 2][Token.Y - 1].ID == "" | Dot.Board[Token.X + 2][Token.Y - 1].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X + 2][Token.Y - 1].Size))
                {
                    Token.Size = new Rectangle((Token.X + 2) * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X += 2; Token.Y--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //Y top right
            if (Token.X + 1 < 8 && Token.Y - 2 > -1 && Dot.Board[Token.X + 1][Token.Y - 2].ID == "" | Dot.Board[Token.X + 1][Token.Y - 2].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X + 1][Token.Y - 2].Size))
                {
                    Token.Size = new Rectangle((Token.X + 1) * Player.X + Player.W, (Token.Y - 2) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.Y -= 2; Token.X++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //Y top left
            if (Token.X - 1 > -1 && Token.Y - 2 > -1 && Dot.Board[Token.X - 1][Token.Y - 2].ID == "" | Dot.Board[Token.X - 1][Token.Y - 2].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X - 1][Token.Y - 2].Size))
                {
                    Token.Size = new Rectangle((Token.X - 1) * Player.X + Player.W, (Token.Y - 2) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.Y -= 2; Token.X--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //X top left
            if (Token.X - 2 > -1 && Token.Y - 1 > -1 && Dot.Board[Token.X - 2][Token.Y - 1].ID == "" | Dot.Board[Token.X - 2][Token.Y - 1].ID.Contains(enemy))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X - 2][Token.Y - 1].Size))
                {
                    Token.Size = new Rectangle((Token.X - 2) * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X -= 2; Token.Y--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
        }
        private static void King(Chess Token, MouseState mouse)
        {
            //up
            if (Token.Y - 1 > -1 && Dot.Board[Token.X][Token.Y - 1].ID == "" | Dot.Board[Token.X][Token.Y - 1].ID.Contains(enemy) && checKing(Token.X, Token.Y - 1, 7))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y - 1].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Token.X * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.Y--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //down
            if (Token.Y + 1 < 8 && Dot.Board[Token.X][Token.Y + 1].ID == "" | Dot.Board[Token.X][Token.Y + 1].ID.Contains(enemy) && checKing(Token.X, Token.Y + 1, 7))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y + 1].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle(Token.X * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.Y++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //left
            for (int i = Token.X - 1; i >= (Castling(Token.Y, 0) ? Token.X - 2 : Token.X - 1); i--)
                if (i > -1 && Dot.Board[i][Token.Y].ID == "" | Dot.Board[i][Token.Y].ID.Contains(enemy) && checKing(i, Token.Y, 7))
                {
                    if (Dot.point.Size.Intersects(Dot.Board[i][Token.Y].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                    {
                        Token.Size = new Rectangle(i * Player.X + Player.W, Token.Y * Player.Y, Player.width, Player.height);
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            if (i == Token.X - 2) Fitup(0, 3);
                            Token.X = i;
                            if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                            {
                                Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                                Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                            }
                            process(Token.X, Token.Y, Token);
                        }
                    }
                }
            //right
            for (int i = Token.X + 1; i <= (Castling(Token.Y, 1) ? Token.X + 2 : Token.X + 1); i++)
                if (i < 8 && Dot.Board[i][Token.Y].ID == "" | Dot.Board[i][Token.Y].ID.Contains(enemy) && checKing(i, Token.Y, 7))
                {
                    if (Dot.point.Size.Intersects(Dot.Board[i][Token.Y].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                    {
                        Token.Size = new Rectangle(i * Player.X + Player.W, Token.Y * Player.Y, Player.width, Player.height);
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            if (i == Token.X + 2) Fitup(1, 5);
                            Token.X = i;
                            if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                            {
                                Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                                Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                            }
                            process(Token.X, Token.Y, Token);
                        }
                    }
                }
            //diagonal top left
            if (Token.X - 1 > -1 && Token.Y - 1 > -1 && Dot.Board[Token.X - 1][Token.Y - 1].ID == "" | Dot.Board[Token.X - 1][Token.Y - 1].ID.Contains(enemy) && checKing(Token.X - 1, Token.Y - 1, 7))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X - 1][Token.Y - 1].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle((Token.X - 1) * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X--; Token.Y--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //diagonal top right
            if (Token.X + 1 < 8 && Token.Y - 1 > -1 && Dot.Board[Token.X + 1][Token.Y - 1].ID == "" | Dot.Board[Token.X + 1][Token.Y - 1].ID.Contains(enemy) && checKing(Token.X + 1, Token.Y - 1, 7))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X + 1][Token.Y - 1].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle((Token.X + 1) * Player.X + Player.W, (Token.Y - 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X++; Token.Y--;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //diagonal bottom left
            if (Token.X - 1 > -1 && Token.Y + 1 < 8 && Dot.Board[Token.X - 1][Token.Y + 1].ID == "" | Dot.Board[Token.X - 1][Token.Y + 1].ID.Contains(enemy) && checKing(Token.X - 1, Token.Y + 1, 7))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X - 1][Token.Y + 1].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle((Token.X - 1) * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X--; Token.Y++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
            //diagonal bottom right
            if (Token.X + 1 < 8 && Token.Y + 1 < 8 && Dot.Board[Token.X + 1][Token.Y + 1].ID == "" | Dot.Board[Token.X + 1][Token.Y + 1].ID.Contains(enemy) && checKing(Token.X + 1, Token.Y + 1, 7))
            {
                if (Dot.point.Size.Intersects(Dot.Board[Token.X + 1][Token.Y + 1].Size) && !Dot.point.Size.Intersects(Dot.Board[Token.X][Token.Y].Size))
                {
                    Token.Size = new Rectangle((Token.X + 1) * Player.X + Player.W, (Token.Y + 1) * Player.Y, Player.width, Player.height);
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Token.X++; Token.Y++;
                        if (Dot.Board[Token.X][Token.Y].ID.Contains(enemy))
                        {
                            Dot.Board[Token.X][Token.Y].Token.Visible = false; Player.Earn += score(Dot.Board[Token.X][Token.Y].ID);
                            Stage.EarnedToken(Dot.Board[Token.X][Token.Y].Token.Texture, Dot.Board[Token.X][Token.Y].ID.Contains("2") ? 0 : 1);
                        }
                        process(Token.X, Token.Y, Token);
                    }
                }
            }
        }
        private static bool Castling(int Y, int Xpos)
        {
            int cast;
            if (Xpos == 0)
            {
                if (Player.King.castle && Player.Rook[0].castle)
                {
                    for (cast = 3; cast > 0; cast--)
                        if (Dot.Board[cast][Y].ID != "") break;
                    return cast == 0;
                }
            }
            else
            {
                if (Player.King.castle && Player.Rook[1].castle)
                {
                    for (cast = 5; cast < 7; cast++)
                        if (Dot.Board[cast][Y].ID != "") break;
                    return cast == 7;
                }
            }
            return false;
        }
        #endregion
        private static float score(string ID)
        {
            if (ID.Contains("Pawn")) return 2.6f;
            else if (ID.Contains("Bishop")) return 4.6f;
            else if (ID.Contains("Rook")) return 4.9f;
            else if (ID.Contains("Knight")) return 5.2f;
            else if (ID.Contains("Queen")) return 50;
            return 0;
        }
        private static void process(int x, int y, Chess Token)
        {
            if (Token.ID.Contains("Rook") || Token.ID.Contains("King")) Token.castle = false;
            Dot.Board[x][y].ID = Token.ID;
            Dot.Board[x][y].Token = Token;
            Dot.point.Texture = Dot.pointcollect[0].Texture;
            move = false; Turn = !Turn; pal = true;
            Clear(Color);
        }
        private static void Fitup(int Nrook, int pos)
        {
            Dot.Board[Player.Rook[Nrook].X][Player.Rook[Nrook].Y].ID = "";
            Player.Rook[Nrook].Size = new Rectangle(pos * Player.X + Player.W, Player.Y * Player.Rook[Nrook].Y, Player.width, Player.height);
            Player.Rook[Nrook].X = pos;
            Dot.Board[pos][Player.Rook[Nrook].Y].ID = Player.Rook[Nrook].ID;
            Dot.Board[pos][Player.Rook[Nrook].Y].Token = Player.Rook[Nrook];
        }
        private static bool checKing(int x, int y, int dir)
        {
            char pl = enemy == '1' ? '2' : '1';
            bool check = false, check2 = false;
            switch (dir)
            {
                case 0:
                    //Rows
                    if (x + 1 == 8 || x == 8) check = true;
                    for (int i = x + 1; i < 8; i++)
                    {
                        if (Dot.Board[i][y].ID == pl + "King") i++;
                        if (i == 8) { check = true; break; }
                        else if (Dot.Board[i][y].ID.Contains(enemy + "Rook")) { check = false; break; }
                        else if (Dot.Board[i][y].ID.Contains(enemy + "Queen")) { check = false; break; }
                        else if (Dot.Board[i][y].ID.Contains(pl) || Dot.Board[i][y].ID.Contains(enemy)) { check = true; break; }
                        if (i + 1 == 8) check = true;
                    }
                    if (x - 1 == -1 || x == -1) check2 = true;
                    for (int i = x - 1; i > -1; i--)
                    {
                        if (Dot.Board[i][y].ID == pl + "King") i--;
                        if (i == -1) { check2 = true; break; }
                        if (Dot.Board[i][y].ID.Contains(enemy + "Rook")) { check2 = false; break; }
                        else if (Dot.Board[i][y].ID.Contains(enemy + "Queen")) { check2 = false; break; }
                        else if (Dot.Board[i][y].ID.Contains(pl) || Dot.Board[i][y].ID.Contains(enemy)) { check2 = true; break; }
                        if (i - 1 == -1) check2 = true;
                    }
                    if (check && check2) return true;
                    break;
                case 1:
                    //Columns
                    if (y + 1 == 8 || y == 8) check = true;
                    for (int i = y + 1; i < 8; i++)
                    {
                        if (Dot.Board[x][i].ID == pl + "King") i++;
                        if (i == 8) { check = true; break; }
                        if (Dot.Board[x][i].ID.Contains(enemy + "Rook")) { check = false; break; }
                        else if (Dot.Board[x][i].ID.Contains(enemy + "Queen")) { check = false; break; }
                        else if (Dot.Board[x][i].ID.Contains(pl) || Dot.Board[x][i].ID.Contains(enemy)) { check = true; break; }
                        if (i + 1 == 8) check = true;
                    }
                    if (y - 1 == -1 || y == -1) check2 = true;
                    for (int i = y - 1; i > -1; i--)
                    {
                        if (Dot.Board[x][i].ID == pl + "King") i--;
                        if (i == -1) { check2 = true; break; }
                        if (Dot.Board[x][i].ID.Contains(enemy + "Rook")) { check2 = false; break; }
                        else if (Dot.Board[x][i].ID.Contains(enemy + "Queen")) { check2 = false; break; }
                        else if (Dot.Board[x][i].ID.Contains(pl) || Dot.Board[x][i].ID.Contains(enemy)) { check2 = true; break; }
                        if (i - 1 == -1) check2 = true;
                    }
                    if (check && check2) return true;
                    break;
                case 2:
                    //Diagonal Right
                    Z = x - 1;
                    if (Z == -1 || y + 1 == 8 || x == -1 || y == 8) check = true;
                    for (int i = y + 1; i < 8 && Z > -1; i++)
                    {
                        if (Dot.Board[Z][i].ID == pl + "King") { i++; Z--; }
                        if (i == 8 || Z == -1) { check = true; break; }
                        if (Dot.Board[Z][i].ID.Contains(enemy + "Bishop")) { check = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(enemy + "Queen")) { check = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(pl) || Dot.Board[Z][i].ID.Contains(enemy)) { check = true; break; }
                        if (i + 1 == 8 || Z - 1 == -1) check = true;
                        Z--;
                    }
                    Z = x + 1;
                    if (Z == 8 || y - 1 == -1 || x == 8 || y == -1) check2 = true;
                    for (int i = y - 1; i > -1 && Z < 8; i--)
                    {
                        if (Dot.Board[Z][i].ID == pl + "King") { i--; Z++; }
                        if (i == -1 || Z == 8) { check2 = true; break; }
                        else if (Dot.Board[Z][i].ID.Contains(enemy + "Bishop")) { check2 = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(enemy + "Queen")) { check2 = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(pl) || Dot.Board[Z][i].ID.Contains(enemy)) { check2 = true; break; }
                        if (i - 1 == -1 || Z + 1 == 8) check2 = true;
                        Z++;
                    }
                    if (check && check2) return true;
                    break;
                case 3:
                    //Diagonal Left
                    Z = x + 1;
                    if (Z == 8 || y + 1 == 8 || x == 8 || y == 8) check = true;
                    for (int i = y + 1; i < 8 && Z < 8; i++)
                    {
                        if (Dot.Board[Z][i].ID == pl + "King") { i++; Z++; }
                        if (i == 8 || Z == 8) { check = true; break; }
                        else if (Dot.Board[Z][i].ID.Contains(enemy + "Bishop")) { check = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(enemy + "Queen")) { check = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(pl) || Dot.Board[Z][i].ID.Contains(enemy)) { check = true; break; }
                        if (i + 1 == 8 || Z + 1 == 8) check = true;
                        Z++;
                    }
                    Z = x - 1;
                    if (Z == -1 || y - 1 == -1 || x == -1 || y == -1) check2 = true;
                    for (int i = y - 1; i > -1 && Z > -1; i--)
                    {
                        if (Dot.Board[Z][i].ID == pl + "King") { i--; Z--; }
                        if (i == -1 || Z == -1) { check2 = true; break; }
                        if (Dot.Board[Z][i].ID.Contains(enemy + "Bishop")) { check2 = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(enemy + "Queen")) { check2 = false; break; }
                        else if (Dot.Board[Z][i].ID.Contains(pl) || Dot.Board[Z][i].ID.Contains(enemy)) { check2 = true; break; }
                        if (i - 1 == -1 || Z - 1 == -1) check2 = true;
                        Z--;
                    }
                    if (check && check2) return true;
                    break;
                case 4:
                    //Checking Pawns Threating
                    if (Player.Rol)
                    {
                        if (x - 1 > -1 && y - 1 > -1 && Dot.Board[x - 1][y - 1].ID.Contains(enemy + "Pawn")) return false;
                        else if (x + 1 < 8 && y - 1 > -1 && Dot.Board[x + 1][y - 1].ID.Contains(enemy + "Pawn")) return false;
                    }
                    else
                    {
                        if (x - 1 > -1 && y + 1 < 8 && Dot.Board[x - 1][y + 1].ID.Contains(enemy + "Pawn")) return false;
                        else if (x + 1 < 8 && y + 1 < 8 && Dot.Board[x + 1][y + 1].ID.Contains(enemy + "Pawn")) return false;
                    }
                    return true;
                case 5:
                    //Checking Knight Threating
                    if (x + 2 < 8 && y + 1 < 8 && Dot.Board[x + 2][y + 1].ID.Contains(enemy + "Knight")) return false;
                    else if (x - 2 > -1 && y - 1 > -1 && Dot.Board[x - 2][y - 1].ID.Contains(enemy + "Knight")) return false;
                    else if (x + 2 < 8 && y - 1 > -1 && Dot.Board[x + 2][y - 1].ID.Contains(enemy + "Knight")) return false;
                    else if (x - 2 > -1 && y + 1 < 8 && Dot.Board[x - 2][y + 1].ID.Contains(enemy + "Knight")) return false;
                    else if (y + 2 < 8 && x + 1 < 8 && Dot.Board[x + 1][y + 2].ID.Contains(enemy + "Knight")) return false;
                    else if (y - 2 > -1 && x - 1 > -1 && Dot.Board[x - 1][y - 2].ID.Contains(enemy + "Knight")) return false;
                    else if (y + 2 < 8 && x - 1 > -1 && Dot.Board[x - 1][y + 2].ID.Contains(enemy + "Knight")) return false;
                    else if (y - 2 > -1 && x + 1 < 8 && Dot.Board[x + 1][y - 2].ID.Contains(enemy + "Knight")) return false;
                    else return true;
                case 6:
                    //Checking King Threating
                    if (x + 1 < 8 && Dot.Board[x + 1][y].ID.Contains(enemy + "King")) return false;
                    else if (x - 1 > -1 && Dot.Board[x - 1][y].ID.Contains(enemy + "King")) return false;
                    else if (y + 1 < 8 && Dot.Board[x][y + 1].ID.Contains(enemy + "King")) return false;
                    else if (y - 1 > -1 && Dot.Board[x][y - 1].ID.Contains(enemy + "King")) return false;
                    else if (x - 1 > -1 && y - 1 > -1 && Dot.Board[x - 1][y - 1].ID.Contains(enemy + "King")) return false;
                    else if (x + 1 < 8 && y + 1 < 8 && Dot.Board[x + 1][y + 1].ID.Contains(enemy + "King")) return false;
                    else if (x + 1 < 8 && y - 1 > -1 && Dot.Board[x + 1][y - 1].ID.Contains(enemy + "King")) return false;
                    else if (x - 1 > -1 && y + 1 < 8 && Dot.Board[x - 1][y + 1].ID.Contains(enemy + "King")) return false;
                    else return true;
                case 7:
                    //Asking for all 
                    for (int i = 0; i < 7; i++)
                    {
                        if (!checKing(x, y, i)) return false;
                        else if (i == 6) return true;
                    }
                    break;
            }
            return false;
        }
        private static bool nailed(int x, int y)
        {

            return true;
        }

        private static void Xrays(int x, int y)
        {
            char pl = enemy == '2' ? '1' : '2';

            for (int i = x + 1; i < 7; i++)
            {
                if (Dot.Board[i][y].ID.Contains(pl))
                {

                }
            }

            for (int i = x - 1; i > -1; i--)
            {
                if (Dot.Board[i][y].ID.Contains(pl))
                {
                }
            }

            for (int j = y + 1; j < 7; j++)
            {
                if (Dot.Board[x][j].ID.Contains(pl))
                {
                }
            }

            for (int j = y - 1; j > -1; j--)
            {
                if (Dot.Board[x][j].ID.Contains(pl))
                {
                }
            }

            for (int i = x + 1, j = y + 1; i < 7 && j < 7; i++, j++)
            {
                if (Dot.Board[i][j].ID.Contains(pl))
                {
                }
            }

            for (int i = x - 1, j = y - 1; i > -1 && j > -1; i--, j--)
            {
                if (Dot.Board[i][j].ID.Contains(pl))
                {
                }
            }

            for (int i = x + 1, j = y - 1; i < 7 && j > -1; i++, j--)
            {
                if (Dot.Board[i][j].ID.Contains(pl))
                {
                }
            }

            for (int i = x - 1, j = y + 1; i > -1 && j < 7; i--, j++)
            {
                if (Dot.Board[i][j].ID.Contains(pl))
                {
                }
            }
        }
        public static void Clear(Color color)
        {
            Dot.BoardSort();
            Player.PlayerColor(color);
        }
        public static void Switch()
        {
            Dot.Cleanse();
            Black.Rol = !Black.Rol;
            White.Rol = !White.Rol;
            Dot.SwitchTable(White);
            Dot.SwitchTable(Black);
            Clear(Color);
        }
    }
}