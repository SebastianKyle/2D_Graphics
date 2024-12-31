using _2D_Graphics.shapes;
using _2D_Graphics.transformations;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Transformer transformer;

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

        public bool isRegular;

        // Fractal fields
        public List<List<Transformer>> ifsTransformations;
        public bool isFractalMode;
        public int fractalIterations;
        public Shape initialFractalShape;

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

            transformer = null;

            isTranslate = false;
            isScale = false;
            isRotate = false;
            isDown = false;
            isSinglePtMove = false;

            isRegular = false;

            ifsTransformations = new List<List<Transformer>>();
            isFractalMode = false;
            fractalIterations = 5;
            initialFractalShape = null;
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
            else if (shapeType == ShapeType.Ellipse)
            {
                curShape = new Ellipse(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling, isRegular);
            }
            else if (shapeType == ShapeType.Triangle)
            {
                curShape = new Triangle(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling, isRegular);
            }
            else if (shapeType == ShapeType.Rectangle)
            {
                curShape = new Rectangle(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling, isRegular);
            }
            else if (shapeType == ShapeType.Pentagon)
            {
                curShape = new Pentagon(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling);
            }
            else if (shapeType == ShapeType.Hexagon)
            {
                curShape = new Hexagon(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling);
            }
            else if (shapeType == ShapeType.Polygon)
            {
                curShape = new Polygon(vertices, pStart, pEnd, thick, paintColor, fillColor, isFilling);
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

                if (isFractalMode && initialFractalShape == null)
                {
                    initialFractalShape = curShape;
                }

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
            verticesTransformed = transformer.transformVerts(vertices, vertices.Count);
            ctrlPtsTransformed = transformer.transformVerts(ctrlPts, ctrlPts.Count);

            if (shapeType == ShapeType.Ellipse)
            {
                ptsTransformed = transformer.transformVerts(pts, pts.Count);
            }

            centerTransformed = transformer.transformPoint(center);
        }

        public void handleScale(Point cur)
        {
            Point vcc = new Point(cur.X - center.X, cur.Y - center.Y);  // Vector cur-center
            double lenVcc = Math.Sqrt(vcc.X * vcc.X + vcc.Y * vcc.Y);   // vcc norm

            Point voc = new Point(oldPos.X - center.X, oldPos.Y - center.Y);    // Vector old-center
            double lenVoc = Math.Sqrt(voc.X * voc.X + voc.Y * voc.Y);           // voc norm

            double sx, sy;
            Point fixedPt;
            if (shapeType == ShapeType.Pentagon || shapeType == ShapeType.Hexagon || isRegular)
            {
                fixedPt = new Point(ctrlPts[(idxCtrlPt + 4) % 8].X, ctrlPts[(idxCtrlPt + 4) % 8].Y);

                Point vcf = new Point(cur.X - fixedPt.X, cur.Y - fixedPt.Y);    // Vector cur-fixed
                double lenVcf = Math.Sqrt(vcf.X * vcf.X + vcf.Y * vcf.Y);       // vcf norm
                Point vof = new Point(oldPos.X - fixedPt.X, oldPos.Y - fixedPt.Y);  // Vector old-fixed
                double lenVof = Math.Sqrt(vof.X * vof.X + vof.Y * vof.Y);           // vof norm

                sy = sx = lenVcf / lenVof;
            }
            else if (shapeType == ShapeType.Line || shapeType == ShapeType.Polygon)
            {
                sx = vcc.X * 1.0 / voc.X;
                sy = vcc.Y * 1.0 / voc.Y;

                fixedPt = new Point(center.X, center.Y);
            }
            else
            {
                fixedPt = new Point(ctrlPts[(idxCtrlPt + 4) % 8].X, ctrlPts[(idxCtrlPt + 4) % 8].Y);

                Point vcf = new Point(cur.X - fixedPt.X, cur.Y - fixedPt.Y);
                double lenVcf = Math.Sqrt(vcf.X * vcf.X + vcf.Y * vcf.Y);
                Point vof = new Point(oldPos.X - fixedPt.X, oldPos.Y - fixedPt.Y);
                double lenVof = Math.Sqrt(vof.X * vof.X + vof.Y * vof.Y);

                if (idxCtrlPt == 1 || idxCtrlPt == 5) // Scale vertically
                {
                    sx = 1;
                    sy = lenVcf / lenVof;
                }
                else if (idxCtrlPt == 3 || idxCtrlPt == 7) // Scale horixontally
                {
                    sy = 1;
                    sx = lenVcf / lenVof;
                }
                else // Scale diagonally
                {
                    sx = vcf.X * 1.0 / vof.X;
                    sy = vcf.Y * 1.0 / vof.Y;
                }
            }

            transformer = new Transformer();
            transformer.scale(fixedPt, sx, sy);

            double[][] scaleCompositeMat = transformer.getMatComposite();
            if (shapeType == ShapeType.Ellipse)
            {
                Transformer tmpTrans = new Transformer();
                tmpTrans.rotate(center, -curShape.angleRotated);

                scaleCompositeMat = tmpTrans.getMatComposite();

                List<Point> tmpVertices = tmpTrans.transformVerts(vertices, vertices.Count);    // Rotate to original orientation
                tmpVertices = transformer.transformVerts(tmpVertices, tmpVertices.Count);       // Scale

                transformer.matrix3x3PreMultiply(transformer.getMatComposite(), ref scaleCompositeMat);

                Shape tempElip = new Ellipse(null, tmpVertices[1], tmpVertices[2], thick, paintColor, fillColor, isFilling, isRegular);
                List<Point> tmpPts = tempElip.getPoints();

                List<Point> tmpCtrlPts = tempElip.getCtrlPoints();

                transformer.setMatCompositeToIdentity();
                transformer.rotate(center, curShape.angleRotated);      // Rotate back to current orientation
                transformer.matrix3x3PreMultiply(transformer.getMatComposite(), ref scaleCompositeMat);

                verticesTransformed = transformer.transformVerts(tmpVertices, tmpVertices.Count);
                ptsTransformed = transformer.transformVerts(tmpPts, tmpPts.Count);
                ctrlPtsTransformed = transformer.transformVerts(tmpCtrlPts, tmpCtrlPts.Count);
            }
            else if (shapeType == ShapeType.Rectangle)
            {
                Transformer tmpTrans = new Transformer();
                tmpTrans.rotate(center, -curShape.angleRotated);

                scaleCompositeMat = tmpTrans.getMatComposite();

                List<Point> tmpVertices = tmpTrans.transformVerts(vertices, vertices.Count);
                tmpVertices = transformer.transformVerts(tmpVertices, tmpVertices.Count);

                transformer.matrix3x3PreMultiply(transformer.getMatComposite(), ref scaleCompositeMat);

                Shape tmpRec = new Rectangle(null, tmpVertices[0], tmpVertices[2], thick, paintColor, fillColor, isFilling, isRegular);

                List<Point> tmpCtrlPts = tmpRec.getCtrlPoints();

                transformer.setMatCompositeToIdentity();
                transformer.rotate(center, curShape.angleRotated);
                transformer.matrix3x3PreMultiply(transformer.getMatComposite(), ref scaleCompositeMat); 

                verticesTransformed = transformer.transformVerts(tmpVertices, tmpVertices.Count);
                ctrlPtsTransformed = transformer.transformVerts(tmpCtrlPts, tmpCtrlPts.Count);
            }
            else if (shapeType == ShapeType.Triangle)
            {
                Transformer tmpTrans = new Transformer();
                tmpTrans.rotate(center, -curShape.angleRotated);

                scaleCompositeMat = tmpTrans.getMatComposite();

                List<Point> tmpVertices = tmpTrans.transformVerts(vertices, vertices.Count);
                tmpVertices = transformer.transformVerts(tmpVertices, tmpVertices.Count);
                transformer.matrix3x3PreMultiply(transformer.getMatComposite(), ref scaleCompositeMat);

                Shape tmpTri = new Triangle(null, new Point(tmpVertices[1].X, tmpVertices[0].Y), tmpVertices[2], thick, paintColor, fillColor, isFilling, isRegular);

                List<Point> tmpCtrlPts = tmpTri.getCtrlPoints();

                transformer.setMatCompositeToIdentity();
                transformer.rotate(center, curShape.angleRotated);
                transformer.matrix3x3PreMultiply(transformer.getMatComposite(), ref scaleCompositeMat);

                verticesTransformed = transformer.transformVerts(tmpVertices, tmpVertices.Count);
                ctrlPtsTransformed = transformer.transformVerts(tmpCtrlPts, tmpCtrlPts.Count);
            }
            else
            {

                verticesTransformed = transformer.transformVerts(vertices, vertices.Count);
                ctrlPtsTransformed = transformer.transformVerts(ctrlPts, ctrlPts.Count);

            }

            if (isFractalMode)
            {
                if (ifsTransformations.Count > 0)
                {
                    ifsTransformations[ifsTransformations.Count - 1][1] = new Transformer();
                    ifsTransformations[ifsTransformations.Count - 1][1].setMatComposite(scaleCompositeMat);

                    double[][] matComposite = ifsTransformations[ifsTransformations.Count - 1][1].getMatComposite();
                    Console.WriteLine("Scale: " + matComposite[0][0] + " " + matComposite[0][1] + " " + matComposite[0][2]);
                    Console.WriteLine("       " + matComposite[1][0] + " " + matComposite[1][1] + " " + matComposite[1][2]);
                }
            }
        }

        public void handleRotate(Point cur)
        {
            Point vcc = new Point(cur.X - curShape.getCenter().X, cur.Y - curShape.getCenter().Y);  // Vector cur-center
            double lenVcc = Math.Sqrt(vcc.X * vcc.X + vcc.Y * vcc.Y);   // vcc norm

            Point voc = new Point(oldPos.X - curShape.getCenter().X, oldPos.Y - curShape.getCenter().Y);    // Vector old-center
            double lenVoc = Math.Sqrt(voc.X * voc.X + voc.Y * voc.Y);           // voc norm

            angle = Math.Acos((vcc.X * voc.X + vcc.Y * voc.Y) / (lenVcc * lenVoc));
            if (voc.X * vcc.Y - voc.Y * vcc.X < 0)  // z-value of cross product < 0 -> counter-clockwise
            {
                angle *= -1;
            }

            transformer = new Transformer();
            transformer.rotate(curShape.getCenter(), angle);

            if (isFractalMode)
            {
                if (ifsTransformations.Count > 0)
                {
                    ifsTransformations[ifsTransformations.Count - 1][2] = new Transformer();
                    ifsTransformations[ifsTransformations.Count - 1][2].setMatComposite(transformer.getMatComposite());
                }
            }

            verticesTransformed = transformer.transformVerts(vertices, vertices.Count);
            if (shapeType == ShapeType.Ellipse)
            {
                ptsTransformed = transformer.transformVerts(pts, pts.Count);
            }
            ctrlPtsTransformed = transformer.transformVerts(ctrlPts, ctrlPts.Count);
        }

        public void finishTransform()
        {
            vertices = verticesTransformed;
            ctrlPts = ctrlPtsTransformed;
            pts = ptsTransformed;
            center = centerTransformed;

            curShape.setVertices(verticesTransformed);
            curShape.ctrlPts = ctrlPtsTransformed;
            //curShape.pts = ptsTransformed;
            //curShape.center = centerTransformed;

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

        public void setIsRegular(bool isRegular)
        {
            this.isRegular = isRegular;
        }

        public void clearCanvas()
        {
            shapes.Clear();
            //ifsTransformations.Clear();
            //isFractalMode = false;
            curShape = null;
            isDrawing = false;
            doneDrawing = false;
            isEditing = false;
            isFree = true;
        }

        public void addCopyShapeAndTransformer(OpenGL gl)
        {
            Shape newShape = initialFractalShape.Clone();
            getProperties(newShape);
            shapes.Add(newShape);
            curShape = newShape;

            this.transformer = new Transformer();
            this.transformer.translate(10, 10);
            handleTranslate(); 
            curShape.editShape(verticesTransformed, ptsTransformed, ctrlPtsTransformed, thick, paintColor, fillColor, isFilling);

            curShape.showEditShape(gl);

            Transformer transformer = new Transformer();
            ifsTransformations.Add(new List<Transformer>() { new Transformer(), new Transformer(), new Transformer() });
            transformer.translate(10, 10);
            ifsTransformations[ifsTransformations.Count - 1][0] = transformer;
        }

        public void addTransformation(Transformer transformer)
        {
            if (isFractalMode)
            {
                //ifsTransformations.Add(transformer);
            }
        }

        public void applyTransformations()
        {
            if (curShape != null && isFractalMode)
            {
                foreach (var transformerSet in ifsTransformations)
                {
                    foreach (var transformer in transformerSet) // translate, scale, rotate
                    {
                        verticesTransformed = transformer.transformVerts(verticesTransformed, verticesTransformed.Count);
                        ctrlPtsTransformed = transformer.transformVerts(ctrlPtsTransformed, ctrlPtsTransformed.Count);
                        ptsTransformed = transformer.transformVerts(ptsTransformed, ptsTransformed.Count);
                        centerTransformed = transformer.transformPoint(centerTransformed);
                    }
                }
            }
        }

        public void drawFractal(OpenGL gl)
        {
            if (ifsTransformations.Count == 0)
            {
                return;
            }

            clearCanvas();

            shapes.Add(initialFractalShape);
            curShape = initialFractalShape;
            curShape.showEditShape(gl);

            Console.WriteLine("IFS Transformations: " + ifsTransformations.Count);

            for (int i = 0; i < fractalIterations; i++)
            {
                Shape newShape = shapes[shapes.Count - 1].Clone(); // Assuming Shape class has a Clone method
                curShape = newShape;
                shapes.Add(curShape);
                getProperties(curShape);
                applyTransformations();
                curShape.editShape(verticesTransformed, ptsTransformed, ctrlPtsTransformed, thick, paintColor, fillColor, isFilling);
                curShape.setVertices(verticesTransformed);
                curShape.ctrlPts = ctrlPtsTransformed;
                curShape.showShape(gl);
                //curShape = newShape;
            }
        }

    }
}
