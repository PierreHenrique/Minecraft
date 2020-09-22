using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Util.Math
{
    class ChunkPos
    {
        public Vector2 Position;

        public ChunkPos(int x, int y)
        {
            Position = new Vector2(x, y);
        }

        public ChunkPos(long l)
        {
            Position = new Vector2((int)l, l >> 32);
        }
    }
}
