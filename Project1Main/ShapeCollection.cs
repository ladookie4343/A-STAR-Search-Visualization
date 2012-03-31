using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Project1Main
{
    // This code is modified From the book "Windows Forms and Custom Controls" by Matthew Macdonald.
    // It handles hit testing of shapes drawn on screen.
    public class ShapeCollection : CollectionBase
    {
        public void Remove(Shape shapeToRemove)
        {
            List.Remove(shapeToRemove);
        }

        public Shape this[int index]
        {
            get { return (Shape)this.List[index]; }
        }

        public Shape Find(string name)
        {
            foreach (Shape shape in List)
            {
                City city = shape as City;
                if (city != null)
                {
                    if (city.Name == name)
                    {
                        return city;
                    }
                }
            }
            return null;
        }

        public void Add(Shape shapeToAdd)
        {
            // Reorder the shapes so the new shape is on top.
            foreach (Shape shape in List)
            {
                shape.ZOrder++;
            }
            shapeToAdd.ZOrder = 0;
            List.Add(shapeToAdd);
        }

        public Shape[] ArrowsBetweenCities(string firstCity, string secondCity)
        {
            Shape[] arrows = new Shape[2];
            int arrowsIndex = 0;
            foreach (Shape shape in List)
            {
                Arrow arrow = shape as Arrow;
                if (arrow != null)
                {
                    if (((arrow.Tail as City).Name == firstCity || (arrow.Head as City).Name == firstCity) &&
                        ((arrow.Tail as City).Name == secondCity || (arrow.Head as City).Name == secondCity))
                    {
                        arrows[arrowsIndex++] = arrow;
                    }
                }
            }
            return arrows;
        }

        public void BringShapeToFront(Shape frontShape)
        {
            foreach (Shape shape in List)
            {
                shape.ZOrder++;
            }
            frontShape.ZOrder = 0;
        }

        public int CityCount()
        {
            int cityCount = 0;
            foreach (Shape shape in List)
            {
                City city = shape as City;
                if (city != null)
                {
                    cityCount++;
                }
            }
            return cityCount;
        }

        public void SendShapeToBack(Shape backShape)
        {
            int maxZOrder = 0;
            foreach (Shape shape in List)
            {
                if (shape.ZOrder > maxZOrder) maxZOrder = shape.ZOrder;
            }
            maxZOrder++;
            backShape.ZOrder = maxZOrder;
        }

        public Shape HitTest(Point point)
        {
            Sort();
            foreach (Shape shape in List)
            {
                if (shape.HitTest(point) || shape.HitTestBorder(point))
                {
                    return shape;
                }
            }
            return null;
        }

        public void Sort()
        {
            InnerList.Sort();
        }
    }
}
