using System;

namespace SdfUtils.Utility.LinAlg
{
    struct Matrix4
    {
        public static readonly Matrix4 IDENTITY = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        public readonly float m00;
        public readonly float m01;
        public readonly float m02;
        public readonly float m03;

        public readonly float m10;
        public readonly float m11;
        public readonly float m12;
        public readonly float m13;

        public readonly float m20;
        public readonly float m21;
        public readonly float m22;
        public readonly float m23;

        public readonly float m30;
        public readonly float m31;
        public readonly float m32;
        public readonly float m33;

        public Matrix4(float _m00, float _m01, float _m02, float _m03, float _m10, float _m11, float _m12, float _m13, float _m20, float _m21, float _m22, float _m23, float _m30, float _m31, float _m32, float _m33)
        {
            m00 = _m00;
            m01 = _m01;
            m02 = _m02;
            m03 = _m03;

            m10 = _m10;
            m11 = _m11;
            m12 = _m12;
            m13 = _m13;

            m20 = _m20;
            m21 = _m21;
            m22 = _m22;
            m23 = _m23;

            m30 = _m30;
            m31 = _m31;
            m32 = _m32;
            m33 = _m33;
        }

        public Matrix4 Transpose()
        {
            return new Matrix4(
                m00, m10, m20, m30,
                m01, m11, m21, m31,
                m02, m12, m22, m32,
                m03, m13, m23, m33
            );
        }

        public static Matrix4 Translation(float x, float y, float z)
        {
            return new Matrix4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1
            );
        }

        public static Matrix4 RotationX(float r)
        {
            float s = (float)Math.Sin(r);
            float c = (float)Math.Cos(r);

            return new Matrix4(
                1, 0, 0, 0,
                0, c, -s, 0,
                0, s, c, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 RotationY(float r)
        {
            float s = (float)Math.Sin(r);
            float c = (float)Math.Cos(r);

            return new Matrix4(
                c, 0, s, 0,
                0, 1, 0, 0,
                -s, 0, c, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 RotationZ(float r)
        {
            float s = (float)Math.Sin(r);
            float c = (float)Math.Cos(r);

            return new Matrix4(
                c, -s, 0, 0,
                s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 Scaling(float x, float y, float z)
        {
            return new Matrix4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1
            );
        }
        /*
        public Vector2 Transform(Vector2 v)
        {
            return new Vector2(
                m00 * v.x + m01 * v.y + m02 + m03,
                m10 * v.x + m11 * v.y + m12 + m13
            );
        }
        */
        public Vector3 Transform(Vector3 v)
        {
            return new Vector3(
                m00 * v.x + m01 * v.y + m02 * v.z + m03,
                m10 * v.x + m11 * v.y + m12 * v.z + m13,
                m20 * v.x + m21 * v.y + m22 * v.z + m23
            );
        }
        /*
        public Vector4 Transform(Vector4 v)
        {
            return new Vector4(
                m00 * v.x + m01 * v.y + m02 * v.z + m03 * v.w,
                m10 * v.x + m11 * v.y + m12 * v.z + m13 * v.w,
                m20 * v.x + m21 * v.y + m22 * v.z + m23 * v.w,
                m30 * v.x + m31 * v.y + m32 * v.z + m33 * v.w
            );
        }

        public Vector2 TransformNormal(Vector2 v)
        {
            return new Vector2(
                m00 * v.x + m01 * v.y,
                m10 * v.x + m11 * v.y
            );
        }
        */
        public Vector3 TransformNormal(Vector3 v)
        {
            return new Vector3(
                m00 * v.x + m01 * v.y + m02 * v.z,
                m10 * v.x + m11 * v.y + m12 * v.z,
                m20 * v.x + m21 * v.y + m22 * v.z
            );
        }
        /*
        public Vector4 TransformNormal(Vector4 v)
        {
            return Transform(v);
        }
        */
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            return new Matrix4(
                a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30,
                a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31,
                a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32,
                a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33,

                a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30,
                a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31,
                a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32,
                a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33,

                a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30,
                a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31,
                a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32,
                a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33,

                a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30,
                a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31,
                a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32,
                a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33
            );
        }

        public static Matrix4 Lerp(Matrix4 a, Matrix4 b, float t)
        {
            return new Matrix4(
                Algo.Lerp(a.m00, b.m00, t),
                Algo.Lerp(a.m01, b.m01, t),
                Algo.Lerp(a.m02, b.m02, t),
                Algo.Lerp(a.m03, b.m03, t),

                Algo.Lerp(a.m10, b.m10, t),
                Algo.Lerp(a.m11, b.m11, t),
                Algo.Lerp(a.m12, b.m12, t),
                Algo.Lerp(a.m13, b.m13, t),

                Algo.Lerp(a.m20, b.m20, t),
                Algo.Lerp(a.m21, b.m21, t),
                Algo.Lerp(a.m22, b.m22, t),
                Algo.Lerp(a.m23, b.m23, t),

                Algo.Lerp(a.m30, b.m30, t),
                Algo.Lerp(a.m31, b.m31, t),
                Algo.Lerp(a.m32, b.m32, t),
                Algo.Lerp(a.m33, b.m33, t)
            );
        }
    }
}
