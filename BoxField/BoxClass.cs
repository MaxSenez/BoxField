using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class BoxClass
    {
        //TODO add colour
        public int x, y, size;
        

        public BoxClass (int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public bool Collison(BoxClass b)
        {
            Rectangle rec1 = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle rec2 = new Rectangle(x, y, size, size);

            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        

        //TODO add move method
        public void Move(int speed)
        {
            y += speed;
        }
        public void Move(int speed, string direction)
        {
            y += speed;
            if (direction == "right")
            {
                x += speed;
            }
            else if (direction == "left")
            {
                x -= speed;
            }
        }
    }
}
