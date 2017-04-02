using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chain_of_Responsibilities
{
    abstract class Command
    {
        protected Shape fig;
        protected Point point;
        public static Canvas canvas;

        public abstract void Execute();
        public abstract void UnExecute();
    }

    class EllipseCommand : Command
    {
        Shape ellipse;

        public EllipseCommand(Shape s, Point p)
        {
            ellipse = new Ellipse
            {
                Width = s.Width,
                Height = s.Height,
                StrokeThickness = s.StrokeThickness,
                Stroke = s.Stroke
            };
            Canvas.SetLeft(ellipse, p.X - s.Width / 2);
            Canvas.SetTop(ellipse, p.Y - s.Height / 2);
        }

        public override void Execute()
        {         
            canvas.Children.Add(ellipse);
        }

        public override void UnExecute()
        {
            canvas.Children.Remove(ellipse);
        }
    }

    class TriangleCommand : Command
    {
        Polygon poly;

        public TriangleCommand(Shape s, Point p)
        {
            Point p1 = new Point(p.X, p.Y - s.Height / 2);
            Point p2 = new Point(p.X - s.Width / 2, p.Y + s.Height / 2);
            Point p3 = new Point(p.X + s.Width / 2, p.Y + s.Height / 2);

            PointCollection pc = new PointCollection(3);
            pc.Add(p1);
            pc.Add(p2);
            pc.Add(p3);

            poly = new Polygon();
            poly.Stroke = Brushes.Black;
            poly.StrokeThickness = 1;
            poly.Points = pc;
        }

        public override void Execute()
        {
            canvas.Children.Add(poly);
        }

        public override void UnExecute()
        {
            canvas.Children.Remove(poly);
        }
    }

    class RectangleCommand : Command
    {
        Shape rect;

        public RectangleCommand(Shape s, Point p)
        {
            rect = new Rectangle
            {
                Width = s.Width,
                Height = s.Height,
                StrokeThickness = s.StrokeThickness,
                Stroke = s.Stroke
            };
            Canvas.SetLeft(rect, p.X - s.Width / 2);
            Canvas.SetTop(rect, p.Y - s.Height / 2);
        }

        public override void Execute()
        {
            canvas.Children.Add(rect);
        }

        public override void UnExecute()
        {
            canvas.Children.Remove(rect);
        }
    }
}
