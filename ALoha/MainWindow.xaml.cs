using Aloha.Helpers;
using Aloha.Core;
using Aloha.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Deployment.Application;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;

namespace Aloha {
    public partial class MainWindow : Window {
        private System.Windows.Media.Brush defautlBrush;
        private SolidColorBrush red;
        private Dictionary<Helpers.Type, State[]> states;

        public MainWindow() {
            InitializeComponent();
            Init();
        }

        private void Init() {
            this.ResizeMode = ResizeMode.NoResize;
            defautlBrush = this.r.Foreground;
            Icon icon = Properties.Resources.alohaIcon;
            Bitmap bitmap = icon.ToBitmap();
            Icon = bitmap.ToImageSource();
            red = System.Windows.Media.Brushes.Red;
            states = null;
            Report.IsEnabled = false;
        }

        private State[] AlohaState(IAloha aloha) {
            return aloha.State;
        }

        private void Go_Click(object sender, RoutedEventArgs e) {
            try {
                GC.Collect();
                states = states ?? new Dictionary<Helpers.Type, State[]>();
                ValidException validException = CheckValidation();
                Graphic.IsEnabled = validException.IsValid;
                bool isAsync = Async.IsChecked == true;
                bool isSync = Sync.IsChecked == true;

                this.listbox.Items.Clear();
                states?.Clear();
                
                r.Text = r.Text.Replace(" ", string.Empty).Replace(".", ",");
                g.Text = g.Text.Replace(" ", string.Empty).Replace(".", ",");
                l.Text = l.Text.Replace(" ", string.Empty).Replace(".", ",");
                n.Text = n.Text.Replace(" ", string.Empty).Replace(".", ",");


                if (!validException.IsValid) {
                    throw validException;
                }

                if (isSync || isAsync) {
                    if (isAsync) {
                        states.Add(Helpers.Type.Async, AlohaState(new Asynchronous(
                            int.Parse(n.Text),
                            int.Parse(r.Text),
                            double.Parse(g.Text),
                            int.Parse(l.Text)
                        )));
                    } if (isSync) {
                        states.Add(Helpers.Type.Sync, AlohaState(new Synchronous(
                            int.Parse(n.Text),
                            int.Parse(r.Text),
                            double.Parse(g.Text),
                            int.Parse(l.Text)
                        )));
                    }
                } else {
                    throw new Exception("Выбирите хотя бы один метод Алоха!");
                }

                foreach (Helpers.Type number in states.Keys) {
                    switch (number) {
                        case Helpers.Type.Async:
                            this.listbox.Items.Add("------Асинхронная------");
                            break;
                        case Helpers.Type.Sync:
                            this.listbox.Items.Add("------Синхронная------");
                            break;
                    }

                    foreach (State state in states[number]) {
                        this.listbox.Items.Add(state.ToString());
                    }

                    this.listbox.Items.Add("/*******************************************************************/");
                }

                Report.IsEnabled = states.Count == 2;
                this.listbox.Items.RemoveAt(listbox.Items.Count - 1);
                GC.Collect();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsValid(TextBox textBox) {
            if (textBox.Text == string.Empty ||
                textBox.Text.Count(symbol => symbol == '.') > 1 ||
                textBox.Text.Count(symbol => symbol == ',') > 1
            ) {
                textBox.Foreground = red;
                return false;
            }
            return true;
        }

        private ValidException CheckValidation() {
            ValidException validException = new ValidException(true, "Неверные данные");

            validException.IsValid = IsValid(r);
            validException.IsValid = IsValid(n);
            validException.IsValid = IsValid(l);
            validException.IsValid = IsValid(g);

            return validException;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            using (AboutAs aboutAs = new AboutAs()) {
                aboutAs.ShowDialog();
            }
        }

        #region TextBoxs
        private void r_KeyDown(object sender, KeyEventArgs e) {
            bool isD = e.Key >= Key.D0 && e.Key <= Key.D1;
            bool isNumpad = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;
            bool isBackSpace = e.Key == Key.Back;
            bool isDelete = e.Key == Key.Delete;

            if (!isD && !isNumpad && !isDelete && !isBackSpace) {
                e.Handled = true;
                return;
            }

            r.Foreground = defautlBrush;
        }

        private void l_KeyDown(object sender, KeyEventArgs e) {
            bool isD = e.Key >= Key.D0 && e.Key <= Key.D1;
            bool isNumpad = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;
            bool isBackSpace = e.Key == Key.Back;
            bool isDelete = e.Key == Key.Delete;

            if (!isD && !isNumpad && !isDelete && !isBackSpace) {
                e.Handled = true;
                return;
            }

            l.Foreground = defautlBrush;
        }

        private void n_KeyDown(object sender, KeyEventArgs e) {
            bool isD = e.Key >= Key.D0 && e.Key <= Key.D1;
            bool isNumpad = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;
            bool isBackSpace = e.Key == Key.Back;
            bool isDelete = e.Key == Key.Delete;

            if (!isD && !isNumpad && !isDelete && !isBackSpace) {
                e.Handled = true;
                return;
            }

            n.Foreground = defautlBrush;
        }

        private void g_KeyDown(object sender, KeyEventArgs e) {
            bool isD = e.Key >= Key.D0 && e.Key <= Key.D1;
            bool isNumpad = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;
            bool isBackSpace = e.Key == Key.Back;
            bool isDelete = e.Key == Key.Delete;
            bool isSeparator = e.Key == Key.OemComma || e.Key == Key.Decimal ||
                                    e.Key == Key.OemQuestion || e.Key == Key.OemPeriod;

            if (!isD && !isNumpad && !isDelete && !isBackSpace && !isSeparator) {
                e.Handled = true;
                return;
            }

            g.Foreground = defautlBrush;
        }
        #endregion

        private void Report_Click(object sender, RoutedEventArgs e) {
            using (ReportForm report = new ReportForm()) {
                GC.Collect();
                ReportForm.States = states;
                report.ShowDialog();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Graphic_Click(object sender, RoutedEventArgs e) {
            try {
                GC.Collect();
                ValidException validException = CheckValidation();
                
                if (!validException.IsValid) {
                    throw validException;
                }

                r.Text = r.Text.Replace(" ", string.Empty).Replace(".", ",");
                g.Text = g.Text.Replace(" ", string.Empty).Replace(".", ",");
                l.Text = l.Text.Replace(" ", string.Empty).Replace(".", ",");
                n.Text = n.Text.Replace(" ", string.Empty).Replace(".", ",");

                GraphicWindow w = new GraphicWindow();
                w.Alohas = new IAloha[] {
                    new Asynchronous(
                        int.Parse(n.Text),
                        int.Parse(r.Text),
                        double.Parse(g.Text),
                        int.Parse(l.Text)
                    ),
                    new Synchronous(
                        int.Parse(n.Text),
                        int.Parse(r.Text),
                        double.Parse(g.Text),
                        int.Parse(l.Text)
                    ),
                };
                w.ShowDialog();
                
                GC.Collect();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            GC.Collect();
        }
    }
}
