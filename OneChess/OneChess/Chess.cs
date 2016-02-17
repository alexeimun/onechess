using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OneChess
{
    public class Chess
    {
        private Color color;
        private Texture2D texture;
        public Rectangle Size;
        private int x, y;
        private bool visible;
        //initials variables
        public bool initial;
        public bool castle;
        //Identidy
        private string Id;
        private Chess token;
        #region Properties
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public int X
        {  // get and set X
            get { return x; }
            set { x = value; }
        }
        public int Y
        { // get and set Y
            get { return y; }
            set { y = value; }
        }
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        public string ID
        {
            get { return Id; }
            set { Id = value; }
        }
        public Chess Token
        {
            get { return token; }
            set { token = value; }
        }
        #endregion
    }
}
