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
    public partial class AgentUserCntrl : UserControl
    {
        public int ID;
        private Color color;
        private Image pictureImg;
        private string type;
        private string name;
        private int salesCount;
        private string phone;
        private int priority;
        private int discount;

        public AgentUserCntrl()
        {
            InitializeComponent();
        }

        private void AgentUserCntrl_Load(object sender, EventArgs e)
        {

        }

        public Image Picture
        {
            get { return pictureImg; }
            set
            {
                pictureImg = value;
                pictureBox1.Image = pictureImg;
            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                labelType.Text = type;
            }
        }
        public string NameAgent
        {
            get { return name; }
            set
            {
                name = value;
                labelName.Text = name;
            }
        }
        public int SalesCount
        {
            get { return salesCount; }
            set
            {
                salesCount = value;
                labelSales.Text = Convert.ToString(salesCount);
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                labelPhone.Text = phone;
            }
        }
        public int Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                labelPriority.Text = Convert.ToString(priority);
            }
        }
        public int Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                labelDiscount.Text = Convert.ToString(discount);
            }
        }
        
    }
}
