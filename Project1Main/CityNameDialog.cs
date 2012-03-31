using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project1Main
{
    public partial class CityNameDialog : Form
    {
        private List<Vertex> vertices;
        public CityNameDialog(List<Vertex> vertices)
        {
            InitializeComponent();
            this.vertices = vertices;
        }

        private string cityName = "";
        public string CityName { get { return cityName; } }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (CityNameFound())
            {
                MessageBox.Show("A city with this name already exists, please choose a different name");
                return;
            }

            cityName = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CityNameFound())
                {
                    MessageBox.Show("A city with this name already exists, please choose a different name");
                    return;
                }
                cityName = textBox1.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool CityNameFound()
        {
            return vertices.Find(v => v.Name == textBox1.Text) != null;
        }
    }
}
