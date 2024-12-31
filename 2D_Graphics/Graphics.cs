using Microsoft.Build.Framework;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D_Graphics
{
    public partial class Graphics : Form
    {
        Painter painter;
        
        public Graphics()
        {
            InitializeComponent();

            painter = new Painter();

            pbFillColor.BackColor = painter.fillColor;
            pbPaintColor.BackColor = painter.paintColor;
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = new OpenGL();

            gl.ClearColor(0, 0, 0, 0);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            painter.handleClear();
            painter.handleUndo();
            painter.showShapes(gl);
            painter.handlePaint(gl);
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);
            gl.Ortho2D(0, openGLControl1.Width, 0, openGLControl1.Height);
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (painter.isTranslate)
            {
                painter.finishTransform();
                painter.isTranslate = false;
            }

            if (painter.isScale)
            {
                painter.finishTransform();
                painter.isScale = false;
            }

            if (painter.isRotate && painter.isDown)
            {
                painter.finishTransform();
                painter.isDown = false;
                painter.isRotate = false;
            }

            if (painter.isSinglePtMove)
            {
                painter.finishTransform();
                painter.isSinglePtMove = false;
            }

            if (painter.shapeType != ShapeType.Polygon)
            {
                String DrawingTime = String.Format("{0:0.000}", painter.timeSpan);
                txtDrawTime.Text = "Draw time: " + DrawingTime + " ms";

                if (painter.isDrawing)
                {
                    painter.isDrawing = false;
                    painter.isEditing = true;
                }
            }
        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            // Select shape
            if (painter.isSelect)
            {
                bool flagOut = true;
                Point current = e.Location;
                int maxIndex = painter.shapes.Count - 1;

                for (int i = maxIndex; i > -1; i--)
                {
                    if (painter.shapes[i].isInShape(current) || painter.shapes[i].isOnShape(current))
                    {
                        painter.curShape = painter.shapes[i];
                        painter.getProperties(painter.curShape);
                        painter.indexEdit = i;

                        painter.isEditing = true;
                        painter.isFree = false;

                        flagOut = false;
                        painter.isSelect = false;
                        break;
                    }
                }
                if (flagOut)
                {
                    return;
                }
            }

            // Start drawing
            if (painter.isFree)
            {
                if (painter.shapeType != ShapeType.Polygon)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        painter.pStart = e.Location;
                        painter.pEnd = painter.pStart;
                        painter.isDrawing = true;
                        painter.isFree = false;
                    }
                }
                else
                {
                    painter.isDrawing = true;
                    painter.isFree = false;
                }
            }

            // Start editing shape
            if (painter.isEditing)
            {
                Point current = e.Location;

                if (painter.curShape.isCtrlPt(current) != -1)
                {
                    painter.idxCtrlPt = painter.curShape.isCtrlPt(current);
                    painter.oldPos = current;

                    if (!painter.isRotate)
                    {
                        if ((painter.shapeType == ShapeType.Line || painter.shapeType == ShapeType.Polygon) && e.Button == MouseButtons.Right)
                        {
                            painter.isSinglePtMove = true;
                        }
                        else
                        {
                            painter.isScale = true;
                        }
                    }
                    else
                    {
                        // Clicked on control point
                        painter.isDown = true;
                    }
                }
                // Click outside of shape -> stop editing and save
                else if (!painter.curShape.isInShape(current) && !painter.curShape.isOnShape(current))
                {
                    painter.isEditing = false;
                    painter.doneDrawing = true;
                    painter.shapeType = painter.oldShapeType;
                }
                // Translate line
                else if (painter.shapeType == ShapeType.Line && painter.curShape.isOnShape(current))
                {
                    painter.oldPos = current;
                    painter.isTranslate = true;
                }
                // Translate other shapes
                else if (painter.shapeType != ShapeType.Line && painter.curShape.isInShape(current))
                {
                    painter.oldPos = current;
                    painter.isTranslate = true;
                }
            }
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            // Mouse hover to shapes
            if (painter.isSelect)
            {
                bool flagOut = true;
                Point current = e.Location;
                int maxIndex = painter.shapes.Count - 1;

                for (int k = maxIndex; k > -1; k--)
                {
                    if (painter.shapes[k].isInShape(current) || painter.shapes[k].isOnShape(current))
                    {
                        openGLControl1.Cursor = Cursors.Hand;
                        flagOut = false;
                        break;
                    }
                }

                if (flagOut)
                {
                    openGLControl1.Cursor = Cursors.Default;
                }
            }

            // Editing
            if (painter.isEditing)
            {
                Point current = new Point(e.Location.X, e.Location.Y);

                if (painter.isTranslate)
                {
                    painter.transformer = new transformations.Transformer();
                    painter.transformer.translate(current.X - painter.oldPos.X, current.Y - painter.oldPos.Y);

                    if (painter.isFractalMode)
                    {
                        if (painter.ifsTransformations.Count > 0)
                        {
                            painter.ifsTransformations[painter.ifsTransformations.Count - 1][0] = new transformations.Transformer();
                            painter.ifsTransformations[painter.ifsTransformations.Count - 1][0].translate(10, 10);
                            painter.ifsTransformations[painter.ifsTransformations.Count - 1][0].translate(current.X - painter.oldPos.X, current.Y - painter.oldPos.Y);
                        }
                    }

                    painter.handleTranslate();
                }
                else if (painter.isScale)
                {
                    painter.handleScale(current);
                }
                else if (painter.isRotate && painter.isDown)
                {
                    painter.handleRotate(current);
                }
                else if (painter.isSinglePtMove)
                {
                    painter.handleSinglePtMove(current);
                }
                else
                {
                    // Line
                    if (painter.shapeType == ShapeType.Line)
                    {
                        if (painter.curShape.isOnShape(current))
                        {
                            openGLControl1.Cursor = Cursors.SizeAll;
                        }
                        else if (painter.curShape.ctrlPts != null && painter.curShape.isCtrlPt(current) != -1)
                        {
                            openGLControl1.Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            openGLControl1.Cursor = Cursors.Default;
                        }
                    }
                    // Polygon
                    else if (painter.shapeType == ShapeType.Polygon)
                    {
                        if (painter.curShape.isInShape(current))
                        {
                            openGLControl1.Cursor = Cursors.SizeAll;
                        }
                        else if (painter.curShape.ctrlPts != null && painter.curShape.isCtrlPt(current) != -1)
                        {
                            openGLControl1.Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            openGLControl1.Cursor = Cursors.Default;
                        }
                    }
                    // Other shape
                    else
                    {
                        if (painter.curShape.isInShape(current))
                        {
                            openGLControl1.Cursor = Cursors.SizeAll;
                        }
                        else if (painter.curShape.ctrlPts != null && painter.curShape.isCtrlPt(current) != -1)
                        {
                            int index = painter.curShape.isCtrlPt(current);
                            if (index == 0 || index == 4)
                            {
                                openGLControl1.Cursor = Cursors.SizeNWSE;
                            }
                            if (index == 1 || index == 5)
                            {
                                openGLControl1.Cursor = Cursors.SizeNS;
                            }
                            if (index == 2 || index == 6)
                            {

                                openGLControl1.Cursor = Cursors.SizeNESW;
                            }
                            if (index == 3 || index == 7)
                            {
                                openGLControl1.Cursor = Cursors.SizeWE;
                            }
                        }
                        else
                        {
                            openGLControl1.Cursor = Cursors.Default;
                        }
                    }
                }
            }

            // Painting
            if (painter.isDrawing)
            {
                if (painter.shapeType != ShapeType.Polygon)
                {
                    bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                    if (isShiftPressed)
                    {
                        painter.isRegular = true;
                    }
                    else
                    {
                        painter.isRegular = false;
                    }

                    painter.pEnd = e.Location;
                }
                else
                {
                    painter.vertices.RemoveRange(painter.nVertices, painter.vertices.Count - painter.nVertices);
                    painter.vertices.Add(new Point(e.Location.X, e.Location.Y));
                }
            }
        }

        private void openGLControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (painter.shapeType == ShapeType.Polygon)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // If drawing
                    if (painter.isDrawing)
                    {
                        Point v = new Point(e.Location.X, e.Location.Y);
                        painter.vertices.Add(v);
                        painter.nVertices++;
                    }

                }

                if (e.Button == MouseButtons.Right)
                {
                    if (painter.isDrawing)
                    {
                        if (painter.nVertices > 1) // It's a polygon
                        {
                            painter.isDrawing = false;
                            painter.isEditing = true;

                            if (painter.nVertices != 0 && painter.nVertices == painter.vertices.Count - 1 && painter.vertices[painter.nVertices].Equals(painter.vertices[painter.nVertices - 1]))
                            {
                                painter.vertices.RemoveAt(painter.nVertices);
                                painter.curShape.setVertices(painter.vertices);
                            }
                        }
                    }
                }
            }
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Line;
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Triangle;
        }

        private void btnHexagon_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Hexagon;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Ellipse; // Initially set as Ellipse, change to Circle when Shift pressed
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Rectangle;
        }

        private void btnPentagon_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Pentagon;
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shapeType = ShapeType.Polygon;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (painter.isEditing)
            {
                painter.doneDrawing = true;
                painter.isDrawing = false;
                painter.isEditing = false;
            }

            painter.isSelect = true;
            painter.fillColor = colorDialog1.Color;
            painter.paintColor = colorDialog2.Color;
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            painter.isRotate = true;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            painter.isUndo = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            painter.isClear = true;
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            if (painter.isFilling)
            {
                painter.isFilling = false;
                btnFill.BackgroundImage = Properties.Resources.NoFillIcon;
            }
            else
            {
                painter.isFilling = true;
                painter.fillColor = colorDialog1.Color;
                btnFill.BackgroundImage = Properties.Resources.FillIcon;
            }
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                painter.fillColor = colorDialog1.Color;
                pbFillColor.BackColor = painter.fillColor;
            }
        }

        private void btnPaintColor_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                painter.paintColor = colorDialog2.Color;
                pbPaintColor.BackColor = painter.paintColor;
            }
        }

        private void cbxThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            int thickness = int.Parse(cbxThickness.Text);
            painter.thick = thickness;
        }

        private void btn_StartFractal_Click(object sender, EventArgs e)
        {
            painter.clearCanvas();
            painter.isFractalMode = !painter.isFractalMode;
            painter.initialFractalShape = null;
            painter.ifsTransformations.Clear();
        }

        private void btn_AddTransformation_Click(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;
            painter.addCopyShapeAndTransformer(gl); 
        }

        private void btn_DrawFractal_Click(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl1.OpenGL;

            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            painter.drawFractal(gl);
        }
    }
}
