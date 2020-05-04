using Aloha.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aloha {
    public partial class AboutAs : Form {
        private string version = "Версия продукта: ";
        private string year = "Год сборки: ";
        private string author = "Автор: ";
        private string group = "Группа: ";

        public AboutAs() {
            InitializeComponent();
            Init();
        }

        private void Init() {
            this.label1.Text = year + Inforamtion.Copyright;
            this.label2.Text = version + Inforamtion.Version;
            this.label3.Text = author + Inforamtion.Company;
            this.label4.Text = group + "ПИ1822";
            this.Text = "Об авторе";
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
