using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics.transformations
{
    internal class Transformer
    {
        double[][] matComposite;

        public Transformer()
        {
            matrix3x3SetIdentity(ref matComposite);
        }

        public void matrix3x3SetIdentity(ref double[][] mat)
        {
            mat = new double[3][];
            mat[0] = new double[] { 1, 0, 0 };
            mat[1] = new double[] { 0, 1, 0 };
            mat[2] = new double[] { 0, 0, 1 };
        }

        public void matrix3x3PreMultiply(double[][] mat1, ref double[][] mat2)
        {
            int row, col;
            double[][] matTmp = new double[3][];
            matrix3x3SetIdentity(ref matTmp);

            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    matTmp[row][col] = mat1[row][0] * mat2[0][col]
                        + mat1[row][1] * mat2[1][col]
                        + mat1[row][2] * mat2[2][col];
                }
            }

            for (row = 0; row < 3; row++)
                for (col = 0; col < 3; col++)
                    mat2[row][col] = matTmp[row][col];
        }

        public void setMatCompositeToIdentity()
        {
            matrix3x3SetIdentity(ref matComposite);
        }

        public void translate(int tx, int ty)
        {
            double[][] matTrans = new double[3][];

            matrix3x3SetIdentity(ref matTrans);

            matTrans[0][2] = tx;
            matTrans[1][2] = ty;

            matrix3x3PreMultiply(matTrans, ref matComposite);
        }

        public void rotate(Point pivotPt, double theta)
        {
            double[][] matRot = new double[3][];
            matrix3x3SetIdentity(ref matRot);

            matRot[0][0] = Math.Cos(theta);
            matRot[0][1] = -Math.Sin(theta);
            matRot[0][2] = pivotPt.X * (1 - Math.Cos(theta))
                + pivotPt.Y * Math.Sin(theta);

            matRot[1][0] = Math.Sin(theta);
            matRot[1][1] = Math.Cos(theta);
            matRot[1][2] = pivotPt.Y * (1 - Math.Cos(theta))
                - pivotPt.X * Math.Sin(theta);

            matrix3x3PreMultiply(matRot, ref matComposite);
        }

        public void scale(Point fixedPt, double sx, double sy)
        {
            double[][] matScale = new double[3][];
            matrix3x3SetIdentity(ref matScale);

            matScale[0][0] = sx;
            matScale[0][2] = fixedPt.X * (1 - sx);
            matScale[1][1] = sy;
            matScale[1][2] = fixedPt.Y * (1 - sy);

            matrix3x3PreMultiply(matScale, ref matComposite);
        }

        public Point transformPoint(Point pt)
        {
            double tempX = matComposite[0][0] * pt.X
                + matComposite[0][1] * pt.Y
                + matComposite[0][2];
            double tempY = matComposite[1][0] * pt.X
                + matComposite[1][1] * pt.X
                + matComposite[1][2];

            return new Point(Convert.ToInt32(tempX), Convert.ToInt32(tempY)) ;
        }

        public List<Point> transformVerts(List<Point> verts, int nVerts)
        {
            int k;
            double tempX = 0, tempY = 0;

            List<Point> result = new List<Point>();
            for (k = 0; k < nVerts; k++)
            {
                tempX = matComposite[0][0] * verts[k].X
                    + matComposite[0][1] * verts[k].Y
                    + matComposite[0][2];
                tempY = matComposite[1][0] * verts[k].X
                    + matComposite[1][1] * verts[k].Y
                    + matComposite[1][2];

                //verts[k] = new Point(Convert.ToInt32(tempX), Convert.ToInt32(tempY);
                result.Add(new Point(Convert.ToInt32(tempX), Convert.ToInt32(tempY)));
            }

            return result;
        }
    }
}
