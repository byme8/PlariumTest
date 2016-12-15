using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Data.Core
{
    public class Vector3<TValue> 
    {
        public TValue X;
        public TValue Y;
        public TValue Z;

        public Vector3(TValue x, TValue y, TValue z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    public class Vector2<TValue>
    {
        public TValue X;
        public TValue Y;

        public Vector2(TValue x, TValue y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
