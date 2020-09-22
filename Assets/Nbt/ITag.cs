using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Nbt
{
    public interface ITag
    {
        void write(DataOutput paramDataOutput) throws IOException;
  
        String toString();
  
        byte getType();
  
        TagReader<?> getReader();
  
        ITag copy();
            default String asString() {
            return toString();
        }
  
        //default Text toText() {
        //    return toText("", 0);
        //}
  
        //Text toText(String paramString, int paramInt);
    }
}
