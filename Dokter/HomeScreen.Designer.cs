namespace Dokter
{
    partial class HomeScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.personDataLB = new System.Windows.Forms.ListBox();
            this.dataLB = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartData
            // 
            this.chartData.BorderlineColor = System.Drawing.Color.Gray;
            chartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.None;
            chartArea1.BackColor = System.Drawing.Color.Gray;
            chartArea1.BorderWidth = 0;
            chartArea1.CursorX.LineWidth = 0;
            chartArea1.CursorY.LineWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.chartData.ChartAreas.Add(chartArea1);
            this.chartData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            legend1.AutoFitMinFontSize = 12;
            legend1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsEquallySpacedItems = true;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 17F;
            legend1.Position.Width = 22F;
            legend1.Position.X = 78F;
            legend1.Position.Y = 3F;
            legend1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartData.Legends.Add(legend1);
            this.chartData.Location = new System.Drawing.Point(0, 0);
            this.chartData.Name = "chartData";
            this.chartData.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartData.Size = new System.Drawing.Size(1003, 681);
            this.chartData.TabIndex = 0;
            this.chartData.Text = "chart";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartData);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 681);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.personDataLB);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataLB);
            this.splitContainer2.Size = new System.Drawing.Size(257, 681);
            this.splitContainer2.SplitterDistance = 203;
            this.splitContainer2.TabIndex = 1;
            // 
            // personDataLB
            // 
            this.personDataLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personDataLB.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.personDataLB.FormattingEnabled = true;
            this.personDataLB.ItemHeight = 20;
            this.personDataLB.Location = new System.Drawing.Point(0, 0);
            this.personDataLB.Name = "personDataLB";
            this.personDataLB.Size = new System.Drawing.Size(257, 203);
            this.personDataLB.TabIndex = 0;
            // 
            // dataLB
            // 
            this.dataLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLB.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLB.FormattingEnabled = true;
            this.dataLB.ItemHeight = 20;
            this.dataLB.Location = new System.Drawing.Point(0, 0);
            this.dataLB.Name = "dataLB";
            this.dataLB.Size = new System.Drawing.Size(257, 474);
            this.dataLB.TabIndex = 0;
            this.dataLB.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "HomeScreen";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox dataLB;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox personDataLB;
    }
}