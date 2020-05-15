using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integrals
{
    partial class Get_Data : Form
    {
        public Get_Data()
        {
            InitializeComponent();
        }
        public Get_Data(Graph f)
        {
            f.Hide();
            InitializeComponent();
            done = f;
        }
        Graph done;
        Graph gr;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (numA.Value < numB.Value)
            {
                if (listBox1.SelectedIndex == 0)
                {

                    MidpointMethod s = new MidpointMethod((double)numA.Value, (double)numB.Value, (double)numQ.Value);

                    gr = new Graph(this, (double)numA.Value, (double)numB.Value, (double)numQ.Value, s);
                    gr.Show();

                }
                if (listBox1.SelectedIndex == 1)
                {
                    SimpsonsMethod s = new SimpsonsMethod((double)numA.Value, (double)numB.Value, (double)numQ.Value);
                    gr = new Graph(this, (double)numA.Value, (double)numB.Value, (double)numQ.Value, s);
                    gr.Show();
                }
                if (listBox1.SelectedIndex == 2)
                {
                    MonteCarloMethod s = new MonteCarloMethod((double)numA.Value, (double)numB.Value, (double)numQ.Value);
                    gr = new Graph(this, (double)numA.Value, (double)numB.Value, (double)numQ.Value, s);
                    gr.Show();

                }
            }
            else label2.Text = "ErRoR";
        }

        private void Get_Data_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(done!=null) done.Close();
        }
    }
}
