using Aloha.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aloha.Core {
    public interface IAloha {
        int R { get; set; }
        int L { get; set; }
        int N { get; set; }
        double G { get; set; }
        double P { get; }
        double S(double g);
        State[] State { get; }
    }
}
