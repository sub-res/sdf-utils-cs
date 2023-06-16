using SdfUtils.Utility.LinAlg;

namespace SdfUtils.Sdf
{
    //  SDF operation base class
    abstract class SdfOp
    {
        public virtual SdfResult CalcSdf(Vector3 p) { return new SdfResult(0, new Vector3(0)); }
    }

    //  unary SDF operation
    abstract class SdfUnOp : SdfOp
    {
        protected SdfOp sdf;

        protected SdfUnOp(SdfOp _sdf) { sdf = _sdf; }
        public override SdfResult CalcSdf(Vector3 p) { return sdf.CalcSdf(p); }
    }

    //  boolean negation
    class SdfNegate : SdfUnOp
    {
        public SdfNegate(SdfOp _sdf) : base(_sdf) { }
        public override SdfResult CalcSdf(Vector3 p)
        {
            SdfResult t = sdf.CalcSdf(p);
            return new SdfResult(-t.distance, t.diffuse);
        }
    }

    //  basic matrix transformation
    class SdfTransform : SdfUnOp
    {
        private Matrix4 mat4;

        public SdfTransform(SdfOp _sdf, Matrix4 _mat4) : base(_sdf) { mat4 = _mat4; }
        public override SdfResult CalcSdf(Vector3 p) { return sdf.CalcSdf(Matrix4.Invert(mat4).Transform(p)); } //  TODO: use inverted matrix instead
    }

    //  rounding operation, NB: expands the bounds of source sdf
    class SdfRound : SdfUnOp
    {
        private float r;

        public SdfRound(SdfOp _sdf, float _r) : base(_sdf) { r = _r; }
        public override SdfResult CalcSdf(Vector3 p)
        {
            SdfResult t = sdf.CalcSdf(p);
            return new SdfResult(t.distance - r, t.diffuse);
        }
    }

    //  binary SDF operation
    abstract class SdfBinOp : SdfOp
    {
        protected SdfOp left;
        protected SdfOp right;

        protected SdfBinOp(SdfOp _left, SdfOp _right) { left = _left; right = _right; }
    }

    //  boolean union
    class SdfUnion : SdfBinOp
    {
        public SdfUnion(SdfOp _left, SdfOp _right) : base(_left, _right) { }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.Min(left.CalcSdf(p), right.CalcSdf(p)); }
    }

    //  boolean intersect
    class SdfIntersect : SdfBinOp
    {
        public SdfIntersect(SdfOp _left, SdfOp _right) : base(_left, _right) { }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.Max(left.CalcSdf(p), right.CalcSdf(p)); }
    }

    //  boolean subtract
    class SdfSubtract : SdfIntersect
    {
        public SdfSubtract(SdfOp _left, SdfOp _right) : base(_left, new SdfNegate(_right)) { }
    }
}
