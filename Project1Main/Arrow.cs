using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project1Main
{
    /// <summary>
    /// This class is responsible for drawing an arrow on the screen.
    /// </summary>
    public class Arrow : Shape
    {
        private Shape head;
        public Shape Head
        {
            get { return head; }
            set { head = value; }
        }

        private Shape tail;
        public Shape Tail
        {
            get { return tail; }
            set { tail = value; }
        }

        protected override GraphicsPath GeneratePath()
        {
            GraphicsPath path = new GraphicsPath();
            Point tailPoint = new Point(tail.Center.X, tail.Center.Y);
            Point headPoint = new Point(head.Center.X, head.Center.Y);
            path.AddLine(tailPoint.X, tailPoint.Y, headPoint.X, headPoint.Y);
            return path;
        }

        public override void Render(Graphics g)
        {
            if (outlinePen != null) outlinePen.Dispose();
            outlinePen = new Pen(Color, PenThickness);
            outlinePen.EndCap = LineCap.ArrowAnchor;
            outlinePen.Width = 5;

            FillPath(g, Path);
            g.DrawPath(outlinePen, Path);
        }
    }
}