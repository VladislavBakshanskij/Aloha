using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Markup;

namespace ALoha {
    public struct State {
        private string name;
        private string value;

        public string Name {
            get {
                return name;
            }

            set {
                if (value is string)
                    name = value;
                else
                    throw new FormatException();
            }
        }

        public string Value {
            get {
                return value;
            }

            set {
                if (value is string)
                    this.value = value;
                else
                    throw new FormatException();
            }
        }

        public State(string name, string value) {
            this.name = name;
            this.value = value;
        }

        public override string ToString() {
            return name + ": " + value;
        }
    }
}
