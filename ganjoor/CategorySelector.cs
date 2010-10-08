﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class CategorySelector : Form
    {
        public CategorySelector()
        {
            InitializeComponent();
            FillTree();
        }

        private void FillTree()
        {
            DbBrowser db = new DbBrowser();
            treeCats.Nodes.Clear();
            foreach (GanjoorPoet poet in db.Poets)
            {
                TreeNode newPoet = treeCats.Nodes.Add(poet._Name);
                newPoet.Tag = poet._CatID;
                AddSubCats(db, newPoet, poet._CatID);
            }
        }

        private void AddSubCats(DbBrowser db, TreeNode Node, int CatID)
        {
            foreach (GanjoorCat Cat in db.GetSubCategories(CatID))
            {
                TreeNode catNode = Node.Nodes.Add(Cat._Text);
                catNode.Tag = Cat._ID;
                AddSubCats(db, catNode, Cat._ID);
            }
        }

        private bool SelectCat(int CatID)
        {
            foreach (TreeNode Node in treeCats.Nodes)
                if (SelectCat(CatID, Node))
                    return true;
            return false;
        }
        private bool SelectCat(int CatID, TreeNode Node)
        {
            if ((int)Node.Tag == CatID)
            {
                treeCats.SelectedNode = Node;
                return true;
            }
            foreach (TreeNode ChildNode in Node.Nodes)
                if (SelectCat(CatID, ChildNode))
                    return true;
            return false;
        }

        public int SelectedCatID
        {
            get
            {
                if (treeCats.SelectedNode != null)
                    return (int)treeCats.SelectedNode.Tag;
                return 0;
            }
            set
            {
                if (treeCats.Nodes.Count == 0)
                    FillTree();
                SelectCat(value);
            }
        }

    }
}