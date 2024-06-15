using SharpGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Painter
    {
        public Color paintColor;
        public ShapeType shapeType;

        public ShapeType oldShapeType;
        public Point pStart, pEnd;
        public bool isDrawing;
        public bool doneDrawing;
        public bool isEditing;
        public bool isFree;

        public float thick;
        public bool isClear;
        public bool isUndo;
        public bool isSelect;

        public int indexEdit;

        public double timeSpan;

        public Color fillColor;
        public bool isFilling;

        public List<Shape> shapes;
        public Shape curShape;

        public Stopwatch stopwatch;

        //public Transformer transformer;

        public List<Point> vertices;
        public int nVertices;
        public List<Point> verticesTransformed;

        public List<Point> pts;
        public List<Point> ptsTransformed;

        public List<Point> ctrlPts;
        public List<Point> ctrlPtsTransformed;

        public Point oldPos;
        public bool isTranslate;
        public bool isRotate;
        public bool isScale;
        public bool isSinglePtMove;

        public Point center;
        public Point centerTransformed;

        public int idxCtrlPt;
        double angle;

        public bool isDown;

        public Painter()
        {
            paintColor = Color.White;
            fillColor = Color.LightBlue;
            shapeType = ShapeType.Line;

            isDrawing = false;
            doneDrawing = false;
            isEditing = false;
            isFree = true;

            thick = 1f;
            shapes = new List<Shape>();
            curShape = null;

            isClear = false;
            isUndo = false;
            isSelect = false;
            indexEdit = -1;

            stopwatch = new Stopwatch();
            timeSpan = 0;
            isFilling = false;

            vertices = new List<Point>();
            nVertices = 0;
            pts = new List<Point>();
            ctrlPts = new List<Point>();

            //transformer = null;

            isTranslate = false;
            isScale = false;
            isRotate = false;
            isDown = false;
            isSinglePtMove = false;
        }

        public void showShapes(OpenGL gl)
        {
            for (int k = 0; k < shapes.Count; k++)
            {
                shapes[k].showShape(gl);
            }
        }

        public void handleClear()
        {
            if (isClear)
            {
                if (isEditing)
                {
                    curShape = null;
                    isEditing = false;
                    isFree = true;
                }

                shapes.Clear();
                isClear = false;
                indexEdit = -1;

                if (vertices.Count > 0)
                {
                    vertices.Clear();
                }
                nVertices = 0;
            }
        }

        public void handleUndo()
        {
            if (isUndo)
            {
                if (isFree)
                {
                    if (shapes.Count > 0)
                    {
                        shapes.RemoveAt(shapes.Count - 1);
                    }
                }

                if (isEditing)
                {
                    if (indexEdit != -1)
                    {
                        shapes.RemoveAt(indexEdit);
                        indexEdit = -1;
                    }

                    curShape = null;
                    isEditing = false;
                    isFree = true;
                }

                isUndo = false;
                if (vertices.Count != 0)
                {
                    vertices.Clear();
                }
                nVertices = 0;
            }
        }

        public void createShape()
        {
            if (shapeType == ShapeType.Line)
            {
                curShape = new Line(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling);
            }
            else if (shapeType == ShapeType.Circle)
            {
                //curShape = new Circle(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shapeType == ShapeType.Ellipse)
            {
                //curShape = new Ellipse(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shapeType == ShapeType.Rectangle)
            {
                curShape = new Rectangle(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling);
            }
            else if (shapeType == ShapeType.Pentagon)
            {
                //curShape = new RegularPentagon(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shapeType == ShapeType.Hexagon)
            {
                //curShape = new RegularHexagon(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shapeType == ShapeType.Polygon)
            {
                //curShape = new Polygon(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
        }

        public void handleDrawing(OpenGL gl)
        {
            createShape();

            stopwatch.Start();
            curShape.showShape(gl);
            stopwatch.Stop();

            timeSpan = stopwatch.Elapsed.TotalMilliseconds * 1000;
            timeSpan /= 1000;
            getProperties(curShape);
        }

        public void handleEdit(OpenGL gl)
        {
            curShape.editShape(verticesTransformed, ptsTransformed, ctrlPtsTransformed, thick, paintColor, fillColor, isFilling);
            curShape.showEditShape(gl);
        }

        public void handleDrawDone()
        {
            if (indexEdit == -1)
            {
                shapes.Add(curShape);
                curShape = null;
            }
            else indexEdit = -1;

            if (vertices.Count != 0)
            {
                vertices.Clear();
            }
            nVertices = 0;

            doneDrawing = false;
            isFree = true;
        }

        public void handlePaint(OpenGL gl)
        {
            if (isDrawing || isEditing || doneDrawing)
            {
                if (curShape != null && isDrawing == false)
                {
                    curShape.showShape(gl);
                }
                if (isDrawing)
                    handleDrawing(gl);
                if (isEditing)
                    handleEdit(gl);
                if (doneDrawing)
                    handleDrawDone();

                stopwatch.Reset();
            }
        }

        public void getProperties(Shape shape)
        {
            vertices = shape.getVertices();
            verticesTransformed = shape.getVertices();

            pts = shape.getPoints();
            ptsTransformed = shape.getPoints();

            ctrlPts = shape.getCtrlPoints();
            ctrlPtsTransformed = shape.getCtrlPoints();

            thick = shape.getThick();
            paintColor = shape.GetColor();
            fillColor = shape.getFillColor();
            isFilling = shape.getFillVal();

            center = shape.getCenter();
            centerTransformed = shape.getCenter();

            oldShapeType = shapeType;
            shapeType = shape.getShapeType();
        }

        public void turnOffActiveMode()
        {
            if (isDrawing || isEditing)
            {
                doneDrawing = true;
                isDrawing = false;
                isEditing = false;
            }

            isSelect = false;
            isRotate = false;
        }

        public void handleTranslate()
        {

        }

        public void handleScale(Point fixedPt)
        {

        }

        public void handleRotate(Point refPt)
        {

        }

        public void finishTransform()
        {
            vertices = verticesTransformed;
            ctrlPts = ctrlPtsTransformed;
            pts = ptsTransformed;
            center = centerTransformed;

            if (isRotate)
            {
                curShape.angleRotated += angle;
            }
        }

        public void handleSinglePtMove(Point desPt)
        {
            verticesTransformed[idxCtrlPt] = desPt;
            ctrlPtsTransformed[idxCtrlPt] = desPt;
        }

    }
}
