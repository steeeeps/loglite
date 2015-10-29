using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loglite
{
    class LogliteLayout:PatternLayout
    {
        public LogliteLayout()
        {
            this.AddConverter("property", typeof(LoglitePatternConverter));

        }
    }
}
