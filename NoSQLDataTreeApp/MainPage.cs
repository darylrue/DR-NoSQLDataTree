using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoSQLDataTreeLib;

namespace NoSQLDataTreeApp
{
    public partial class MainPage : Form
    {
        //IVARS
        private Item _root;
        private int _itemCount = 0;
        private int _tabIndex = 1;
        private List<Item> _laidOutList = new List<Item>(); //to track which Items have been removed for re-layout
        private List<Item> _noLayoutList = new List<Item>(); //to track Items that don't need layout

        //CONSTRUCTOR
        public MainPage()
        {
            InitializeComponent();
            CreateRootItem();
        }

        //METHODS
        private void CreateRootItem()
        {
            //Root column is half the width of the other columns, so it's x-position will be negative
            NoSQLDataTreeLib.Point pos = new NoSQLDataTreeLib.Point(Item.ROOT_COL_WIDTH - Item.COL_WIDTH + 3, 3);
            this._root = new Item(null, null, pos, 0, 0);
        }

        public void AddChildToRoot_Click(object sender, EventArgs e)
        {
            AddChild_Click(sender, e, _root);
        }

        public void AddChild_Click(object sender, EventArgs e, Item parent)
        {
            //Instantiate the child Item
            Item me = new Item(parent, parent.LastChild(), parent.NextChildPos(), 
                parent.NextChildColumn(), parent.NextChildRow());
            parent.Children.Add(me);

            //Determine whether a new column needs to be added
            if(NeedNewColumnFor(me))
            {
                AddColumn();
            }

            _itemCount++;  //total number of items (not including root)

            //Instantiate child Item controls
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip(this.components);
            ToolStripMenuItem addChildToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem();
            TextBox textBoxName = new TextBox()
            {
                BackColor = SystemColors.ActiveCaption,
                ContextMenuStrip = contextMenuStrip
            }; 
            TextBox textBoxType = new TextBox()
            {
                BackColor = SystemColors.ActiveCaption,
                ContextMenuStrip = contextMenuStrip
            };
            Label labelType = new Label();
            Label labelName = new Label();

            // 
            // panel
            // 
            Panel panel = new Panel
            {
                BackColor = SystemColors.ActiveCaption,
                ContextMenuStrip = contextMenuStrip
            };
            panel.Controls.Add(labelName);
            panel.Controls.Add(textBoxName);
            panel.Controls.Add(labelType);
            panel.Controls.Add(textBoxType);
            panel.Location = new System.Drawing.Point(me.Position.X, me.Position.Y);
            panel.Name = "panel" + _itemCount;
            this.tableLayoutPanel1.SetRowSpan(panel, 2);
            panel.Size = new Size(144, 54);
            me.Pnl = panel;  //add Panel to this child Item so it can be used for re-layout later

            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] {
            addChildToolStripMenuItem, deleteMenuItem});
            contextMenuStrip.Name = "contextMenuStrip" + _itemCount;
            contextMenuStrip.Size = new Size(128, 26);

            // 
            // addChildToolStripMenuItem
            // 
            addChildToolStripMenuItem.Name = "addChildToolStripMenuItem" + _itemCount;
            addChildToolStripMenuItem.Size = new Size(127, 22);
            addChildToolStripMenuItem.Text = "Add Child";
            addChildToolStripMenuItem.Click += (objSender, evt) => AddChild_Click(objSender, evt, me);

            //
            // deleteMenuItem
            //
            deleteMenuItem.Name = "deleteMenuItem" + _itemCount;
            deleteMenuItem.Size = new Size(127, 22);
            deleteMenuItem.Text = "Delete";
            deleteMenuItem.Click += (objSender, evt) => Delete_Click(objSender, evt, me);

