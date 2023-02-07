using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MangerTagGen
{
    public partial class Form1 : Form
    {
        Algo algo = new Algo();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void saveSelected_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
                algo.creatPDF(showNumBox.Checked, false, listBox1.SelectedIndex);
        }

        private void genButton_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void saveAllButton_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count > 0)
                algo.creatPDF(showNumBox.Checked, true);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int x = listBox1.SelectedIndex;
            if (x < 0)
                return;
            algo.updateList(x);
            listBox1.Items.RemoveAt(x);
            if (listBox1.Items.Count == x)
                listBox1.SelectedIndex = x - 1;
            else
                listBox1.SelectedIndex = x;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void submit()
        {
            bool didItPrint = algo.printBarcode(pictureBox1, opNum.Text, password.Text);
            if (didItPrint)
            {
                listBox1.Items.Add("Operator Number: " + opNum.Text + " Password: " + password.Text);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                pictureBox1.Image=null;
            else
                algo.updateBarcode(pictureBox1, listBox1.SelectedIndex);
        }

        private void opNum_TextChanged(object sender, EventArgs e)
        {
            if (algo.IsDigitsOnly(opNum.Text))
                errorProviderPassword.SetError(opNum, String.Empty);
            else
            {
                errorProviderPassword.SetError(opNum, "Only digits are allowed");
                return;
            }

            if (opNum.TextLength > 3)
                errorProviderOpNum.SetError(opNum, "to many digets");
            else errorProviderOpNum.SetError(opNum, String.Empty);
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            if (algo.IsDigitsOnly(password.Text))
                errorProviderPassword.SetError(password, String.Empty);
            else
            {
                errorProviderPassword.SetError(password, "Only digits are allowed");
                return;
            }

            if (password.TextLength > 2)
                errorProviderPassword.SetError(password, "to many digets");
            else errorProviderPassword.SetError(password, String.Empty);
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)

            {
                submit();
            }
        }
    }
}
