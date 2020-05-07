using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Markup;

namespace Aloha.Helpers {
    public class State {
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

        public static bool operator >(State a, State b) {
            double aValue = Convert.ToDouble(a.value);
            double bValue = Convert.ToDouble(b.value);
            return aValue > bValue;
        }

        public static bool operator <(State a, State b) {
            double aValue = Convert.ToDouble(a.value);
            double bValue = Convert.ToDouble(b.value);
            return aValue > bValue;
        }

        public static bool operator ==(State a, State b) {
            double aValue = Convert.ToDouble(a.value);
            double bValue = Convert.ToDouble(b.value);
            return aValue == bValue;
        }

        public static bool operator !=(State a, State b) {
            double aValue = Convert.ToDouble(a.value);
            double bValue = Convert.ToDouble(b.value);
            return aValue == bValue;
        }

        public override string ToString() {
            return name + ": " + value;
        }

        public override bool Equals(object obj) {
            return value.Equals(obj);
        }

        public override int GetHashCode() {
            int hashCode = 1753385216;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(value);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
