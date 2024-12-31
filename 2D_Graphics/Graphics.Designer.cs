namespace _2D_Graphics
{
    partial class Graphics
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
            this.components = new System.ComponentModel.Container();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnPentagon = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnHexagon = new System.Windows.Forms.Button();
            this.btnTriangle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnFillColor = new System.Windows.Forms.Button();
            this.btnPaintColor = new System.Windows.Forms.Button();
            this.pbFillColor = new System.Windows.Forms.PictureBox();
            this.pbPaintColor = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.cbxThickness = new System.Windows.Forms.ComboBox();
            this.lblThick = new System.Windows.Forms.Label();
            this.txtDrawTime = new System.Windows.Forms.TextBox();
            this.btn_StartFractal = new System.Windows.Forms.Button();
            this.btn_AddTransformation = new System.Windows.Forms.Button();
            this.btn_DrawFractal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFillColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPaintColor)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.Location = new System.Drawing.Point(-5, 146);
            this.openGLControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(1508, 694);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.Resized += new System.EventHandler(this.openGLControl1_Resized);
            this.openGLControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseClick);
            this.openGLControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseDown);
            this.openGLControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseMove);
            this.openGLControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseUp);
            // 
            // btnPolygon
            // 
            this.btnPolygon.BackgroundImage = global::_2D_Graphics.Properties.Resources.PolygonIcon;
            this.btnPolygon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPolygon.Location = new System.Drawing.Point(433, 66);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(65, 55);
            this.btnPolygon.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnPolygon, "Polygon");
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // btnPentagon
            // 
            this.btnPentagon.BackgroundImage = global::_2D_Graphics.Properties.Resources.PentagonIcon;
            this.btnPentagon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPentagon.Location = new System.Drawing.Point(372, 66);
            this.btnPentagon.Name = "btnPentagon";
            this.btnPentagon.Size = new System.Drawing.Size(65, 55);
            this.btnPentagon.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnPentagon, "Pentagon");
            this.btnPentagon.UseVisualStyleBackColor = true;
            this.btnPentagon.Click += new System.EventHandler(this.btnPentagon_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.BackgroundImage = global::_2D_Graphics.Properties.Resources.RectangleIcon;
            this.btnRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRectangle.Location = new System.Drawing.Point(311, 66);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(65, 55);
            this.btnRectangle.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnRectangle, "Rectangle");
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.BackgroundImage = global::_2D_Graphics.Properties.Resources.CircleIcon;
            this.btnCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCircle.Location = new System.Drawing.Point(495, 12);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(65, 55);
            this.btnCircle.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnCircle, "Ellipse");
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnHexagon
            // 
            this.btnHexagon.BackgroundImage = global::_2D_Graphics.Properties.Resources.HexagonIcon;
            this.btnHexagon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHexagon.Location = new System.Drawing.Point(433, 12);
            this.btnHexagon.Name = "btnHexagon";
            this.btnHexagon.Size = new System.Drawing.Size(65, 55);
            this.btnHexagon.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnHexagon, "Hexagon");
            this.btnHexagon.UseVisualStyleBackColor = true;
            this.btnHexagon.Click += new System.EventHandler(this.btnHexagon_Click);
            // 
            // btnTriangle
            // 
            this.btnTriangle.BackgroundImage = global::_2D_Graphics.Properties.Resources.TriangleIcon;
            this.btnTriangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTriangle.Location = new System.Drawing.Point(372, 12);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(65, 55);
            this.btnTriangle.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnTriangle, "Triangle");
            this.btnTriangle.UseVisualStyleBackColor = true;
            this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
            // 
            // btnLine
            // 
            this.btnLine.BackgroundImage = global::_2D_Graphics.Properties.Resources.LineIcon;
            this.btnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLine.Location = new System.Drawing.Point(311, 12);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(65, 55);
            this.btnLine.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnLine, "Line");
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackgroundImage = global::_2D_Graphics.Properties.Resources.SelectIcon;
            this.btnSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelect.Location = new System.Drawing.Point(12, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(65, 55);
            this.btnSelect.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnSelect, "Select");
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(99, 12);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(70, 25);
            this.btnUndo.TabIndex = 10;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(99, 43);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 25);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.BackgroundImage = global::_2D_Graphics.Properties.Resources.RotateIcon;
            this.btnRotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRotate.Location = new System.Drawing.Point(240, 12);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(65, 55);
            this.btnRotate.TabIndex = 9;
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnFill
            // 
            this.btnFill.BackgroundImage = global::_2D_Graphics.Properties.Resources.FillIcon;
            this.btnFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFill.Location = new System.Drawing.Point(647, 12);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(65, 55);
            this.btnFill.TabIndex = 12;
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnFillColor
            // 
            this.btnFillColor.Location = new System.Drawing.Point(733, 12);
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(82, 25);
            this.btnFillColor.TabIndex = 13;
            this.btnFillColor.Text = "Fill Color";
            this.btnFillColor.UseVisualStyleBackColor = true;
            this.btnFillColor.Click += new System.EventHandler(this.btnFillColor_Click);
            // 
            // btnPaintColor
            // 
            this.btnPaintColor.Location = new System.Drawing.Point(733, 42);
            this.btnPaintColor.Name = "btnPaintColor";
            this.btnPaintColor.Size = new System.Drawing.Size(82, 25);
            this.btnPaintColor.TabIndex = 14;
            this.btnPaintColor.Text = "Paint Color";
            this.btnPaintColor.UseVisualStyleBackColor = true;
            this.btnPaintColor.Click += new System.EventHandler(this.btnPaintColor_Click);
            // 
            // pbFillColor
            // 
            this.pbFillColor.Location = new System.Drawing.Point(833, 12);
            this.pbFillColor.Name = "pbFillColor";
            this.pbFillColor.Size = new System.Drawing.Size(25, 25);
            this.pbFillColor.TabIndex = 15;
            this.pbFillColor.TabStop = false;
            // 
            // pbPaintColor
            // 
            this.pbPaintColor.Location = new System.Drawing.Point(833, 42);
            this.pbPaintColor.Name = "pbPaintColor";
            this.pbPaintColor.Size = new System.Drawing.Size(25, 25);
            this.pbPaintColor.TabIndex = 16;
            this.pbPaintColor.TabStop = false;
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.LightBlue;
            // 
            // colorDialog2
            // 
            this.colorDialog2.Color = System.Drawing.Color.White;
            // 
            // cbxThickness
            // 
            this.cbxThickness.FormattingEnabled = true;
            this.cbxThickness.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbxThickness.Location = new System.Drawing.Point(733, 79);
            this.cbxThickness.Name = "cbxThickness";
            this.cbxThickness.Size = new System.Drawing.Size(82, 24);
            this.cbxThickness.TabIndex = 17;
            this.cbxThickness.Text = "1";
            this.cbxThickness.SelectedIndexChanged += new System.EventHandler(this.cbxThickness_SelectedIndexChanged);
            // 
            // lblThick
            // 
            this.lblThick.AutoSize = true;
            this.lblThick.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThick.Location = new System.Drawing.Point(656, 79);
            this.lblThick.Name = "lblThick";
            this.lblThick.Size = new System.Drawing.Size(56, 24);
            this.lblThick.TabIndex = 18;
            this.lblThick.Text = "Thick";
            // 
            // txtDrawTime
            // 
            this.txtDrawTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrawTime.Location = new System.Drawing.Point(647, 109);
            this.txtDrawTime.Name = "txtDrawTime";
            this.txtDrawTime.Size = new System.Drawing.Size(211, 26);
            this.txtDrawTime.TabIndex = 19;
            // 
            // btn_StartFractal
            // 
            this.btn_StartFractal.Location = new System.Drawing.Point(928, 12);
            this.btn_StartFractal.Name = "btn_StartFractal";
            this.btn_StartFractal.Size = new System.Drawing.Size(145, 24);
            this.btn_StartFractal.TabIndex = 20;
            this.btn_StartFractal.Text = "Start Fractal";
            this.btn_StartFractal.UseVisualStyleBackColor = true;
            this.btn_StartFractal.Click += new System.EventHandler(this.btn_StartFractal_Click);
            // 
            // btn_AddTransformation
            // 
            this.btn_AddTransformation.Location = new System.Drawing.Point(928, 42);
            this.btn_AddTransformation.Name = "btn_AddTransformation";
            this.btn_AddTransformation.Size = new System.Drawing.Size(145, 24);
            this.btn_AddTransformation.TabIndex = 21;
            this.btn_AddTransformation.Text = "Add transformation";
            this.btn_AddTransformation.UseVisualStyleBackColor = true;
            this.btn_AddTransformation.Click += new System.EventHandler(this.btn_AddTransformation_Click);
            // 
            // btn_DrawFractal
            // 
            this.btn_DrawFractal.Location = new System.Drawing.Point(928, 72);
            this.btn_DrawFractal.Name = "btn_DrawFractal";
            this.btn_DrawFractal.Size = new System.Drawing.Size(145, 26);
            this.btn_DrawFractal.TabIndex = 22;
            this.btn_DrawFractal.Text = "Draw Fractal";
            this.btn_DrawFractal.UseVisualStyleBackColor = true;
            this.btn_DrawFractal.Click += new System.EventHandler(this.btn_DrawFractal_Click);
            // 
            // Graphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 835);
            this.Controls.Add(this.btn_DrawFractal);
            this.Controls.Add(this.btn_AddTransformation);
            this.Controls.Add(this.btn_StartFractal);
            this.Controls.Add(this.txtDrawTime);
            this.Controls.Add(this.lblThick);
            this.Controls.Add(this.cbxThickness);
            this.Controls.Add(this.pbPaintColor);
            this.Controls.Add(this.pbFillColor);
            this.Controls.Add(this.btnPaintColor);
            this.Controls.Add(this.btnFillColor);
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnPolygon);
            this.Controls.Add(this.btnPentagon);
            this.Controls.Add(this.btnRectangle);
            this.Controls.Add(this.btnCircle);
            this.Controls.Add(this.btnHexagon);
            this.Controls.Add(this.btnTriangle);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Graphics";
            this.Text = "2D Graphics";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFillColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPaintColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnTriangle;
        private System.Windows.Forms.Button btnHexagon;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnPentagon;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.Button btnPaintColor;
        private System.Windows.Forms.PictureBox pbFillColor;
        private System.Windows.Forms.PictureBox pbPaintColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.ComboBox cbxThickness;
        private System.Windows.Forms.Label lblThick;
        private System.Windows.Forms.TextBox txtDrawTime;
        private System.Windows.Forms.Button btn_StartFractal;
        private System.Windows.Forms.Button btn_AddTransformation;
        private System.Windows.Forms.Button btn_DrawFractal;
    }
}