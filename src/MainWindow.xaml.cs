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
           
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            using (AboutAs aboutAs = new AboutAs()) {
                aboutAs.ShowDialog();
            }
        }
    }
}
