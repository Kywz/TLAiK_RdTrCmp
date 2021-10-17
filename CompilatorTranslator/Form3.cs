using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranslatorCompilator
{
    public partial class Form3 : Form
    {
        Form1 forma1;
        int i = 0;
        public Form3(Form1 forma1)
        {
            this.forma1 = forma1;
            i = this.forma1.dataGridView1.RowCount;
            i--;
            InitializeComponent();
        }

        //Кнопка для удаления переменних
        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                for(int j = 0; j < forma1.dataGridView1.RowCount - 1; j++)
                {
                    if(textBox1.Text == forma1.dataGridView1.Rows[j].Cells[1].Value.ToString())
                    {
                        forma1.tableInitializationHash.Delete(forma1.dataGridView1.Rows[j].Cells[1].Value.ToString());
                        forma1.dataGridView1.Rows.RemoveAt(j);
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
