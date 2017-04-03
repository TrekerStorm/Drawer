using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Chain_of_Responsibilities
{
    abstract class Creator
    {
        public Command commandToExecute;
        public abstract void Draw();
    }

    class RectangleCreator : Creator
    {
        public RectangleCreator(Command com)
        {
            commandToExecute = com;
        }
        public override void Draw()
        {
            commandToExecute.Execute();
        }
    }

    class EllipseCreator : Creator
    {
        public EllipseCreator(Command com)
        {
            commandToExecute = com;
        }
        public override void Draw()
        {
            commandToExecute.Execute();
        }
    }

    class TriangleCreator : Creator
    {
        public TriangleCreator(Command com)
        {
            commandToExecute = com;
        }
        public override void Draw()
        {
            commandToExecute.Execute();
        }
    }
}
