// -------------------------------------------------
// C-Sharp DataViz ColorBar
//
// License:
// Copyright (c) 2008-2017 Joseph True
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// Sample Data Files:
// Auto MPG and Iris data sets originally from:
// Lichman, M. (2013). UCI Machine Learning Repository [http://archive.ics.uci.edu/ml].
// Irvine, CA: University of California, School of Information and Computer Science. 
// https://archive.ics.uci.edu/ml/datasets/auto+mpg
// https://archive.ics.uci.edu/ml/datasets/Iris
//
// -------------------------------------------------
// Author:			      Joseph True
// Original date:         Originally created as a homework project for WPI CS525D
//                        Data Visualization, Fall 2008 graduate course
//
// Programming Language:  C#
//
// Overall Design:		  Reads plain text file with single data series and
//						  maps/scales data values to a color range.
//						  Plots the result as a horizontal color bar.
//		
// Default data file:     "sampledata_car-prices.csv" data file with 50 values
// Additional Files:	  Other sample data files provided as "sampledata_..."
//
// -------------------------------------------------
//
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace WindowsApplication2
{
	// .NET genertared code for form
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmColorBar : System.Windows.Forms.Form
	{

		//---------------------------------------
		// Application variables
		//---------------------------------------
		// String data values read from file
		ArrayList m_arrayX = new ArrayList();
        //ArrayList m_arrayXsorted = new ArrayList();

		// Axis points
		static int m_offsetX = 10;
		static int m_offsetY = 10;
        static int m_BarSize = 1;         // Size of indivudal bar for a data value
		static int m_ColorBarSize = 800;    // Size of overall viz
		int m_xAxisStart =m_offsetX;
		int m_barHeight = 150;			// default val for color bar heigth

        int m_MaxRecords = 10000;    // Can only handle this many records
       //  int m_MaxChartSize = 2000;

		//imported data
		float[] m_Xdata;
        float[] m_XdataSorted;
		float m_xMin;
		float m_xMax;
		float m_xDataAxisStart;
		float m_xDataAxisEnd;

		private System.Windows.Forms.Label lblMinMax;
		private System.Windows.Forms.ComboBox cboBarHeight;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rdoGray;
		private System.Windows.Forms.RadioButton rdoColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private GroupBox groupBox2;
        private OpenFileDialog openFileDialog1;
        private Button btnOpenFile;
        private Label lblFilename;
        private GroupBox groupBox3;
        private CheckBox chkSort;
        private Panel panel1;
        private Panel panel2;
        private Label lblNumRecs;
        private Label lblTitleRecCount;
        private Label lblTitleMinMax;
        private Label lblTitleFile;
        private ComboBox cboBarSize;
        private Label lblBarSize;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmColorBar()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            // this.WindowState = FormWindowState.Maximized;

            chkSort.Checked = false;

            cboBarHeight.Text = "150";

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblMinMax = new System.Windows.Forms.Label();
            this.cboBarHeight = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoGray = new System.Windows.Forms.RadioButton();
            this.rdoColor = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBarSize = new System.Windows.Forms.Label();
            this.cboBarSize = new System.Windows.Forms.ComboBox();
            this.chkSort = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTitleRecCount = new System.Windows.Forms.Label();
            this.lblTitleMinMax = new System.Windows.Forms.Label();
            this.lblTitleFile = new System.Windows.Forms.Label();
            this.lblNumRecs = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMinMax
            // 
            this.lblMinMax.AutoSize = true;
            this.lblMinMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinMax.Location = new System.Drawing.Point(106, 49);
            this.lblMinMax.Name = "lblMinMax";
            this.lblMinMax.Size = new System.Drawing.Size(50, 13);
            this.lblMinMax.TabIndex = 4;
            this.lblMinMax.Text = "Min, Max";
            // 
            // cboBarHeight
            // 
            this.cboBarHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBarHeight.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "100",
            "150"});
            this.cboBarHeight.Location = new System.Drawing.Point(193, 31);
            this.cboBarHeight.Name = "cboBarHeight";
            this.cboBarHeight.Size = new System.Drawing.Size(80, 21);
            this.cboBarHeight.TabIndex = 5;
            this.cboBarHeight.SelectedIndexChanged += new System.EventHandler(this.cboBarHeight_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bar Height";
            // 
            // rdoGray
            // 
            this.rdoGray.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoGray.Location = new System.Drawing.Point(22, 47);
            this.rdoGray.Name = "rdoGray";
            this.rdoGray.Size = new System.Drawing.Size(80, 16);
            this.rdoGray.TabIndex = 8;
            this.rdoGray.Text = "Gray-scale";
            this.rdoGray.CheckedChanged += new System.EventHandler(this.rdoGray_CheckedChanged);
            // 
            // rdoColor
            // 
            this.rdoColor.Checked = true;
            this.rdoColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoColor.Location = new System.Drawing.Point(22, 24);
            this.rdoColor.Name = "rdoColor";
            this.rdoColor.Size = new System.Drawing.Size(73, 16);
            this.rdoColor.TabIndex = 9;
            this.rdoColor.TabStop = true;
            this.rdoColor.Text = "Color";
            this.rdoColor.CheckedChanged += new System.EventHandler(this.rdoColor_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoColor);
            this.groupBox1.Controls.Add(this.rdoGray);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(300, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 76);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color Map Style";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblBarSize);
            this.groupBox2.Controls.Add(this.cboBarSize);
            this.groupBox2.Controls.Add(this.chkSort);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.cboBarHeight);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(406, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 107);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color Bar Options";
            // 
            // lblBarSize
            // 
            this.lblBarSize.AutoSize = true;
            this.lblBarSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarSize.Location = new System.Drawing.Point(121, 70);
            this.lblBarSize.Name = "lblBarSize";
            this.lblBarSize.Size = new System.Drawing.Size(46, 13);
            this.lblBarSize.TabIndex = 13;
            this.lblBarSize.Text = "Bar Size";
            // 
            // cboBarSize
            // 
            this.cboBarSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBarSize.FormattingEnabled = true;
            this.cboBarSize.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "12",
            "20"});
            this.cboBarSize.Location = new System.Drawing.Point(194, 67);
            this.cboBarSize.Name = "cboBarSize";
            this.cboBarSize.Size = new System.Drawing.Size(79, 21);
            this.cboBarSize.TabIndex = 12;
            this.cboBarSize.SelectedIndexChanged += new System.EventHandler(this.cboBarSize_SelectedIndexChanged);
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSort.Location = new System.Drawing.Point(15, 33);
            this.chkSort.Name = "chkSort";
            this.chkSort.Size = new System.Drawing.Size(79, 17);
            this.chkSort.TabIndex = 11;
            this.chkSort.Text = "Sort values";
            this.chkSort.UseVisualStyleBackColor = true;
            this.chkSort.CheckedChanged += new System.EventHandler(this.chkSort_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.Location = new System.Drawing.Point(256, 64);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(110, 28);
            this.btnOpenFile.TabIndex = 12;
            this.btnOpenFile.Text = "Select Data File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.Location = new System.Drawing.Point(106, 24);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(49, 13);
            this.lblFilename.TabIndex = 13;
            this.lblFilename.Text = "Filename";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTitleRecCount);
            this.groupBox3.Controls.Add(this.lblTitleMinMax);
            this.groupBox3.Controls.Add(this.lblTitleFile);
            this.groupBox3.Controls.Add(this.lblNumRecs);
            this.groupBox3.Controls.Add(this.btnOpenFile);
            this.groupBox3.Controls.Add(this.lblFilename);
            this.groupBox3.Controls.Add(this.lblMinMax);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 107);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data File";
            // 
            // lblTitleRecCount
            // 
            this.lblTitleRecCount.AutoSize = true;
            this.lblTitleRecCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleRecCount.Location = new System.Drawing.Point(8, 74);
            this.lblTitleRecCount.Name = "lblTitleRecCount";
            this.lblTitleRecCount.Size = new System.Drawing.Size(91, 13);
            this.lblTitleRecCount.TabIndex = 17;
            this.lblTitleRecCount.Text = "Total Records:";
            // 
            // lblTitleMinMax
            // 
            this.lblTitleMinMax.AutoSize = true;
            this.lblTitleMinMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleMinMax.Location = new System.Drawing.Point(8, 49);
            this.lblTitleMinMax.Name = "lblTitleMinMax";
            this.lblTitleMinMax.Size = new System.Drawing.Size(66, 13);
            this.lblTitleMinMax.TabIndex = 16;
            this.lblTitleMinMax.Text = "Min - Max:";
            // 
            // lblTitleFile
            // 
            this.lblTitleFile.AutoSize = true;
            this.lblTitleFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleFile.Location = new System.Drawing.Point(8, 24);
            this.lblTitleFile.Name = "lblTitleFile";
            this.lblTitleFile.Size = new System.Drawing.Size(31, 13);
            this.lblTitleFile.TabIndex = 15;
            this.lblTitleFile.Text = "File:";
            // 
            // lblNumRecs
            // 
            this.lblNumRecs.AutoSize = true;
            this.lblNumRecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRecs.Location = new System.Drawing.Point(106, 74);
            this.lblNumRecs.Name = "lblNumRecs";
            this.lblNumRecs.Size = new System.Drawing.Size(99, 13);
            this.lblNumRecs.TabIndex = 14;
            this.lblNumRecs.Text = "Number of Records";
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 326);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(937, 293);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // frmColorBar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(984, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.MinimumSize = new System.Drawing.Size(1000, 510);
            this.Name = "frmColorBar";
            this.Text = "DataViz - Color Bar";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		// --- JTrue --------------------------------------------
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmColorBar());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            importData("sampledata_car-prices.csv");
		}

		// C#
		protected override void OnPaint(PaintEventArgs e) 
		{			
			// Redraw screen when form re-paints
			//drawCanvas();
			//drawColorBar();
		}

		// Read text file and load the data
		private void importData(string myFile)
		{
            string input = myFile;
            int index = input.LastIndexOf("\\");
            if (index > 0)
                input = input.Substring(index+1);

            lblFilename.Text = input;

            FileStream aFile = new FileStream(myFile, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(aFile);

            // Reset the data array
            if (m_arrayX.Count > 0){
                m_arrayX.Clear();
            }

            // Read lines from file
            int x = 0;
            string line;
            while ((line = sr.ReadLine()) != null )
			{
                if (x > m_MaxRecords)
                {
                    // show message and stop reading file
                    MessageBox.Show("Too many records.\nData import stopped at " + m_MaxRecords.ToString() + " records");
                    break;
                }
                m_arrayX.Add(line);
                x += 1;
			}
			sr.Close();

			// Convert imported strings to float
			m_Xdata = new float[m_arrayX.Count];
            m_XdataSorted = new float[m_arrayX.Count];
			for(int i=0;i<m_arrayX.Count;i++)
			{
				m_Xdata[i]=System.Convert.ToSingle( m_arrayX[i]);
                m_XdataSorted[i] = System.Convert.ToSingle(m_arrayX[i]);
			}

			getMinMax();

            // Set the size of a bar based on number of data values
            // Get number of data points to fit into color bar.
            int xCount = m_Xdata.Length;
            double dx = System.Convert.ToDouble(m_ColorBarSize) / System.Convert.ToDouble(xCount);

            m_BarSize = System.Convert.ToInt32(Math.Ceiling(dx));

            cboBarSize.SelectedText = m_BarSize.ToString();
		}

        // Draw white canvas area for color bar
        private void drawCanvas()
        {
            using (Graphics g = panel2.CreateGraphics())
            {
                int ht = 280;
                int wd = m_ColorBarSize + 50;

                // Draw the canvas
                Color myColor;
                myColor = Color.White;
                // g.FillRectangle(new SolidBrush(myColor), m_xAxisStart, m_offsetY+10, wd, ht);
                g.FillRectangle(new SolidBrush(myColor), 0, 0, wd, ht);

                // Add a border
                Pen blackPen = new Pen(Color.Black, 1);
                g.DrawRectangle(blackPen, 0, 0, wd, ht);
            }
        }

		// Draw the color bar from imported x data series.
		private void drawColorBar()
		{
            float[] theData;

            // Need to restructure this to allow sorting as an option
            if (chkSort.Checked == true)
            {
                Array.Sort(m_XdataSorted);
                theData = m_XdataSorted;
            }
            else
            {
                theData = m_Xdata;
            }

            if (theData == null) { return; }

			// Display number of data points
            lblNumRecs.Text = theData.Length.ToString();

            // Size the panel and the border
            m_ColorBarSize = theData.Length * m_BarSize;
            panel2.Width = m_ColorBarSize + 100;

            // Draw the actual color bar
			// Define x start location of color bar
			int cBarStartX = m_xAxisStart + 10;

			int rColor;
			int gColor;
			int bColor;

            // Draw a bar and label for each data point
			if (m_Xdata != null)
			{
				// Adjust to coordinate system
				int x=0;
				int[] rgbVal = new int[3];
				float xDataAxisRange = m_xDataAxisEnd - m_xDataAxisStart;

                for (int i = 0; i <= theData.GetUpperBound(0); i++)
				{
					// Scale x to a color.
					if (rdoGray.Checked==true)
					{
                        x = ConvertXtoGray(theData[i]); 
						rColor = x;
						gColor = x;
						bColor = x;
					}
					else
					{
                        rgbVal = ConvertXtoColor(theData[i]); 
						rColor = rgbVal[0];
						gColor = rgbVal[1];
						bColor = rgbVal[2];
					}		

					string strVal;
                    strVal = System.Convert.ToString(theData[i]);

                    int alpha = ConvertXtoGray(theData[i]);

                    using (Graphics g = panel2.CreateGraphics())
					{
                        Color myColor;
                        myColor = Color.Black;

                        // Only draw labels if we have enough room
                        if (m_BarSize >= 12)
                        {
                            float fntSizef;
                            fntSizef = 8;
                            Font fnt = new Font("Verdana", fntSizef);

                            // display vertical string label for data value
                            StringFormat drawFormat = new StringFormat();
                            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                            g.DrawString(strVal, fnt, new SolidBrush(myColor), (i * m_BarSize) + cBarStartX, m_offsetY + m_barHeight + 20, drawFormat);
                        }

						// Set color of bar using ARGB (alpha, red, green, and blue) value
						myColor = Color.FromArgb(rColor,gColor,bColor);

						// Use selected bar ht for ht of rect
						// FillRectangle(brush,x,y,w,h)
						g.FillRectangle(new SolidBrush(myColor),(i*m_BarSize)+cBarStartX,m_offsetY+10,m_BarSize,m_barHeight);
					}
				}
			}
		}

        // Get and display min, max values from loaded data
		private void getMinMax()
		{
           float xMin;
           float xMax;
			if (m_Xdata != null)
			{
				xMin = m_Xdata[0];
				for(int i=1; i<=m_Xdata.GetUpperBound(0);i++)
				{
					if (m_Xdata[i] < xMin)
					{
						xMin = m_Xdata[i];
					}
				}
				xMax = m_Xdata[0];
				for(int i=1; i<=m_Xdata.GetUpperBound(0);i++)
				{
					if (m_Xdata[i] > xMax)
					{
						xMax = m_Xdata[i];
					}
				}

                m_xMin = xMin;
                m_xMax = xMax;
                    
				// Display results
                lblMinMax.Text = m_xMin + "  to  " + m_xMax;

				// Set overall axis range of imported data based on data min max
				float xDataRange = m_xMax - m_xMin;
				m_xDataAxisStart = m_xMin - System.Convert.ToSingle(.5*m_xMin);
				m_xDataAxisEnd = m_xMax + System.Convert.ToSingle((2*m_xMin));
			}
		}

		// Get a gray-scale color/level for a given x value.
		private int ConvertXtoGray(float Xval)
		{
			int xGray;
			float xDataAxisRange = m_xDataAxisEnd - m_xDataAxisStart;

			xGray = 255 - System.Convert.ToInt16((255)*(Xval/(xDataAxisRange)));
			return xGray;
		}

		// Get an RGB color value for a given x value.
		private int[] ConvertXtoColor(float Xval)
		{
			int R,G,B;
			int[] xColor = new int[3];
			float xDataAxisRange = m_xDataAxisEnd - m_xDataAxisStart;

			// Color map ranges from light blue to dark blue
			// R 214 to 0
			// G 214 to 0
			// B 255 to 130
            // Try blues: 255, 200

			R = 214 - System.Convert.ToInt16((214)*(Xval/(xDataAxisRange)));
			G = 214 - System.Convert.ToInt16((214)*(Xval/(xDataAxisRange)));
			B = 255 - System.Convert.ToInt16((125)*(Xval/(xDataAxisRange)));

			xColor[0] = R;
			xColor[1] = G;
			xColor[2] = B;

			return xColor;
		}

		// Allow user to adjust height of color bar.
		private void cboBarHeight_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_barHeight = System.Convert.ToUInt16 ( cboBarHeight.Text);
			//drawColorBar();
            panel2.Refresh();
		}

        // Refresh after user action
        private void rdoColor_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Refresh();
        }

        // Refresh after user action
        private void rdoGray_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Refresh();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            //openFileDialog1.DefaultExt = (".csv, .txt");
            openFileDialog1.Filter = "Text and Comma Separated files|*.csv; *.txt";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                /*System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close(); */

                importData(openFileDialog1.FileName);
                //drawCanvas();
                panel2.Refresh();
            };
        }

        // Refresh after user action
        private void chkSort_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Refresh();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            drawCanvas();
            drawColorBar();
           // panel2.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel2.Refresh();
        }

        private void panel1_Scroll(Object sender, ScrollEventArgs e)
        {
            //https://stackoverflow.com/questions/13415431/lag-between-scroll-event-and-paint-event-and-maybe-something-inbetween

            //panel2.Refresh();
            //Form.ActiveForm.Refresh();
            //drawCanvas();
            //drawColorBar();
        }

        // Refresh after user action
        private void cboBarSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_BarSize = System.Convert.ToInt16(cboBarSize.Text);
            panel2.Refresh();
        }
	}
}
