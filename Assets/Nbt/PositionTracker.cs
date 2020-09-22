using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Nbt
{
    class PositionTracker
    {
        public long Max;
  
        public long Pos;
  
        public PositionTracker(long l) {
            Max = l;
        }
  
        public void add(long l) {
            Pos += l / 8L;

            if (Pos > Max)
                throw new Exception("Tried to read NBT tag that was too big; tried to allocate: " + Pos + "bytes where max allowed: " + Max); 
        }
    }
}
