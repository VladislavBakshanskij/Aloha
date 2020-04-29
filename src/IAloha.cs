using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALoha {
    interface IAloha {
        double RG { get; }
        int R { get; set; }
        int L { get; set; }
        int N { get; set; }
        double G { get; set; }
        double P { get; }
        State[] State();
    }
}
