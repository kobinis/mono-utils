using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoUtils.Utils
{
    public enum ModeType { Debug, Release, Test }
    public class DebugUtils
    {
        public static ModeType Mode = ModeType.Release;
    }
}
