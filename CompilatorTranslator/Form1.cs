using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Data.OleDb;
//using System.Drawing;
//using System.IO;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranslatorCompilator
{
    public partial class Form1 : Form
    {
        // место для переменных 
        //string path = @"C:\Users\Asus\Desktop\AnalizLecksicheskiq\inichializate.txt";
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Kywz/Desktop/5 Семестр/Теорія синтакс. аналізу і компіляц/RdTrCmp/CompilatorTranslator/exampleFuncDataset.accdb;";
        bool import_fromDataBase = true;
        public HashTable tableInitializationHash;
        // поле - ссылка на экземпляр класса OleDbConnection для соединения с БД
        private OleDbConnection myConnection;
        Scanner scannerForCode = new Scanner();
        public Form1()
        {
            InitializeComponent();

            //Загрузка значерий с базы данных и занесения в хеш таблицу
            tableInitializationHash = new HashTable();

            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();
        }


        // кнопка для импортирования переменных с базы данных
        private void button1_Click(object sender, EventArgs e)
        {
           if(import_fromDataBase)
            {
                //import_fromDataBase = false;
                string query = "SELECT [hash], [funcName], [varNumb], [returnType], [funcCallsNum], [initialization] FROM examplaryDataSet;";

                // создаем объект OleDbCommand для выполнения запроса к БД MS Access
                OleDbCommand command = new OleDbCommand(query, myConnection);

                // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
                OleDbDataReader reader;

                reader = command.ExecuteReader();

                // очищаем listBox1

                // в цикле построчно читаем ответ от БД
                int i = dataGridView1.RowCount - 1;
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    // Вводим значения в хеш таблицу после этого заполняем dataGirdView
                    tableInitializationHash.Insert(reader[1].ToString(), reader[1].ToString());
                    int hash = tableInitializationHash.Search(reader[1].ToString());
                    dataGridView1.Rows[i].Cells[0].Value = hash;
                    Console.WriteLine(hash);
                    dataGridView1.Rows[i].Cells[1].Value = reader[1].ToString();
                    Console.WriteLine(" " + reader[1].ToString());
                    dataGridView1.Rows[i].Cells[2].Value = reader[2].ToString();
                    Console.WriteLine(" " + reader[2].ToString());
                    dataGridView1.Rows[i].Cells[3].Value = reader[3].ToString();
                    Console.WriteLine(" " + reader[3].ToString());
                    dataGridView1.Rows[i].Cells[4].Value = reader[4].ToString();
                    Console.WriteLine(" " + reader[4].ToString() + "/n");
                    dataGridView1.Rows[i].Cells[5].Value = reader[5].ToString();

                    i++;
                }
                // закрываем OleDbDataReader
                reader.Close();
            }
        }

        //вызов формы для добавления
        private void button2_Click_addObjectToTable(object sender, EventArgs e)
        {
            Form2 dbImport = new Form2(this);
            dbImport.ShowDialog();
        }

        //при закрытии формы переводим все в базу данных
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //сохранение БД на закрытие формы
           /* if(dataGridView1.RowCount - 1 != 0 && import_fromDataBase == false)
            {
                //Очищаем базу данных перед записью
                string query = "DELETE * FROM examplaryDataSet";

                // создаем объект OleDbCommand для выполнения запроса к БД MS Access
                OleDbCommand command = new OleDbCommand(query, myConnection);

                // выполняем запрос к MS Access
                command.ExecuteNonQuery();
                //Заносим новые значения в таблицу

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    query = "INSERT INTO examplaryDataSet ([hash], [funcName], [varNumb], [returnType], [funcCallsNum], [initialization]) VALUES (" + dataGridView1.Rows[i].Cells[0].Value + ", '" + dataGridView1.Rows[i].Cells[1].Value + "', " + dataGridView1.Rows[i].Cells[2].Value + ", '" + dataGridView1.Rows[i].Cells[3].Value + "', " + dataGridView1.Rows[i].Cells[4].Value + ", '" + dataGridView1.Rows[i].Cells[5].Value + "')";
                    // создаем объект OleDbCommand для выполнения запроса к БД MS Access
                    command = new OleDbCommand(query, myConnection);

                    // выполняем запрос к MS Access
                    command.ExecuteNonQuery();
                }
            }

           */ 
            
            myConnection.Close();
        }
           
        //Кнопка для удаления строк с таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.ShowDialog();
        }

        private void button5_Click_ClearTextBox(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void button4_Click_scanner(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            for (int lineCounter = 0; lineCounter < richTextBox1.Lines.Count(); lineCounter++)
            {
                richTextBox2.AppendText(scannerForCode.scannerMainAlgorith(richTextBox1.Lines[lineCounter]) + "\n");//scannerForCode.scannerMainAlgorith(richTextBox1.Lines[lineCounter]); //Lines[richTextBox2.Lines.Count()]
            }

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

        }

        private void checkBox1_CheckedChanged_Debug(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button1.Enabled = true;
                button1.Visible = true;
                button2.Enabled = true;
                button2.Visible = true;
                button3.Enabled = true;
                button3.Visible = true;
            }
            else
            {
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = false;
                button2.Visible = false;
                button3.Enabled = false;
                button3.Visible = false;
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>
    /// Элемент данных хеш таблицы.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Хранимые данные.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Создать новый экземпляр хранимых данных Item.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="value"> Значение. </param>
        public Item(string key, string value)
        {
            // Проверяем входные данные на корректность.
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            // Устанавливаем значения.
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Ключ объекта. </returns>
        public override string ToString()
        {
            return Key;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Хеш-таблица.
    /// </summary>
    /// <remarks>
    /// Используется метод цепочек (открытое хеширование).
    /// </remarks>
    public class HashTable
    {
        /// <summary>
        /// Максимальная длина ключевого поля.
        /// </summary>
        //private readonly byte _maxSize = 255;

        /// <summary>
        /// Коллекция хранимых данных.
        /// </summary>
        /// <remarks>
        /// Представляет собой словарь, ключ которого представляет собой хеш ключа хранимых данных,
        /// а значение это список элементов с одинаковым хешем ключа.
        /// </remarks>
        private Dictionary<int, List<Item>> _items = null;

        /// <summary>
        /// Коллекция хранимых данных в хеш-таблице в виде пар Хеш-Значения.
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<int, List<Item>>> Items => _items?.ToList()?.AsReadOnly();

        /// <summary>
        /// Создать новый экземпляр класса HashTable.
        /// </summary>
        public HashTable()
        {
            // Инициализируем коллекцию максимальным количество элементов.
            _items = new Dictionary<int, List<Item>>();
        }

        /// <summary>
        /// Добавить данные в хеш таблицу.
        /// </summary>
        /// <param name="key"> Ключ хранимых данных. </param>
        /// <param name="value"> Хранимые данные. </param>
        public void Insert(string key, string value)
        {
            // Проверяем входные данные на корректность.
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            //if (key.Length > _maxSize)
            //{
            //    throw new ArgumentException($"Максимальная длинна ключа составляет {_maxSize} символов.", nameof(key));
            //}

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            // Создаем новый экземпляр данных.
            var item = new Item(key, value);

            // Получаем хеш ключа
            var hash = GetHash(item.Key);

            // Получаем коллекцию элементов с таким же хешем ключа.
            // Если коллекция не пустая, значит заначения с таким хешем уже существуют,
            // следовательно добавляем элемент в существующую коллекцию.
            // Иначе коллекция пустая, значит значений с таким хешем ключа ранее не было,
            // следовательно создаем новую пустую коллекцию и добавляем данные.
            List<Item> hashTableItem = null;
            if (_items.ContainsKey(hash))
            {
                // Получаем элемент хеш таблицы.
                hashTableItem = _items[hash];

                // Проверяем наличие внутри коллекции значения с полученным ключом.
                // Если такой элемент найден, то сообщаем об ошибке.
                var oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (oldElementWithKey != null)
                {
                    return;
                    //throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                // Добавляем элемент данных в коллекцию элементов хеш таблицы.
                _items[hash].Add(item);
            }
            else
            {
                // Создаем новую коллекцию.
                hashTableItem = new List<Item> { item };

                // Добавляем данные в таблицу.
                _items.Add(hash, hashTableItem);
            }
        }

        /// <summary>
        /// Удалить данные из хеш таблицы по ключу.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        public void Delete(string key)
        {
            // Проверяем входные данные на корректность.
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            //if (key.Length > _maxSize)
            //{
            //    throw new ArgumentException($"Максимальная длинна ключа составляет {_maxSize} символов.", nameof(key));
            //}

            // Получаем хеш ключа.
            var hash = GetHash(key);

            // Если значения с таким хешем нет в таблице, 
            // то завершаем выполнение метода.
            if (!_items.ContainsKey(hash))
            {
                return;
            }

            // Получаем коллекцию элементов по хешу ключа.
            var hashTableItem = _items[hash];

            // Получаем элемент коллекции по ключу.
            var item = hashTableItem.SingleOrDefault(i => i.Key == key);

            // Если элемент коллекции найден, 
            // то удаляем его из коллекции.
            if (item != null)
            {
                hashTableItem.Remove(item);
            }
        }

        /// <summary>
        /// Поиск значения по ключу.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <returns> Найденные по ключу хранимые данные. </returns>
        public int Search(string key)
        {
            // Проверяем входные данные на корректность.
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            //if (key.Length > _maxSize)
            //{
            //    throw new ArgumentException($"Максимальная длинна ключа составляет {_maxSize} символов.", nameof(key));
            //}

            // Получаем хеш ключа.
            int hash = GetHash(key);

            // Если таблица не содержит такого хеша,
            // то завершаем выполнения метода вернув null.
            if (!_items.ContainsKey(hash))
            {
                return 0;
            }

            // Если хеш найден, то ищем значение в коллекции по ключу.
            var hashTableItem = _items[hash];

            // Если хеш найден, то ищем значение в коллекции по ключу.
            if (hashTableItem != null)
            {
                // Получаем элемент коллекции по ключу.
                var item = hashTableItem.SingleOrDefault(i => i.Key == key);

                // Если элемент коллекции найден, 
                // то возвращаем хранимые данные.
                if (item != null)
                {
                    return hash;
                }
            }

            // Возвращаем null если ничего найдено.
            return 0;
        }

        /// <summary>
        /// Хеш функция.
        /// </summary>
        /// <remarks>
        /// Возвращает длину строки.
        /// </remarks>
        /// <param name="value"> Хешируемая строка. </param>
        /// <returns> Хеш строки. </returns>
        private int GetHash(string value)
        {
            // Проверяем входные данные на корректность.
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            //// Получаем длину строки.
            var hash = 0;
            for (int i = 0; i < value.Length; i++)
                hash += value[i];

            return hash;
        }
    }

}
