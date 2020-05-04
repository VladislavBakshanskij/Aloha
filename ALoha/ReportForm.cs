using Aloha.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Aloha {
    public partial class ReportForm : Form {
        private static Dictionary<Helpers.Type, State[]> states;

        public static Dictionary<Helpers.Type, State[]> States {
            get {
                return states;
            }

            set {
                if (value is Dictionary<Helpers.Type, State[]>)
                    states = value;
                else
                    throw new FormatException("Неверные формат данных");
            }
        }

        public ReportForm() {
            InitializeComponent();
        }

        private void Init() {
            async.Items.Add("------Асинхронная------");
            sync.Items.Add("------Синхронная------");
            report.Items.Add("-----Сводка------");

            FillListBox(async, states[Helpers.Type.Async]);
            FillListBox(sync, states[Helpers.Type.Sync]);

            int id = 1;

            List<State> diff = new List<State> {
                new State(
                    $"Число коллизий у {AsyncOrSyncDesc(states[Helpers.Type.Async][id], states[Helpers.Type.Sync][id])}",
                    states[Helpers.Type.Async][id] < states[Helpers.Type.Sync][id] ? states[Helpers.Type.Async][id].Value : states[Helpers.Type.Sync][id].Value
                )
            };
            id++;

            diff.Add(new State(
                $"Опытное значение нормированной пропускнной нагрузки (RG) больше у {AsyncOrSyncAsc(states[Helpers.Type.Async][id], states[Helpers.Type.Sync][id])}",
                states[Helpers.Type.Async][id] > states[Helpers.Type.Sync][id] ? states[Helpers.Type.Async][id].Value : states[Helpers.Type.Sync][id].Value
            ));
            id++;

            diff.Add(new State(
                $"Опытное значение производительности (S) больше у {AsyncOrSyncAsc(states[Helpers.Type.Async][id], states[Helpers.Type.Sync][id])}",
                states[Helpers.Type.Async][id] > states[Helpers.Type.Sync][id] ? states[Helpers.Type.Async][id].Value : states[Helpers.Type.Sync][id].Value
            ));
            id++;
            
            diff.Add(new State(
                $"Общее время передачи кадров меньше у {AsyncOrSyncDesc(states[Helpers.Type.Async][id], states[Helpers.Type.Sync][id])}",
                states[Helpers.Type.Async][id] < states[Helpers.Type.Sync][id] ? states[Helpers.Type.Async][id].Value : states[Helpers.Type.Sync][id].Value
            ));
            id++;

            diff.Add(new State(
                $"Время передачи одного кадра меньше у {AsyncOrSyncDesc(states[Helpers.Type.Async][id], states[Helpers.Type.Sync][id])}",
                states[Helpers.Type.Async][id] < states[Helpers.Type.Sync][id] ? states[Helpers.Type.Async][id].Value : states[Helpers.Type.Sync][id].Value
            ));

            report.Items.AddRange(diff.ToArray());
        }

        private string AsyncOrSyncAsc(State async, State sync) => async > sync ? "Асинхронной" : "Синхронной";
        private string AsyncOrSyncDesc(State async, State sync) => async < sync ? "Асинхронной" : "Синхронной";

        private void FillListBox(ListBox listBox, State[] states) {
            foreach (State state in states) {
                listBox.Items.Add(state);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void ReportForm_Shown(object sender, EventArgs e) {
            Init();
        }
    }
}
