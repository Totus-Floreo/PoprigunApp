using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoprigunApp.ModelEF;

namespace PoprigunApp
{
    public partial class MainForm : Form
    {
        public static DBModel db = new DBModel();
        static public List<int> lstSelectedProduct = new List<int>();

        static int nAgentList = 10;
        static int numButtons = 5;

        List<Agent> lstAgent = new List<Agent>();

        List<AgentUserCntrl> lstCntrl = new List<AgentUserCntrl>();
        Button[] btnsList = new Button[5];
        List<string> lstTypes = db.AgentType.Select(a => a.Title).ToList();
        int nPageAll = 0;
        int nPageFirst = 1;
        int nPageCurrent = 1;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lstAgent = db.Agent.ToList();

            for (int i = 0; i < nAgentList; i++)
            {
                lstCntrl.Add(new AgentUserCntrl());
                flowLayoutPanel1.Controls.Add(lstCntrl[i]);
            }

            SortCombo.SelectedIndex = 0;
            
            lstTypes.Insert(0, "Все типы");
            FiltrCombo.DataSource = lstTypes;


            for (int i = 0; i < nAgentList; i++)
            {
                lstCntrl.Add(new AgentUserCntrl());
                flowLayoutPanel1.Controls.Add(lstCntrl[i]);
            }
            {
                btnsList[0] = button1;
                btnsList[1] = button2;
                btnsList[2] = button3;
                btnsList[3] = button4;
                btnsList[4] = button5;
                nPageFirst = nPageCurrent = 1;
            }
            PageUpdate();
        }

        string filtr = "Все типы";
        string sort = "Без сортировки";
        string sortDirection = "возрастание"; 
        string search = "";
        public void SortData()
        {
            lstAgent = db.Agent.ToList();

            if (filtr != "Все типы")
            {
                lstAgent = lstAgent.Where(p => (p.AgentType.Title == filtr)).ToList();
            }

            if (search != "")
            {
                search = search.ToUpper();
                lstAgent = lstAgent
                    .Where(p => (p.Title.ToUpper().Contains(search)) ||
                        (p.Title != null && 
                         p.Title.ToUpper().Contains(search)))
                    .ToList();
            }

            if (sort != "Без сортировки")
            {
                if (sort == "Наименование")
                {
                    if (!DownCheck.Checked)
                        lstAgent = lstAgent.OrderBy(p => p.Title).ToList();
                    else
                        lstAgent = lstAgent.
                            OrderByDescending(p => p.Title).ToList();
                }
                if (sort == "Приоритет")
                {
                    if (!DownCheck.Checked)
                        lstAgent = lstAgent.OrderBy(p => p.Priority).ToList();
                    else
                        lstAgent = lstAgent.
                            OrderByDescending(p => p.Priority).ToList();
                }
            }
 
            nPageCurrent = 1;     
            nPageFirst = 1;         
            nPageAll = lstAgent.Count() / nAgentList;
            if (nPageAll * nAgentList < lstAgent.Count())
                nPageAll++;

            PageUpdate();
        }
        private void SortCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort = SortCombo.Text;
            SortData();
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            search = SearchTxt.Text;
            SortData();
        }

        private void FiltrCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtr = FiltrCombo.Text;
            SortData();
        }

        private void DownCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DownCheck.Checked)
                sortDirection = "убывание";
            else
                sortDirection = "возрастание";
            SortData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEditForm form = new AddEditForm();
            form.agent = null;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                PageUpdate();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            AddEditForm form = new AddEditForm();
            DialogResult dr = form.ShowDialog();
            if(dr == DialogResult.OK)
            {
                SortData();
                PageUpdate();
            }
        }
        private void PageUpdate()
        {
            int nAgentMax = lstAgent.Count();
            int n = (nPageCurrent - 1) * nAgentList;

            foreach (AgentUserCntrl auc in lstCntrl)
            {
                if (n < nAgentMax)
                {
                    auc.ID = lstAgent[n].ID;
                    if (lstSelectedProduct.IndexOf(auc.ID) != -1)
                    {
                        auc.BackColor = Color.LightGray;
                    }
                    else
                    {
                        auc.BackColor = Color.White;
                    }
                    auc.NameAgent = lstAgent[n].Title;
                    auc.Type = lstTypes[lstAgent[n].AgentTypeID - 1];
                    int salesCount = 0;
                    auc.SalesCount = salesCount;

                    auc.Phone = lstAgent[n].Phone;
                    auc.Priority = lstAgent[n].Priority;
                    auc.Discount = 5;
                    /*if ((lstAgent[n].Logo != "") && (lstAgent[n].Logo != null))
                        auc.Picture = Image.FromFile(lstAgent[n].Logo);
                    else
                        auc.Picture = Image.FromFile(@"Res\picture.png");*/
                }
                n++;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void buttonN_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            nPageCurrent = Convert.ToInt32(btn.Text);

            PageUpdate();
        }

        private void RightBtn_Click(object sender, EventArgs e)
        {
            if (nPageCurrent < nPageAll)
            {
                nPageCurrent++;
                if (nPageCurrent > (nPageFirst + (numButtons - 1)))
                    nPageFirst++;
            }

            LeftBtn.Enabled = true;

            RightBtn.Enabled = false;
            if (nPageFirst + (numButtons - 1) < nPageAll)
                RightBtn.Enabled = true;

            PageUpdate();
        }

        private void LeftBtn_Click(object sender, EventArgs e)
        {

            if (nPageCurrent > 1)
            {
                nPageCurrent--;
                if (nPageCurrent < nPageFirst)
                    nPageFirst--;
            }
            LeftBtn.Enabled = true;
            if (nPageCurrent == 1 && nPageFirst == 1)
                LeftBtn.Enabled = false;

            RightBtn.Enabled = false;
            if (nPageFirst + (numButtons - 1) < nPageAll)
                RightBtn.Enabled = true;

            PageUpdate();
        }
    }
}
