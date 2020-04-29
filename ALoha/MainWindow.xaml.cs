﻿using System;
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

namespace ALoha {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private State[] Aloha(IAloha aloha) {
            return aloha.State();
        }

        private void Go_Click(object sender, RoutedEventArgs e) {
            try {
                this.listbox.Items.Clear();
                Dictionary<Number, State[]> states = new Dictionary<Number, State[]>();
                ValidException validException = new ValidException();

                this.r.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));

                if (!validException.IsValid) {
                    return;
                    throw validException;
                }

                if (Sync.IsChecked == true || Async.IsChecked == true) {
                    if (Async.IsChecked == true) {
                        states.Add(Number.Async, Aloha(new Asynchronous()));
                    } if (Sync.IsChecked == true) {
                        states.Add(Number.Sync, Aloha(new Synchronous()));
                    }
                } else {
                    throw new Exception("Выбирите хотя бы один метод Алоха!");
                }

                foreach (Number number in states.Keys) {
                    switch (number) {
                        case Number.Async:
                            this.listbox.Items.Add("------Асинхронная------");
                            break;
                        case Number.Sync:
                            this.listbox.Items.Add("------Синхронная------");
                            break;
                    }

                    foreach (State state in states[number]) {
                        this.listbox.Items.Add(state.ToString());
                    }

                    this.listbox.Items.Add("/************************/");
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
            }
        }

        private void r_KeyDown(object sender, KeyEventArgs e) {
            bool isD = e.Key >= Key.D0 && e.Key <= Key.D1;
            bool isNumpad = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;
            bool isBackSpace = e.Key == Key.Back;
            bool isDelete = e.Key == Key.Delete;

            if (!isD && !isNumpad && !isDelete && !isBackSpace) {
                e.Handled = true;
            }
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
        }
    }
}
