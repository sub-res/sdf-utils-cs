using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    //  binary SDF operation
    abstract class SdfBinOp : SdfOp
    {
        protected SdfOp left;
        protected SdfOp right;

        protected SdfBinOp(SdfOp _left, SdfOp _right) { left = _left; right = _right; }
    }
}
