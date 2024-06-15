﻿namespace _2D_Graphics
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
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnTriangle = new System.Windows.Forms.Button();
            this.btnHexagon = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnPentagon = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
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
            // btnSelect
            // 
            this.btnSelect.BackgroundImage = global::_2D_Graphics.Properties.Resources.SelectIcon;
            this.btnSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelect.Location = new System.Drawing.Point(12, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(65, 55);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnLine
            // 
            this.btnLine.BackgroundImage = global::_2D_Graphics.Properties.Resources.LineIcon;
            this.btnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLine.Location = new System.Drawing.Point(311, 12);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(65, 55);
            this.btnLine.TabIndex = 2;
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnTriangle
            // 
            this.btnTriangle.BackgroundImage = global::_2D_Graphics.Properties.Resources.TriangleIcon;
            this.btnTriangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTriangle.Location = new System.Drawing.Point(372, 12);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(65, 55);
            this.btnTriangle.TabIndex = 3;
            this.btnTriangle.UseVisualStyleBackColor = true;
            this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
            // 
            // btnHexagon
            // 
            this.btnHexagon.BackgroundImage = global::_2D_Graphics.Properties.Resources.HexagonIcon;
            this.btnHexagon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHexagon.Location = new System.Drawing.Point(433, 12);
            this.btnHexagon.Name = "btnHexagon";
            this.btnHexagon.Size = new System.Drawing.Size(65, 55);
            this.btnHexagon.TabIndex = 4;
            this.btnHexagon.UseVisualStyleBackColor = true;
            this.btnHexagon.Click += new System.EventHandler(this.btnHexagon_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.BackgroundImage = global::_2D_Graphics.Properties.Resources.CircleIcon;
            this.btnCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCircle.Location = new System.Drawing.Point(495, 12);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(65, 55);
            this.btnCircle.TabIndex = 5;
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.BackgroundImage = global::_2D_Graphics.Properties.Resources.RectangleIcon;
            this.btnRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRectangle.Location = new System.Drawing.Point(311, 66);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(65, 55);
            this.btnRectangle.TabIndex = 6;
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnPentagon
            // 
            this.btnPentagon.BackgroundImage = global::_2D_Graphics.Properties.Resources.PentagonIcon;
            this.btnPentagon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPentagon.Location = new System.Drawing.Point(372, 66);
            this.btnPentagon.Name = "btnPentagon";
            this.btnPentagon.Size = new System.Drawing.Size(65, 55);
            this.btnPentagon.TabIndex = 7;
            this.btnPentagon.UseVisualStyleBackColor = true;
            this.btnPentagon.Click += new System.EventHandler(this.btnPentagon_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.BackgroundImage = global::_2D_Graphics.Properties.Resources.PolygonIcon;
            this.btnPolygon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPolygon.Location = new System.Drawing.Point(433, 66);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(65, 55);
            this.btnPolygon.TabIndex = 8;
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // Graphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 835);
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
            this.Text = "Graphics";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);

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
    }
}