            // 
            // textBoxName
            // 
            textBoxName.BorderStyle = BorderStyle.None;
            textBoxName.Font = new Font("Dubai", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            textBoxName.Location = new System.Drawing.Point(47, 5); 
            textBoxName.MinimumSize = new Size(0, 20);
            textBoxName.Name = "textBoxName" + _itemCount;
            textBoxName.Size = new Size(91, 22);
            textBoxName.TabIndex = _tabIndex++;

            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.ForeColor = SystemColors.ControlDarkDark;
            labelName.Location = new System.Drawing.Point(3, 9); 
            labelName.Name = "labelName" + _itemCount;
            labelName.Size = new Size(38, 13);
            labelName.Text = "Name:";

            // 
            // textBoxType
            // 
            textBoxType.BorderStyle = BorderStyle.None;
            textBoxType.Font = new Font("Dubai", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            textBoxType.Location = new System.Drawing.Point(47, 29);
            textBoxType.Margin = new Padding(0);
            textBoxType.MinimumSize = new Size(0, 20);
            textBoxType.Name = "textBoxType" + _itemCount;
            textBoxType.Size = new Size(91, 22);
            textBoxType.TabIndex = _tabIndex++;

            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.ForeColor = SystemColors.ControlDarkDark;
            labelType.ImageAlign = ContentAlignment.BottomLeft;
            labelType.Location = new System.Drawing.Point(3, 33);
            labelType.Name = "labelType" + _itemCount;
            labelType.Size = new Size(34, 13);
            labelType.Text = "Type:";

            //Younger siblings of parents and their children need re-layout (move down)
            RelayoutBasedOn(me);

            //Add this new child to the table layout panel
            this.tableLayoutPanel1.Controls.Add(me.Pnl, me.Column, me.Row);
        }

        private bool NeedNewColumnFor(Item item)
        {
            return this.tableLayoutPanel1.ColumnCount == item.Column;
        }

        private void AddColumn()
        {
            this.tableLayoutPanel1.ColumnCount++;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Item.COL_WIDTH));
        }

        private void RelayoutBasedOn(Item item)
        {
            //Determine which Items don't need layout
            _noLayoutList.Clear();
            PopulateNoLayoutList(item);

            //Starting from root, relayout all Items except those on the noLayoutList
            _laidOutList.Clear();
            RelayoutAllChildren(_root);

            //Add all removed Panels back to the table
            AddAllPanels(_laidOutList);
        }

        private void PopulateNoLayoutList(Item item)
        {
            //Items in this branch and any older siblings don't need relayout
            Item currentItem = item;
            while(currentItem != null)
            {
                _noLayoutList.Add(currentItem);
                if(currentItem.OlderSibling != null)
                {
                    PopulateNoLayoutList(currentItem.OlderSibling);
                    currentItem = null; //let the older sibling continue up the branch
                }
                else
                {
                    currentItem = currentItem.Parent;
                }
            }
        }

        private void RelayoutAllChildren(Item item)
        {
            if(item.Children != null && item.Children.Count > 0)
            {
                foreach(Item child in item.Children)
                {
                    if(!_noLayoutList.Contains(child))
                    {
                        Relayout(child);
                    }
                    RelayoutAllChildren(child);
                }
            }
        }

        private void Relayout(Item item)
        {
            _laidOutList.Add(item);
            RemovePanel(item);
            
            //Set the row and vertical position to below the lowest child of oldest sibling
            if(item.OlderSibling != null)
            {
                item.Position = new NoSQLDataTreeLib.Point(item.Position.X, 
                    item.OlderSibling.LowestChildY() + Item.ROW_HEIGHT);
                item.Row = item.OlderSibling.LowestChildRow() + 2;
            }
            else
            {
                //set row and vert. position based on parent
                item.Position = new NoSQLDataTreeLib.Point(item.Parent.Position.X + Item.COL_WIDTH, 
                    item.Parent.Position.Y + Item.ROW_HEIGHT);
                item.Row = item.Parent.Row + 1;
            }
        }

        private void RemovePanel(Item item)
        {
            this.tableLayoutPanel1.Controls.Remove(item.Pnl);  //remove the Panel from table
        }

        private void AddAllPanels(List<Item> itemList)
        {
            foreach(Item item in itemList)
            {
                this.tableLayoutPanel1.Controls.Add(item.Pnl, item.Column, item.Row);
            }
        }

        private void Delete_Click(object sender, EventArgs e, Item item)
        {
            //remove item from younger sibling
            int itemIndex = item.Parent.Children.IndexOf(item);
            if(item.Parent.Children.Count > itemIndex + 1) //younger sibling exists
            {
                if(item.OlderSibling == null)
                {
                    item.Parent.Children[itemIndex + 1].OlderSibling = null;
                }
                else
                {
                    item.Parent.Children[itemIndex + 1].OlderSibling = item.OlderSibling;
                }
            }

            //remove item from parent
            item.Parent.Children.Remove(item);
            DeleteItemAndChildren(item);

            //re-layout all elements below this item
            RelayoutBasedOn(item.Parent);
        }

        private void DeleteItemAndChildren(Item item)
        {
            if(item.Children != null && item.Children.Count > 0)
            {
                foreach(Item child in item.Children)
                {
                    DeleteItemAndChildren(child);
                }
            }
            RemovePanel(item);
        }

    } //END OF CLASS
} //END OF NAMESPACE

//TODO Save to file
