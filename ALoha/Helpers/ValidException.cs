using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;

namespace Aloha.Helpers {
    public class ValidException : Exception {
        private bool isValid;

        /// <summary>
        /// Проверка валидации
        /// </summary>
        public bool IsValid {
            get {
                return isValid;
            }

            set {
                if (value.GetType() == typeof(bool))
                    isValid = value;
                else
                    throw new FormatException();
            }
        }

        public ValidException() 
            : this(false, "Неверные данные") { 
        
        }

        public ValidException(bool isValid, string message) 
            : base(message) {
            this.isValid = isValid;
        }
    }
}
