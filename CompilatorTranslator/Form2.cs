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
    public partial class Form2 : Form
    {
        Form1 mainForm;
        int RowCount = 0;

        public Form2(Form1 thisMainForm)
        {
            this.mainForm = thisMainForm;
            RowCount = this.mainForm.dataGridView1.RowCount;
            RowCount--;
            InitializeComponent();
        }

        //Кнопка которая добавляет значения в хеш таблицу и таблицу родительской формы
        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(comboBox1.Text) && nameCheck(textBox1.Text))
            {

                mainForm.dataGridView1.Rows.Add();

                mainForm.tableInitializationHash.Insert(textBox1.Text, textBox1.Text);
                int hex = mainForm.tableInitializationHash.Search(textBox1.Text);
                int numberOfVars = 0;
                if (textBox3.Text != "")
                {
                    numberOfVars++;
                }
                foreach (char c in textBox3.Text)
                {
                    if (c == ',') 
                    {
                        numberOfVars++;
                    }
                }
                mainForm.dataGridView1.Rows[RowCount].Cells[0].Value = hex;
                mainForm.dataGridView1.Rows[RowCount].Cells[1].Value = textBox1.Text;
                mainForm.dataGridView1.Rows[RowCount].Cells[2].Value = numberOfVars;
                mainForm.dataGridView1.Rows[RowCount].Cells[3].Value = comboBox1.Text;
                mainForm.dataGridView1.Rows[RowCount].Cells[4].Value = textBox2.Text;
                mainForm.dataGridView1.Rows[RowCount].Cells[5].Value = comboBox1.Text + " " + textBox1.Text + " (" + textBox3.Text + ")";

                RowCount++;
            }
        }

        //Кнопка которая очищает поля
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        //Функция проверяет налчие таких же имен в таблице и пробелов в названии переменой
        private bool nameCheck(string funcName)
        {
            for(int j = 0; j < mainForm.dataGridView1.RowCount - 1; j++)
            {
                if (mainForm.dataGridView1.Rows[j].Cells[1].Value.ToString() == funcName)
                {
                    MessageBox.Show(
                    "Функция с таким именем уже инициализирована",
                    "ВНИМАНИЕ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1,
                     MessageBoxOptions.DefaultDesktopOnly);
                    return false;
                }
            }
            //Пhоверка на наличие пробелов
            for (int j = 0; j < funcName.Length; j++)
            {
                if(funcName[j] == ' ')
                {
                    MessageBox.Show(
                    "Переменная имеет пробелы",
                    "ВНИМАНИЕ",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                     return false;
                }
            }

            return true;
        }
    }
}
