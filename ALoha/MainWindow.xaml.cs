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
using Aloha.Helpers;
using Aloha.Core;
using Aloha.Service;

namespace Aloha {
    public partial class MainWindow : Window {
        private System.Windows.Media.Brush defautlBrush;

        public MainWindow() {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            defautlBrush = this.r.Foreground;
            System.Drawing.Icon icon = Properties.Resources.alohaIcon;
            Bitmap bitmap = icon.ToBitmap();
            Icon = bitmap.ToImageSource();
        }

        private State[] Aloha(IAloha aloha) {
            return aloha.State();
        }

        private void Go_Click(object sender, RoutedEventArgs e) {
            try {
                this.listbox.Items.Clear();
                Dictionary<Aloha.Helpers.Type, State[]> states = new Dictionary<Aloha.Helpers.Type, State[]>();
                ValidException validException = new ValidException();
                SolidColorBrush red = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));

                r.Text = r.Text.Replace(" ", string.Empty).Replace(".", ",");
                rg.Text = rg.Text.Replace(" ", string.Empty).Replace(".", ",");
                g.Text = g.Text.Replace(" ", string.Empty).Replace(".", ",");
                l.Text = l.Text.Replace(" ", string.Empty).Replace(".", ",");
                n.Text = n.Text.Replace(" ", string.Empty).Replace(".", ",");

                if (r.Text == string.Empty || 
                    r.Text.Count(symbol => symbol == '.') > 1 ||
                    r.Text.Count(symbol => symbol == ',') > 1
                ) {
                    this.r.Foreground = red;
                    validException.IsValid = false;
                } 
                
                if (rg.Text == string.Empty || 
                    rg.Text.Count(symbol => symbol == '.') > 1 ||
                    rg.Text.Count(symbol => symbol == ',') > 1
                ) {
                    this.rg.Foreground = red;
                    validException.IsValid = false;
                } 
                
                if (n.Text == string.Empty || 
                    n.Text.Count(symbol => symbol == '.') > 1 || 
                    n.Text.Count(symbol => symbol == ',') > 1
                ) {
                    this.n.Foreground = red;
                    validException.IsValid = false;
                } 
                
                if (l.Text == string.Empty || 
                    l.Text.Count(symbol => symbol == '.') > 1 ||
                    l.Text.Count(symbol => symbol == ',') > 1
                ) {
                    this.l.Foreground = red;
                    validException.IsValid = false;
                } 
                
                if (g.Text == string.Empty ||
                    g.Text.Count(symbol => symbol == '.') > 1 || 
                    g.Text.Count(symbol => symbol == ',') > 1
                ) {
                    this.g.Foreground = red;
                    validException.IsValid = false;
                }

                if (!validException.IsValid) {
                    throw validException;
                }

                if (Sync.IsChecked == true || Async.IsChecked == true) {
                    if (Async.IsChecked == true) {
                        states.Add(global::Aloha.Helpers.Type.Async, Aloha(new Asynchronous(
                            int.Parse(n.Text),
                            int.Parse(r.Text),
                            double.Parse(g.Text),
                            int.Parse(l.Text), 
                            double.Parse(rg.Text)
                        )));
                    } if (Sync.IsChecked == true) {
                        states.Add(global::Aloha.Helpers.Type.Sync, Aloha(new Synchronous(
                            int.Parse(n.Text),
                            int.Parse(r.Text),
                            double.Parse(g.Text),
                            int.Parse(l.Text),
                            double.Parse(rg.Text)
                        )));
                    }
                } else {
                    throw new Exception("Выбирите хотя бы один метод Алоха!");
                }

                foreach (Aloha.Helpers.Type number in states.Keys) {
                    switch (number) {
                        case global::Aloha.Helpers.Type.Async:
                            this.listbox.Items.Add("------Асинхронная------");
                            break;
                        case global::Aloha.Helpers.Type.Sync:
                            this.listbox.Items.Add("------Синхронная------");
                            break;
                    }

                    foreach (State state in states[number]) {
                        this.listbox.Items.Add(state.ToString());
                    }

                    this.listbox.Items.Add("/*********************************************/");
                }

                this.listbox.Items.RemoveAt(listbox.Items.Count - 1);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            using (AboutAs aboutAs = new AboutAs()) {
                aboutAs.ShowDialog();
            }
        }

        private void rg_KeyDown(object sender, KeyEventArgs e) {
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

            rg.Foreground = defautlBrush;
        }

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

            n.Foreground = defautlBrush;
        }
    }
}
