using SdfUtils.Utility.LinAlg;

namespace SdfUtils.Sdf
{
    //  SDF operation base class
    abstract class SdfOp
    {
        public virtual SdfResult CalcSdf(Vector3 p) { return new SdfResult(0, new Vector3(0)); }
    }

    //  unary SDF operation
    abstract class SdfUnaryOp : SdfOp
    {
        protected SdfOp sdf;    //  operand

        protected SdfUnaryOp(SdfOp _sdf) { sdf = _sdf; }
        public override SdfResult CalcSdf(Vector3 p) { return sdf.CalcSdf(p); }
    }

    //  boolean negation
    class SdfNegate : SdfUnaryOp
    {
        public SdfNegate(SdfOp _sdf) : base(_sdf) { }
        public override SdfResult CalcSdf(Vector3 p)
        {
            SdfResult t = sdf.CalcSdf(p);
            return new SdfResult(-t.distance, t.diffuse);
        }
    }

    //  basic matrix transformation, NB: is bound for scale operations, not exact
    class SdfTransform : SdfUnaryOp
    {
        private Matrix4 mat4;   //  transformation matrix

        public SdfTransform(SdfOp _sdf, Matrix4 _mat4) : base(_sdf) { mat4 = _mat4; }
        public override SdfResult CalcSdf(Vector3 p) { return sdf.CalcSdf(Matrix4.Invert(mat4).Transform(p)); } //  TODO: use inverted matrix instead
    }

    //  exact scale
    class SdfScale : SdfUnaryOp
    {
        private float s;    //  scale factor

        public SdfScale(SdfOp _sdf, float _s) : base(_sdf) { s = _s; }
        public override SdfResult CalcSdf(Vector3 p)
        {
            SdfResult t = sdf.CalcSdf(p / s);
            return new SdfResult(t.distance * s, t.diffuse);
        }
    }

    //  rounding operation, NB: expands the bounds of source sdf
    class SdfRound : SdfUnaryOp
    {
        private float r;    //  radius

        public SdfRound(SdfOp _sdf, float _r) : base(_sdf) { r = _r; }
        public override SdfResult CalcSdf(Vector3 p)
        {
            SdfResult t = sdf.CalcSdf(p);
            return new SdfResult(t.distance - r, t.diffuse);
        }
    }

    //  domain repetition
    class SdfRepeat : SdfUnaryOp
    {
        protected Vector3 c;  //  repetition period

        public SdfRepeat(SdfOp _sdf, Vector3 _c) : base(_sdf) { c = _c; }
        public override SdfResult CalcSdf(Vector3 p)
        {
            Vector3 q = Vector3.Mod(p + 0.5f * c, c) - 0.5f * c;
            return sdf.CalcSdf(q);
        }
    }

    //  finite domain repetition
    class SdfFiniteRepeat : SdfRepeat
    {
        private Vector3 l;  //  # of instances per axis

        public SdfFiniteRepeat(SdfOp _sdf, Vector3 _c, Vector3 _l) : base(_sdf, _c) { l = _l; }
        public override SdfResult CalcSdf(Vector3 p)
        {
            Vector3 q = p - c * Vector3.Clamp(Vector3.Round(p / c), -l, l);
            return sdf.CalcSdf(q);
        }
    }

    //  binary SDF operation
    abstract class SdfBinaryOp : SdfOp
    {
        protected SdfOp left;   //  left operand
        protected SdfOp right;  //  right operand

        protected SdfBinaryOp(SdfOp _left, SdfOp _right) { left = _left; right = _right; }
    }

    //  blend SDFs
    class SdfBlend : SdfBinaryOp
    {
        private float t;    //  blend factor

        public SdfBlend(SdfOp _left, SdfOp _right, float _t) : base(_left, _right) { t = _t; }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.Lerp(left.CalcSdf(p), right.CalcSdf(p), t); }
    }

    //  boolean union
    class SdfUnion : SdfBinaryOp
    {
        public SdfUnion(SdfOp _left, SdfOp _right) : base(_left, _right) { }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.Min(left.CalcSdf(p), right.CalcSdf(p)); }
    }

    //  smoothed
    class SdfSmoothUnion : SdfBinaryOp
    {
        private float k;    //  smoothing factor

        public SdfSmoothUnion(SdfOp _left, SdfOp _right, float _k) : base(_left, _right) { k = _k; }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.SmoothMin(left.CalcSdf(p), right.CalcSdf(p), k); }
    }

    //  boolean intersect
    class SdfIntersect : SdfBinaryOp
    {
        public SdfIntersect(SdfOp _left, SdfOp _right) : base(_left, _right) { }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.Max(left.CalcSdf(p), right.CalcSdf(p)); }
    }

    //  smoothed
    class SdfSmoothIntersect : SdfBinaryOp
    {
        private float k;    //  smoothing factor

        public SdfSmoothIntersect(SdfOp _left, SdfOp _right, float _k) : base(_left, _right) { k = _k; }
        public override SdfResult CalcSdf(Vector3 p) { return SdfResult.SmoothMax(left.CalcSdf(p), right.CalcSdf(p), k); }
    }

    //  boolean subtract
    class SdfSubtract : SdfIntersect
    {
        public SdfSubtract(SdfOp _left, SdfOp _right) : base(_left, new SdfNegate(_right)) { }
    }

    //  smoothed
    class SdfSmoothSubtract : SdfSmoothIntersect
    {
        public SdfSmoothSubtract(SdfOp _left, SdfOp _right, float _k) : base(_left, new SdfNegate(_right), _k) { }
    }
}
