using System;

using SdfUtils.Utility.LinAlg;

namespace SdfUtils.Sdf
{
    //  SDF primitive base class
    abstract class SdfPrimitive : SdfOp
    {
        protected Vector3 diffuse;  //  diffuse color

        protected SdfPrimitive(Vector3 _diffuse) { diffuse = _diffuse; }
        public override SdfResult CalcSdf(Vector3 p) { return new SdfResult(0, diffuse); }
    }

    //  halfspace as represented by plane
    class SdfPlane : SdfPrimitive
    {
        private Vector3 n;  //  normal
        private float d;    //  distance to origin

        public SdfPlane(Vector3 _n, float _d, Vector3 _diffuse) : base(_diffuse) { n = _n; d = _d; }
        public override SdfResult CalcSdf(Vector3 p) { return new SdfResult(Vector3.Dot(n, p) - d, diffuse); }
    }

    //  sphere
    class SdfSphere : SdfPrimitive
    {
        private float r;    //  radius

        public SdfSphere(float _r, Vector3 _diffuse) : base(_diffuse) { r = _r; }
        public override SdfResult CalcSdf(Vector3 p) { return new SdfResult(p.Length() - r, diffuse); }
    }

    //  box, NB: box dimensions b are halved, the distance function specified by Quilez creates a box of twice the provided dimensions
    class SdfBox : SdfPrimitive
    {
        private Vector3 b;  //  box dimensions

        public SdfBox(Vector3 _b, Vector3 _diffuse) : base(_diffuse) { b = _b * 0.5f; }
        public override SdfResult CalcSdf(Vector3 p)
        {
            Vector3 q = Vector3.Abs(p) - b;
            return new SdfResult(Vector3.Max(q, new Vector3(0)).Length() + Math.Min(Math.Max(q.x, Math.Max(q.y, q.z)), 0), diffuse);
        }
    }
}
