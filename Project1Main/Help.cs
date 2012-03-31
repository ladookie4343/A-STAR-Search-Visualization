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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            label1.Text = "There are three tools: 'Add New City', 'Create 1-Way Path' and 'Create 2-Way Path.'\n\n" +
                "At any time you may right click on a city to show a pop up menu that will allow you to delete that city or " +
                "make that city a start city or make it an end city.\n\n" +
                "When no tools are selected, cities can be moved around by clicking " +
                "and dragging them.\n\nWhen the 'Add New City' tool is selected, cities can be added by " +
                "left clicking " +
                "anywhere on the panel. Cities cannot be moved around while this tool is selected.\n\n" +
                "The 'Create 1 - Way Path' and 'Create 2 - Way Path' tools are nearly identical. When either of " +
                "these tools is selected, click on the first city, then click on the second city and a 1-way or " +
                "2-way path will be created depending on which tool is selected. Again, the cities cannot be moved " +
                "while either of these tools are selected.\n\n" +
                "Tip: If you cannot move cities, make sure there are no tools selected. ";
        }
    }
}
