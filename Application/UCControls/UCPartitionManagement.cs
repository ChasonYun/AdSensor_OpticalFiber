using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticalFiber
{
    public partial class UCPartitionManagement : UserControl
    {
        SQL_Select sql_Select = new SQL_Select();
        List<struct_PrtName> struct_PrtNames = new List<struct_PrtName>();
        public int device;
        public int channel;
        public UCPartitionManagement()
        {
            InitializeComponent();
            struct_PrtNames = sql_Select.Select_PrtName(1, 1);
            device = 1;
            channel = 1;
            ChanngeBtnColor();
            InitDgv();
        }

        public void InitDgv()
        {
            dgvPrtName.Rows.Clear();
            foreach(struct_PrtName struct_PrtName in struct_PrtNames)
            {
                dgvPrtName.Rows.Add(struct_PrtName.prtNo, struct_PrtName.prtName);
            }
        }

        private void btnDevice_1_Click(object sender, EventArgs e)
        {
            device = 1;
            ChanngeBtnColor();
        }

        private void btnDevice_2_Click(object sender, EventArgs e)
        {
            device = 2;
            ChanngeBtnColor();
        }

        private void btnDevice_3_Click(object sender, EventArgs e)
        {
            device = 3;
            ChanngeBtnColor();
        }

        private void btnDevice_4_Click(object sender, EventArgs e)
        {
            device = 4;
            ChanngeBtnColor();
        }

        private void btnDevice_5_Click(object sender, EventArgs e)
        {
            device = 5;
            ChanngeBtnColor();
        }

        private void btnDevice_6_Click(object sender, EventArgs e)
        {
            device = 6;
            ChanngeBtnColor();
        }

        private void btnDevice_7_Click(object sender, EventArgs e)
        {
            device = 7;
            ChanngeBtnColor();
        }

        private void btnDevice_8_Click(object sender, EventArgs e)
        {
            device = 8;
            ChanngeBtnColor();
        }

        private void btnChannel_1_Click(object sender, EventArgs e)
        {
            channel = 1;
            ChanngeBtnColor();
        }

        private void btnChannel_2_Click(object sender, EventArgs e)
        {
            channel = 2;
            ChanngeBtnColor();
        }

        private void btnChannel_3_Click(object sender, EventArgs e)
        {
            channel = 3;
            ChanngeBtnColor();
        }

        private void btnChannel_4_Click(object sender, EventArgs e)
        {
            channel = 4;
            ChanngeBtnColor();
        }

        private void ChanngeBtnColor()
        {
            foreach(Control control in tlpDevice.Controls)
            {
                control.BackColor = Color.SteelBlue;
            }
            foreach(Control control in tlpChnnel.Controls)
            {
                control.BackColor = Color.SteelBlue;
            }
            tlpDevice.Controls.Find("btnDevice_" + device + "", false)[0].BackColor = Color.Green;
            tlpChnnel.Controls.Find("btnChannel_" + channel + "", false)[0].BackColor = Color.Green;
        }
    }
}
