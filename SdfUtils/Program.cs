using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using SdfUtils.Utility;
using SdfUtils.Utility.LinAlg;
using SdfUtils.Sdf;

namespace SdfUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        //  throwaway testing method
        static void Test()
        {
            int sz = 256;

            SdfOp sdf = new SdfRepeat(
                new SdfSmoothUnion(
                    new SdfBox(new Vector3(32, 32, 32), new Vector3(0xff, 0, 0)),
                    new SdfTransform(
                        new SdfSphere(16, new Vector3(0, 0xff, 0)),
                        Matrix4.Translation(16, 16, 0)
                    ),
                    16
                ),
                new Vector3(64)
            );

            Bitmap bmp = new Bitmap(sz, sz);

            //  TODO: use BitmapData instead for easy parallelization
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);

                for (int i = 0; i < sz; i++)
                {
                    for (int j = 0; j < sz; j++)
                    {
                        SdfResult sr = sdf.CalcSdf(new Vector3(i, j, 0));
                        if (sr.distance <= 0)
                        {
                            Color c = Color.FromArgb(
                                (int)Algo.Clamp(sr.diffuse.x, 0, 0xff),
                                (int)Algo.Clamp(sr.diffuse.y, 0, 0xff),
                                (int)Algo.Clamp(sr.diffuse.z, 0, 0xff)
                            );

                            g.FillRectangle(new SolidBrush(c), i, j, 1, 1);
                        }
                    }
                }
            }

            bmp.Save("test_output.png");
        }
    }
}
