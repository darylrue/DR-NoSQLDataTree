using System.Collections.Generic;
using System.Windows.Forms;

namespace NoSQLDataTreeLib
{
    public class Item
    {
        //CONSTANTS
        public static readonly int ROOT_COL_WIDTH = 75;
        public static readonly int COL_WIDTH = 150;
        public static readonly int ROW_HEIGHT = 30;

        //IVARS
        public string Name { get; set; }
        public string DataType { get; set; }
        public Item Parent { get; set; }
        public Item OlderSibling { get; set; }  //the item just before this one in the same column
        public Point Position { get; set; }
        public int Column { get; }
        public int Row { get; set; }
        public Panel Pnl { get; set; }  //the Panel that this Item represents

        private List<Item> _children;
        public List<Item> Children
        {
            get
            {
                if(_children == null)  //instantiate when needed
                {
                    _children = new List<Item>();
                }
                return _children;
            }
        }

        //CONSTRUCTOR(S)
        public Item(Item parent, Item olderSibling, Point position, int column, int row)
        {
            this.Parent = parent;
            this.OlderSibling = olderSibling;
            this.Position = position;
            this.Column = column;
            this.Row = row;
        }

        //METHODS
        public Point NextChildPos()
        {
            int y;
            int lastChildY = LowestChildY();
            if(lastChildY == this.Position.Y)  //no children
            {
                y = lastChildY + ROW_HEIGHT;  //stagger from the parent
            }
            else
            {
                y = lastChildY + ROW_HEIGHT * 2;
            }
            return new Point(this.Position.X + COL_WIDTH, y);
        }

        public int NextChildRow()
        {
            if (this.Children == null || this.Children.Count == 0)
            {
                return this.Row + 1;
            }
            return LowestChildRow() + 2;
        }

        public int LowestChildRow()
        {
            if (this.Children == null || this.Children.Count == 0)
            {
                return this.Row;
            }
            return this.Children[this.Children.Count - 1].LowestChildRow();
        }

        public int NextChildColumn()
        {
            return this.Column + 1;
        }

        public Item LastChild()
        {
            if (this.Children == null || this.Children.Count == 0)
            {
                return null;
            }
            return this.Children[this.Children.Count - 1];
        }

        //returns the y-coordinate of the last child in the entire branch
        public int LowestChildY()
        {
            Item lastChild = LastChild();
            if(lastChild == null)
            {
                return this.Position.Y;
            }
            return lastChild.LowestChildY();
        }

    } //END OF CLASS

    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

} //END OF NAMESPACE
