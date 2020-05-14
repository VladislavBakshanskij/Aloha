using Aloha.Core;
using Aloha.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Data;

namespace Aloha {
    public class Synchronous : IAloha {
        private int n;
        private int r;
        private double g;
        private int l;

        #region Properties

        /// <summary>
        /// Временной интервал
        /// </summary>
        public int R {
            get => r;
            set {
                if (value.GetType() == typeof(int))
                    r = value;
                else
                    throw new FormatException();
            }
        }

        /// <summary>
        /// Количество пакетов
        /// </summary>
        public int L {
            get => l;
            set {
                if (value.GetType() == typeof(int))
                    l = value;
                else
                    throw new FormatException();
            }
        }

        /// <summary>
        /// Количество процессоров в системе
        /// </summary>
        public int N {
            get {
                return n;
            }

            set {
                if (value.GetType() == typeof(int))
                    n = value;
                else
                    throw new FormatException();
            }
        }

        /// <summary>
        /// Нормированная пропускная нагрузка
        /// </summary>
        public double G {
            get {
                return g;
            }

            set {
                if (value.GetType() == typeof(double))
                    g = value;
                else
                    throw new FormatException();
            }
        }

        /// <summary>
        /// Вероятность прохождения кадра Exp(-G)
        /// </summary>
        public double P {
            get {
                if (g != double.NaN)
                    return Math.Exp(-G);
                else
                    throw new Exception("Пропускная нагрузка не задана!");
            }
        }
        #endregion

        /// <summary>
        /// Опытное значение производительности
        /// </summary>
        /// <param name="g">Нормированная пропуская способность</param>
        /// <returns>g * Exp(-g)</returns>
        public double S(double g) {
            if (g != double.NaN)
                return g * Math.Exp(-g);
            else
                throw new ArgumentException("Пропускная нагрузка не задана!");
        }

        public Synchronous() 
            : this(1, 5, 2, 10) {

        }

        public Synchronous(int n, int r, double g, int l) {
            this.n = n;
            this.r = r;
            this.g = g;
            this.l = l;
        }

        public Synchronous(IAloha aloha) 
            : this(aloha.N, aloha.R, aloha.G, aloha.L) { 
        
        }

        public State[] State {
            get {
                int i = 0;
                int j = 0;
                Random random = new Random();

                for (int k = 0; k < l; k++) {
                    double rp = random.NextDouble();

                    if (rp <= P) {
                        ++j;
                    } else {
                        while (true) {
                            rp = random.NextDouble();
                            ++i;

                            if (rp <= P) {
                                ++j;
                                break;
                            }
                        }
                    }
                }

                dynamic rg = (j + i) * n * l / r / l;

                return new State[] {
                    new State("Количество успешно пройденных пакетов", j.ToString()),
                    new State("Теоритическое значений производительности", (P * G).ToString()),
                    new State("Количество коллизий", i.ToString()),
                    new State("Опытное значение нормированной пропускнной нагрузки (RG)", rg.ToString()),
                    new State("Опытное значение производительности (S)", (rg * Math.Exp(-rg)).ToString()),
                    new State("Общее время передачи кадров", ((j + i) * r).ToString()),
                    new State("Время передачи одного кадра", ((j + i) * r / l).ToString()),
                };
            }
        }
    }
}