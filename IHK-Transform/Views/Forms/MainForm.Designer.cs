namespace IHK_Transform.Views.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvAzubi = new System.Windows.Forms.DataGridView();
            this.btnLoadSQL = new System.Windows.Forms.Button();
            this.btnLoadCSV = new System.Windows.Forms.Button();
            this.btnLoadXML = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAzubi)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAzubi
            // 
            this.dgvAzubi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAzubi.Location = new System.Drawing.Point(12, 12);
            this.dgvAzubi.Name = "dgvAzubi";
            this.dgvAzubi.Size = new System.Drawing.Size(695, 426);
            this.dgvAzubi.TabIndex = 0;
            // 
            // btnLoadSQL
            // 
            this.btnLoadSQL.Location = new System.Drawing.Point(713, 12);
            this.btnLoadSQL.Name = "btnLoadSQL";
            this.btnLoadSQL.Size = new System.Drawing.Size(75, 23);
            this.btnLoadSQL.TabIndex = 1;
            this.btnLoadSQL.Text = "SQL";
            this.btnLoadSQL.UseVisualStyleBackColor = true;
            this.btnLoadSQL.Click += new System.EventHandler(this.btnLoadSQL_Click);
            // 
            // btnLoadCSV
            // 
            this.btnLoadCSV.Location = new System.Drawing.Point(713, 42);
            this.btnLoadCSV.Name = "btnLoadCSV";
            this.btnLoadCSV.Size = new System.Drawing.Size(75, 23);
            this.btnLoadCSV.TabIndex = 2;
            this.btnLoadCSV.Text = "CSV";
            this.btnLoadCSV.UseVisualStyleBackColor = true;
            this.btnLoadCSV.Click += new System.EventHandler(this.btnLoadCSV_Click);
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.Location = new System.Drawing.Point(713, 72);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(75, 23);
            this.btnLoadXML.TabIndex = 3;
            this.btnLoadXML.Text = "XML";
            this.btnLoadXML.UseVisualStyleBackColor = true;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoadXML);
            this.Controls.Add(this.btnLoadCSV);
            this.Controls.Add(this.btnLoadSQL);
            this.Controls.Add(this.dgvAzubi);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAzubi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAzubi;
        private System.Windows.Forms.Button btnLoadSQL;
        private System.Windows.Forms.Button btnLoadCSV;
        private System.Windows.Forms.Button btnLoadXML;
    }
}

