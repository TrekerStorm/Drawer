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

            poly = new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Points = pc
            };
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

    class ThicknessCommand : Command
    {
        double newThickness, oldThickness;

        public ThicknessCommand(double t1, double t2)
        {
            newThickness = t1;
            oldThickness = t2;
        }

        public override void Execute()
        {
            foreach(var item in canvas.Children.OfType<Shape>())
            {
                item.StrokeThickness = newThickness;
            }
            canvas.InvalidateVisual();
        }

        public override void UnExecute()
        {
            foreach (var item in canvas.Children.OfType<Shape>())
            {
                item.StrokeThickness = oldThickness;
            }
            canvas.InvalidateVisual();
        }
    }

    class ColorCommand : Command
    {
        Brush newBrush, oldBrush;

        public ColorCommand(Brush b1, Brush b2)
        {
            newBrush = b1;
            oldBrush = b2;
        }

        public override void Execute()
        {
            foreach (var item in canvas.Children.OfType<Shape>())
            {
                item.Stroke = newBrush;
            }
            canvas.InvalidateVisual();
        }

        public override void UnExecute()
        {
            foreach (var item in canvas.Children.OfType<Shape>())
            {
                item.Stroke = oldBrush;
            }
            canvas.InvalidateVisual();
        }
    }

    class ClearCanvasCommand : Command
    {
        List<Shape> shapesOnCanvas = new List<Shape>();

        public override void Execute()
        {
            shapesOnCanvas.AddRange(canvas.Children.OfType<Shape>());
            canvas.Children.Clear();
        }

        public override void UnExecute()
        {
            foreach(var item in shapesOnCanvas)
            {
                canvas.Children.Add(item);
            }
        }
    }

    class MacroCommand : Command
    {
        List<Command> commands;
        
        public MacroCommand(List<Command> coms)
        {
            commands = coms;
        }

        public override void Execute()
        {
            foreach(var com in commands)
            {
                com.Execute();
            }
        }

        public override void UnExecute()
        {
            foreach(var com in commands)
            {
                com.UnExecute();
            }
        }
    }
}
