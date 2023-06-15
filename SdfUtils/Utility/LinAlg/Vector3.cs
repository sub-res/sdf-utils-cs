using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdfUtils.Utility.LinAlg
{
    struct Vector3
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;
        public Vector3(float f) { x = f; y = f; z = f; }
        public Vector3(float _x, float _y, float _z) { x = _x; y = _y; z = _z; }

        public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z); }
        public static Vector3 operator -(Vector3 v) { return new Vector3(-v.x, -v.y, -v.z); }
        public static Vector3 operator *(Vector3 a, Vector3 b) { return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z); }
        public static Vector3 operator /(Vector3 a, Vector3 b) { return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z); }
        public static Vector3 operator %(Vector3 a, Vector3 b) { return new Vector3(a.x % b.x, a.y % b.y, a.z % b.z); }

        public static Vector3 operator +(Vector3 v, float f) { return v + new Vector3(f); }
        public static Vector3 operator -(Vector3 v, float f) { return v - new Vector3(f); }
        public static Vector3 operator *(Vector3 v, float f) { return v * new Vector3(f); }
        public static Vector3 operator /(Vector3 v, float f) { return v / new Vector3(f); }
        public static Vector3 operator %(Vector3 v, float f) { return v % new Vector3(f); }

        public static Vector3 operator +(float f, Vector3 v) { return new Vector3(f) + f; }
        public static Vector3 operator -(float f, Vector3 v) { return new Vector3(f) - f; }
        public static Vector3 operator *(float f, Vector3 v) { return new Vector3(f) * f; }
        public static Vector3 operator /(float f, Vector3 v) { return new Vector3(f) / f; }
        public static Vector3 operator %(float f, Vector3 v) { return new Vector3(f) % f; }

        public static Vector3 Normal(Vector3 v) { return v / v.Length(); }
        public static float Dot(Vector3 a, Vector3 b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
        }

        public static Vector3 Abs(Vector3 v) { return new Vector3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z)); }
        public static Vector3 Max(Vector3 a, Vector3 b) { return new Vector3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z)); }
        public static Vector3 Min(Vector3 a, Vector3 b) { return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z)); }
        public static Vector3 Mod(Vector3 a, Vector3 b) { return new Vector3(Algo.Mod(a.x, b.x), Algo.Mod(a.y, b.y), Algo.Mod(a.z, b.z)); }
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t) { return new Vector3(Algo.Lerp(a.x, b.x, t), Algo.Lerp(a.y, b.y, t), Algo.Lerp(a.z, b.z, t)); }

        public float LengthSquared() { return Dot(this, this); }
        public float Length() { return (float)(Math.Sqrt(LengthSquared())); }

        public override string ToString() { return string.Format("({0} {1} {2})", x, y, z); }
    }
}
