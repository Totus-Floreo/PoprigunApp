using PoprigunApp.ModelEF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoprigunApp
{
    public partial class AddEditForm : Form
    {
        DBModel db = new DBModel();
        public Agent agent;
        public AddEditForm()
        {
            InitializeComponent();
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            if(agent == null)
            {
                this.Text = "Добавить";
            }
            else
            {
                this.Text = "Изменить";
                titleTextBox.Text = agent.Title;
            }
        }
    }
}
