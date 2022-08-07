using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ganjoor
{
    partial class WSInstallItems
    {
        private void InitializeComponent()
        {
            this.lblDesc = new Label();
            this.grdList = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = SystemColors.Window;
            this.lblDesc.BorderStyle = BorderStyle.FixedSingle;
            this.lblDesc.Dock = DockStyle.Top;
            this.lblDesc.Location = new Point(0, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new Size(585, 38);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "فهرست زیر وضعیت نصب مجموعه‌های دریافتی را نشان می‌دهد.";
            this.lblDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] {
            this.Column1,
            this.Column4});
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.Location = new Point(0, 38);
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new Size(585, 327);
            this.grdList.TabIndex = 6;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "نام مجموعه";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 50F;
            this.Column4.HeaderText = "وضعیت";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = DataGridViewTriState.True;
            this.Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // WSInstallItems
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.lblDesc);
            this.Name = "WSInstallItems";
            ((ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        private Label lblDesc;
        private DataGridView grdList;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;

    }
}
