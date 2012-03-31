using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Project1Main
{
    /// <summary>
    /// This class is responsible for drawing the circles that represent cities
    /// on the screen.
    /// </summary>
    class City : Shape
    {

        #region Constructors

        public City(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Label nameLabel;
        public Label NameLabel
        {
            get { return nameLabel; }
            set { nameLabel = value; }
        }

        private ShapeCollection connectedLines = new ShapeCollection();
        public ShapeCollection ConntectedLines
        {
            get { return connectedLines; }
            set { connectedLines = value; }
        }

        #endregion

        #region Methods

        private const int fontSize = 20;
        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(Location.X, Location.Y, Size.Width, Size.Height);
            // path.AddString(Name, FontFamily.GenericSerif, (int)FontStyle.Regular, 14, new Point(Location.X - Name.Length * 4, Location.Y - 24), StringFormat.GenericDefault);
            return path;
        }

        protected override void FillPath(Graphics g, GraphicsPath gP)
        {
            Brush surfaceBrush = new SolidBrush(Color);
            g.FillPath(surfaceBrush, gP);
            surfaceBrush.Dispose();
        }

        protected override void drawString(Graphics g)
        {
            Font f = new Font(FontFamily.GenericSerif, 14.0f);
            g.DrawString(Name, f, Brushes.Black, new PointF(Location.X - Name.Length * 4, Location.Y - 24));
        }

        public override Rectangle GetLargesetPossibleRegion()
        {
            path = null;
            Rectangle rect = Rectangle.Round(Path.GetBounds());
            rect.Inflate(new Size(focusBorderSpace + Name.Length * 4, focusBorderSpace + 10 + Name.Length * 4));
            return rect;
        }

        #endregion
    }
}
