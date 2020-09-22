using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Assets.Util.Math;
using UnityEngine;

namespace Assets.Util
{
    class BlockPos
    {
        public Vector3 Position;

        private static int SIZE_BITS_X = 1 + MathHelper.log2(MathHelper.smallestEncompassingPowerOfTwo(30000000));
  
        private static int SIZE_BITS_Z = SIZE_BITS_X;
  
        private static int SIZE_BITS_Y = 64 - SIZE_BITS_X - SIZE_BITS_Z;
  
        private static long BITS_X = (1L << SIZE_BITS_X) - 1L;
  
        private static long BITS_Y = (1L << SIZE_BITS_Y) - 1L;
  
        private static long BITS_Z = (1L << SIZE_BITS_Z) - 1L;
  
        private static int BIT_SHIFT_Z = SIZE_BITS_Y;
  
        private static int BIT_SHIFT_X = SIZE_BITS_Y + SIZE_BITS_Z;

        public static int unpackLongX(long l) {
            return (int)(l << 64 - BIT_SHIFT_X - SIZE_BITS_X >> 64 - SIZE_BITS_X);
        }
  
        public static int unpackLongY(long l) {
            return (int)(l << 64 - SIZE_BITS_Y >> 64 - SIZE_BITS_Y);
        }
  
        public static int unpackLongZ(long l) {
            return (int)(l << 64 - BIT_SHIFT_Z - SIZE_BITS_Z >> 64 - SIZE_BITS_Z);
        }

        public static BlockPos fromLong(long l) {
            return new BlockPos(unpackLongX(l), unpackLongY(l), unpackLongZ(l));
        }

        public BlockPos(int x, int y, int z)
        {
            Position = new Vector3(x, y, z);
        }

        public BlockPos(double x, double y, double z)
        {
            Position = new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), Convert.ToSingle(z));
        }
    }
}
