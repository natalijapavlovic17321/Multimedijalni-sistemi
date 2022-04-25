
namespace _17321_Blok1
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.embossLaplacianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gammaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.win32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konvoluToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoRedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memorijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectDifferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomJitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stuckiDitheringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeVisualsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderedDitheringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleColorizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossdomainColorizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleColorizeWithPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorUniformityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kuwaharaFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.undoRedoToolStripMenuItem,
            this.moreFiltersToolStripMenuItem,
            this.histogramToolStripMenuItem,
            this.colorizeToolStripMenuItem,
            this.dToolStripMenuItem,
            this.moreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(879, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMYToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.embossLaplacianToolStripMenuItem,
            this.gammaToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // cMYToolStripMenuItem
            // 
            this.cMYToolStripMenuItem.Name = "cMYToolStripMenuItem";
            this.cMYToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.cMYToolStripMenuItem.Text = "CMY";
            this.cMYToolStripMenuItem.Click += new System.EventHandler(this.cMYToolStripMenuItem_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.invertToolStripMenuItem.Text = "Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // embossLaplacianToolStripMenuItem
            // 
            this.embossLaplacianToolStripMenuItem.Name = "embossLaplacianToolStripMenuItem";
            this.embossLaplacianToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.embossLaplacianToolStripMenuItem.Text = "Emboss Laplacian";
            this.embossLaplacianToolStripMenuItem.Click += new System.EventHandler(this.embossLaplacianToolStripMenuItem_Click);
            // 
            // gammaToolStripMenuItem
            // 
            this.gammaToolStripMenuItem.Name = "gammaToolStripMenuItem";
            this.gammaToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.gammaToolStripMenuItem.Text = "Gamma";
            this.gammaToolStripMenuItem.Click += new System.EventHandler(this.gammaToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.win32ToolStripMenuItem,
            this.konvoluToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(77, 23);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // win32ToolStripMenuItem
            // 
            this.win32ToolStripMenuItem.Name = "win32ToolStripMenuItem";
            this.win32ToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.win32ToolStripMenuItem.Text = "Win32";
            this.win32ToolStripMenuItem.Click += new System.EventHandler(this.win32ToolStripMenuItem_Click);
            // 
            // konvoluToolStripMenuItem
            // 
            this.konvoluToolStripMenuItem.Name = "konvoluToolStripMenuItem";
            this.konvoluToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.konvoluToolStripMenuItem.Text = "Convolution";
            this.konvoluToolStripMenuItem.Click += new System.EventHandler(this.konvoluToolStripMenuItem_Click);
            // 
            // undoRedoToolStripMenuItem
            // 
            this.undoRedoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.memorijaToolStripMenuItem});
            this.undoRedoToolStripMenuItem.Name = "undoRedoToolStripMenuItem";
            this.undoRedoToolStripMenuItem.Size = new System.Drawing.Size(101, 23);
            this.undoRedoToolStripMenuItem.Text = "Undo/Redo";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // memorijaToolStripMenuItem
            // 
            this.memorijaToolStripMenuItem.Name = "memorijaToolStripMenuItem";
            this.memorijaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.memorijaToolStripMenuItem.Text = "Memory";
            this.memorijaToolStripMenuItem.Click += new System.EventHandler(this.memorijaToolStripMenuItem_Click);
            // 
            // moreFiltersToolStripMenuItem
            // 
            this.moreFiltersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeMatrixToolStripMenuItem,
            this.edgeDetectDifferenceToolStripMenuItem,
            this.randomJitterToolStripMenuItem});
            this.moreFiltersToolStripMenuItem.Name = "moreFiltersToolStripMenuItem";
            this.moreFiltersToolStripMenuItem.Size = new System.Drawing.Size(110, 23);
            this.moreFiltersToolStripMenuItem.Text = "More Filters";
            // 
            // changeMatrixToolStripMenuItem
            // 
            this.changeMatrixToolStripMenuItem.Name = "changeMatrixToolStripMenuItem";
            this.changeMatrixToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
            this.changeMatrixToolStripMenuItem.Text = "Change Matrix";
            this.changeMatrixToolStripMenuItem.Click += new System.EventHandler(this.changeMatrixToolStripMenuItem_Click_1);
            // 
            // edgeDetectDifferenceToolStripMenuItem
            // 
            this.edgeDetectDifferenceToolStripMenuItem.Name = "edgeDetectDifferenceToolStripMenuItem";
            this.edgeDetectDifferenceToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
            this.edgeDetectDifferenceToolStripMenuItem.Text = "EdgeDetectDifference";
            this.edgeDetectDifferenceToolStripMenuItem.Click += new System.EventHandler(this.edgeDetectDifferenceToolStripMenuItem_Click);
            // 
            // randomJitterToolStripMenuItem
            // 
            this.randomJitterToolStripMenuItem.Name = "randomJitterToolStripMenuItem";
            this.randomJitterToolStripMenuItem.Size = new System.Drawing.Size(247, 26);
            this.randomJitterToolStripMenuItem.Text = "Random Jitter";
            this.randomJitterToolStripMenuItem.Click += new System.EventHandler(this.randomJitterToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stuckiDitheringToolStripMenuItem,
            this.changeVisualsToolStripMenuItem,
            this.orderedDitheringToolStripMenuItem,
            this.grayScaleToolStripMenuItem});
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(187, 23);
            this.histogramToolStripMenuItem.Text = "Histogram and dithering";
            // 
            // stuckiDitheringToolStripMenuItem
            // 
            this.stuckiDitheringToolStripMenuItem.Name = "stuckiDitheringToolStripMenuItem";
            this.stuckiDitheringToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.stuckiDitheringToolStripMenuItem.Text = "Stucki dithering";
            this.stuckiDitheringToolStripMenuItem.Click += new System.EventHandler(this.stuckiDitheringToolStripMenuItem_Click);
            // 
            // changeVisualsToolStripMenuItem
            // 
            this.changeVisualsToolStripMenuItem.Name = "changeVisualsToolStripMenuItem";
            this.changeVisualsToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.changeVisualsToolStripMenuItem.Text = "Change Visuals";
            this.changeVisualsToolStripMenuItem.Click += new System.EventHandler(this.changeVisualsToolStripMenuItem_Click_1);
            // 
            // orderedDitheringToolStripMenuItem
            // 
            this.orderedDitheringToolStripMenuItem.Name = "orderedDitheringToolStripMenuItem";
            this.orderedDitheringToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.orderedDitheringToolStripMenuItem.Text = "Ordered dithering";
            this.orderedDitheringToolStripMenuItem.Click += new System.EventHandler(this.orderedDitheringToolStripMenuItem_Click);
            // 
            // grayScaleToolStripMenuItem
            // 
            this.grayScaleToolStripMenuItem.Name = "grayScaleToolStripMenuItem";
            this.grayScaleToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.grayScaleToolStripMenuItem.Text = "GrayScale";
            this.grayScaleToolStripMenuItem.Click += new System.EventHandler(this.grayScaleToolStripMenuItem_Click);
            // 
            // colorizeToolStripMenuItem
            // 
            this.colorizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleColorizeToolStripMenuItem,
            this.crossdomainColorizeToolStripMenuItem,
            this.simpleColorizeWithPictureToolStripMenuItem});
            this.colorizeToolStripMenuItem.Name = "colorizeToolStripMenuItem";
            this.colorizeToolStripMenuItem.Size = new System.Drawing.Size(83, 23);
            this.colorizeToolStripMenuItem.Text = "Colorize";
            // 
            // simpleColorizeToolStripMenuItem
            // 
            this.simpleColorizeToolStripMenuItem.Name = "simpleColorizeToolStripMenuItem";
            this.simpleColorizeToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.simpleColorizeToolStripMenuItem.Text = "Simple colorize";
            this.simpleColorizeToolStripMenuItem.Click += new System.EventHandler(this.simpleColorizeToolStripMenuItem_Click);
            // 
            // crossdomainColorizeToolStripMenuItem
            // 
            this.crossdomainColorizeToolStripMenuItem.Name = "crossdomainColorizeToolStripMenuItem";
            this.crossdomainColorizeToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.crossdomainColorizeToolStripMenuItem.Text = "Cross-domain colorize";
            this.crossdomainColorizeToolStripMenuItem.Click += new System.EventHandler(this.crossdomainColorizeToolStripMenuItem_Click);
            // 
            // simpleColorizeWithPictureToolStripMenuItem
            // 
            this.simpleColorizeWithPictureToolStripMenuItem.Name = "simpleColorizeWithPictureToolStripMenuItem";
            this.simpleColorizeWithPictureToolStripMenuItem.Size = new System.Drawing.Size(286, 26);
            this.simpleColorizeWithPictureToolStripMenuItem.Text = "Simple colorize with picture";
            this.simpleColorizeWithPictureToolStripMenuItem.Click += new System.EventHandler(this.simpleColorizeWithPictureToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.saveToolStripMenuItem1});
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(124, 23);
            this.dToolStripMenuItem.Text = "Downsampling";
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(126, 26);
            this.loadToolStripMenuItem1.Text = "Load";
            this.loadToolStripMenuItem1.Click += new System.EventHandler(this.loadToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(126, 26);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // moreToolStripMenuItem
            // 
            this.moreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorUniformityToolStripMenuItem,
            this.kuwaharaFilterToolStripMenuItem});
            this.moreToolStripMenuItem.Name = "moreToolStripMenuItem";
            this.moreToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.moreToolStripMenuItem.Text = "More";
            // 
            // colorUniformityToolStripMenuItem
            // 
            this.colorUniformityToolStripMenuItem.Name = "colorUniformityToolStripMenuItem";
            this.colorUniformityToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.colorUniformityToolStripMenuItem.Text = "Color uniformity";
            this.colorUniformityToolStripMenuItem.Click += new System.EventHandler(this.colorUniformityToolStripMenuItem_Click);
            // 
            // kuwaharaFilterToolStripMenuItem
            // 
            this.kuwaharaFilterToolStripMenuItem.Name = "kuwaharaFilterToolStripMenuItem";
            this.kuwaharaFilterToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.kuwaharaFilterToolStripMenuItem.Text = "Kuwahara filter";
            this.kuwaharaFilterToolStripMenuItem.Click += new System.EventHandler(this.kuwaharaFilterToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(823, 532);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(18, 38);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Red";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Green";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Blue";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(823, 532);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(879, 603);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Zadatak 2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoRedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem win32ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konvoluToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memorijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cMYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem embossLaplacianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gammaToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem moreFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeMatrixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectDifferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomJitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stuckiDitheringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeVisualsToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ToolStripMenuItem orderedDitheringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleColorizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossdomainColorizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleColorizeWithPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem moreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorUniformityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kuwaharaFilterToolStripMenuItem;
    }
}

