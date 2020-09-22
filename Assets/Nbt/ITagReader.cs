using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Nbt
{
    public interface ITagReader
    {
        object read(Stream paramDataInput, int paramInt, PositionTracker paramPositionTracker) throws IOException;
  
            default boolean isImmutable() {
            return false;
        }
  
        String getCrashReportName();
  
        String getCommandFeedbackName();
  
        static TagReader<EndTag> createInvalid(int i) {
            return new TagReader<EndTag>(i) {
                public EndTag read(DataInput dataInput, int i, PositionTracker arg) throws IOException {
                throw new IllegalArgumentException("Invalid tag id: " + this.field_21047);
            }
        
            public String getCrashReportName() {
                return "INVALID[" + this.field_21047 + "]";
            }
        
            public String getCommandFeedbackName() {
                return "UNKNOWN_" + this.field_21047;
            }
            };
        }
    }
}
