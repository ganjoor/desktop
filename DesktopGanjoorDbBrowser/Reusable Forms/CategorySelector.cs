using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class CategorySelector : Form
    {
        public CategorySelector(DbBrowser refDb = null)
            : this(0, refDb)
        {
            treeCats.CheckBoxes = true;
        }
        public CategorySelector(int PoetID, DbBrowser refDb = null)
        {
            InitializeComponent();
            _RefDb = refDb;
            FillTree(PoetID);
        }

        private DbBrowser _RefDb;

        private void FillTree(int PoetID)
        {
            DbBrowser db = _RefDb == null ? new DbBrowser() : _RefDb;
            treeCats.Nodes.Clear();
            if (PoetID == 0)
            {
                treeCats.Nodes.Add("همه").Tag = 0;
                foreach (GanjoorPoet poet in db.Poets)
                {
                    TreeNode newPoet = treeCats.Nodes.Add(poet._Name);
                    newPoet.Tag = poet._CatID;
                    AddSubCats(db, newPoet, poet._CatID);
                }
            }
            else
            {
                GanjoorPoet poet = db.GetPoet(PoetID);
                TreeNode newPoet = treeCats.Nodes.Add(poet._Name);
                newPoet.Tag = poet._CatID;
                AddSubCats(db, newPoet, poet._CatID);
            }
            if (_RefDb == null)
            {
                db.CloseDb();
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
            set => SelectCat(value);
        }

        private void CheckCatList(int[] CatList)
        {
            if (CatList.Length == 0)
                CatList = new[] { 0 };
            foreach (TreeNode Node in treeCats.Nodes)
                CheckCatList(CatList, Node);
        }
        private void CheckCatList(int[] CatList, TreeNode Node)
        {
            int NodeID = (int)Node.Tag;
            foreach (int CatID in CatList)
                if (CatID == NodeID)
                {
                    Node.Checked = true;
                    CheckUnCheckChildren(Node, true);
                    Node.ExpandAll();
                    TreeNode parent = Node.Parent;
                    while (parent != null)
                    {
                        parent.Expand();
                        parent = parent.Parent;
                    }
                    return;
                }
            foreach (TreeNode ChildNode in Node.Nodes)
                CheckCatList(CatList, ChildNode);
        }

        private void FillWithCheckedList(List<int> outputList)
        {
            foreach (TreeNode Node in treeCats.Nodes)
                FillWithCheckedList(outputList, Node);
        }
        private void FillWithCheckedList(List<int> outputList, TreeNode Node)
        {
            if (Node.Checked)
                outputList.Add((int)Node.Tag);
            else//if a node is check all its children is assumed to be checked
                foreach (TreeNode ChildNode in Node.Nodes)
                    FillWithCheckedList(outputList, ChildNode);
        }

        public int[] CheckedCats
        {
            set => CheckCatList(value);
            get
            {
                List<int> checkedCatList = new List<int>();
                FillWithCheckedList(checkedCatList);
                return checkedCatList.ToArray();
            }
        }
        /// <summary>
        /// Sample: "12;23;2;67;"
        /// </summary>
        public string CheckedCatsString
        {
            set
            {
                string[] CatStrs = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int[] Cats = new int[CatStrs.Length];
                for (int i = 0; i < CatStrs.Length; i++)
                    Cats[i] = Convert.ToInt32(CatStrs[i]);
                CheckedCats = Cats;
            }
            get
            {
                string result = "";
                foreach (int Cat in CheckedCats)
                {
                    result += Cat.ToString();
                    result += ";";
                }
                return result;
            }
        }

        private void treeCats_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                int Cat = (int)e.Node.Tag;
                if (Cat == 0)
                {
                    for (int i = 1; i < treeCats.Nodes.Count; i++)
                        CheckUnCheckChildren(treeCats.Nodes[i], false);
                }
                else
                {
                    treeCats.Nodes[0].Checked = false;
                }
            }
            CheckUnCheckChildren(e.Node, e.Node.Checked);
            if (e.Node.Checked)
                e.Node.ExpandAll();
            else
                e.Node.Collapse();
        }

        private void CheckUnCheckChildren(TreeNode Node, bool check)
        {
            foreach (TreeNode ChildNode in Node.Nodes)
            {
                ChildNode.Checked = check;
                CheckUnCheckChildren(ChildNode, check);
            }
        }

    }
}
