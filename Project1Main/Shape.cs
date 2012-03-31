using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project1Main
{
    /// <summary>
    /// This code is modified From the book "Windows Forms and Custom Controls" by Matthew Macdonald.
    /// It handles the drawing of shapes
    /// </summary>
    public abstract class Shape : IComparable
    {

        #region Properties and Instance Variables

        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private Size size;
        public Size Size
        {
            get { return size; }
            set
            {
                size = value;
                path = null;
            }
        }

        private Point center;
        public Point Center
        {
            get { return center; }
            set { center = value; }
        }

        private int zOrder;
        public int ZOrder
        {
            get { return zOrder; }
            set { zOrder = value; }
        }

        private void updateCenter()
        {
            center = new Point(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
        }

        private Point location;
        public Point Location
        {
            get { return location; }
            set
            {
                location = value;
                updateCenter();
                path = null;
            }
        }

        protected GraphicsPath path = null;

        public GraphicsPath Path
        {
            get
            {
                if (path == null)
                {
                    RefreshPath();
                }
                return path;
            }
        }

        private void RefreshPath()
        {
            path = GeneratePath();
        }

        protected abstract GraphicsPath GeneratePath();

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private int penThickness;
        public int PenThickness
        {
            get { return penThickness; }
            set { penThickness = value; }
        }

        protected int focusBorderSpace = 5;
        protected Pen outlinePen;

        #endregion

        #region Methods

        protected virtual void FillPath(Graphics g, GraphicsPath gP)
        {
            Brush surfaceBrush = new SolidBrush(Color);
            g.FillPath(surfaceBrush, gP);
            surfaceBrush.Dispose();
        }

        protected virtual void drawString(Graphics g) { }

        public virtual void Render(Graphics g)
        {
            if (outlinePen != null) outlinePen.Dispose();
            outlinePen = new Pen(color, penThickness);

            FillPath(g, Path);
            g.DrawPath(outlinePen, Path);

            drawString(g);

            if (Selected)
            {
                Rectangle rect = Rectangle.Round(Path.GetBounds());
                rect.Inflate(new Size(focusBorderSpace, focusBorderSpace));
                ControlPaint.DrawFocusRectangle(g, rect);
            }
        }

        // Check if the point is in the shape.
        public virtual bool HitTest(Point point)
        {
            return Path.IsVisible(point);
        }

        // Check if the point is in the outline of the shape.
        public virtual bool HitTestBorder(Point point)
        {
            return Path.IsOutlineVisible(point, outlinePen);
        }

        public virtual Rectangle GetLargesetPossibleRegion()
        {
            path = null;
            Rectangle rect = Rectangle.Round(Path.GetBounds());
            rect.Inflate(new Size(focusBorderSpace, focusBorderSpace));
            return rect;
        }

        public int CompareTo(object shape)
        {
            return ZOrder.CompareTo(((Shape)shape).ZOrder);
        }

        #endregion
    }
}
