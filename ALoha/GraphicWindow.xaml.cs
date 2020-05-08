using Aloha.Core;
using Aloha.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Aloha {
    public partial class GraphicWindow : Window {
        private readonly double minX;
        private readonly double maxX;
        private readonly double cx;
        private readonly double cy;
        private readonly double distanceX;
        private readonly double distanceY;
        private readonly double graphWidth;
        private readonly double graphHeight;
        private readonly Brush defautlBrush;
        private readonly SolidColorBrush red;
        private readonly int countKillChildren;

        private double dx;
        private bool isSync;
        private bool isAsync;
        private IAloha[] alohas;

        public IAloha[] Alohas {
            get {
                return alohas;
            }

            set {
                if (value is IAloha[])
                    alohas = value;
                else
                    throw new FormatException();
            }
        }

        public GraphicWindow() {
            InitializeComponent();
            dx = 0.5;

            minX = 0.0;
            maxX = 4.5;

            graphHeight = this.Height - 100;
            graphWidth = this.Width / 2;

            cx = 30;
            cy = graphHeight - 5;

            distanceX = graphWidth / 3.0;
            distanceY = graphHeight / 1.5;

            defautlBrush = Dx.Foreground;
            red = Brushes.Red;

            countKillChildren = grid1.Children.Count;
           
            this.x.Content = $"Позиция по x: 0.0";
            this.y.Content = $"Позиция по y: 0.0";

            DrawAxis();
        }

        private void DrawAxis() {
            Polyline axis = new Polyline() {
                Points = new PointCollection() {
                    new Point(cx, graphHeight - cy),
                    new Point(cx, cy),
                    new Point(cx + graphWidth + 250, cy)
                },
                Stroke = Brushes.Black
            };
            grid1.Children.Add(axis);

            Polyline vertArr = new Polyline {
                Points = new PointCollection {
                    new Point(cx - 10, graphHeight - cy + 15),
                    new Point(cx, graphHeight - cy),
                    new Point(cx + 10, graphHeight - cy + 15)
                },
                Stroke = Brushes.Black
            };
            grid1.Children.Add(vertArr);

            Polyline horArr = new Polyline {
                Points = new PointCollection {
                    new Point(cx + graphWidth + 250 - 15, cy - 10),
                    new Point(cx + graphWidth + 250, cy),
                    new Point(cx + graphWidth + 250 - 15, cy + 10)
                },
                Stroke = Brushes.Black
            };
            grid1.Children.Add(horArr);

            for (byte i = 1; i < 33; i++) {
                Line line = new Line {
                    X1 = cx - 5,
                    X2 = cx + 5,
                    Y1 = cy - i * 10,
                    Y2 = cy - i * 10,
                    Stroke = Brushes.Black,
                    Opacity = 0.5
                };
                grid1.Children.Add(line);
            }

            for (byte i = 1; i < 64; i++) {
                Line line = new Line {
                    X1 = cx + i * 10,
                    X2 = cx + i * 10,
                    Y1 = cy - 5,
                    Y2 = cy + 5,
                    Stroke = Brushes.Black,
                    Opacity = 0.5
                };
                grid1.Children.Add(line);
            }
        }

        private void Draw_Click(object sender, RoutedEventArgs e) {
            try {
                ValidException validException = CheckValidation();
                
                isAsync = async.IsChecked == true;
                isSync = sync.IsChecked == true;
                
                Dx.Text = Dx.Text.Replace(" ", string.Empty).Replace(".", ",");

                if (!validException.IsValid) {
                    throw validException;
                }
                dx = double.Parse(Dx.Text);

                Draw(dx);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Draw(double dx) {
            try {
                grid1.Children.RemoveRange(countKillChildren, grid1.Children.Count - 1);
                DrawAxis();

                if (isSync || isAsync) { 
                    if (isAsync) 
                        Draw(dx, maxX, Brushes.Red, alohas[0].S);
                    if (isSync) 
                        Draw(dx, maxX, Brushes.Green, alohas[1].S);
                } else {
                    throw new Exception("Выбирите метод для графика!");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private ValidException CheckValidation() {
            ValidException validException = new ValidException(true, "Неверные данные");
            validException.IsValid = IsValid(Dx);
            return validException;
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

        private void Draw(double dx, double maxX, SolidColorBrush stroke, Function function) {
            double x = minX;
            double px = x;
            double py = function(x);

            while (x <= maxX - dx) {
                x += dx;
                double y = function(x);

                Line line = new Line {
                    X1 = px * distanceX + cx,
                    Y1 = -py * distanceY + cy,
                    X2 = x * distanceX + cx,
                    Y2 = -y * distanceY + cy,
                    Stroke = stroke,
                    StrokeThickness = 3  
                };

                grid1.Children.Add(line);

                if (Round(y, 4) <= 0.0)
                    return;
                px = x;
                py = y;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            grid1.Children.Clear();
            this.Close();
        }

        private void Dx_KeyDown(object sender, KeyEventArgs e) {
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

            Dx.Foreground = defautlBrush;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e) {
            Point position = e.GetPosition(this);

            if (position.X > cx + graphWidth + 250 || position.Y > cy) {
                e.Handled = true;
                return;
            }

            double x = (position.X - cx) / distanceX;
            double y = (-position.Y + cy) / distanceY;

            if (x < 0 || y < 0) {
                e.Handled = true;
                return;
            }

            this.x.Content = $"Позиция по x: {Round(x)}";
            this.y.Content = $"Позиция по y: {Round(y)}";
        }

        private double Round(double value, int countDigitAfterPoint = 5) {
            double ten = 10 << countDigitAfterPoint;
            return ((int)(value * ten)) / ten;
        }
    }
}
