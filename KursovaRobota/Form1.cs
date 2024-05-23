using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KursovaRobota
{
    struct Passenger
    {
        public string Name;
        public string Destination;
        public int CarriageNumber;
        public int SeatNumber;
        public int NumberOfItems;
        public double TotalWeight;
    }
    public partial class TLC : Form
    {
        private bool isRowHeadersVisible = false;
        public TLC()
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.ReadOnly = true;
            }
            dataGridView1.RowHeadersVisible = isRowHeadersVisible;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(surnameAndInitialsTextBox.Text))
            {
                MessageBox.Show("Введіть прізвище та ініціали", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(surnameAndInitialsTextBox.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.]*$"))
            {
                MessageBox.Show("Некоректне прізвище та ініціали", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(destinationStationTextBox.Text))
            {
                MessageBox.Show("Введіть станцію призначення", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(destinationStationTextBox.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s]*$"))
            {
                MessageBox.Show("Некоректна станція призначення", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(carriageNumberTextBox.Text))
            {
                MessageBox.Show("Введіть номер вагона", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(carriageNumberTextBox.Text, out int carriageNumber))
            {
                MessageBox.Show("Некоректний номер вагона, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(seatNumberTextBox.Text))
            {
                MessageBox.Show("Введіть номер місця", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(seatNumberTextBox.Text, out int seatNumber))
            {
                MessageBox.Show("Некоректний номер місця, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(numberOfItemsTextBox.Text))
            {
                MessageBox.Show("Введіть кількість речей", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(numberOfItemsTextBox.Text, out int numberOfItems))
            {
                MessageBox.Show("Некоректна кількість речей, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(totalWeightTextBox.Text))
            {
                MessageBox.Show("Введіть загальну вагу речей", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!double.TryParse(totalWeightTextBox.Text, out double totalWeight))
            {
                MessageBox.Show("Некоректна загальна вага речей, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Passenger passenger = new Passenger();
            passenger.Name = surnameAndInitialsTextBox.Text;
            passenger.Destination = destinationStationTextBox.Text;
            passenger.CarriageNumber = int.Parse(carriageNumberTextBox.Text);
            passenger.SeatNumber = int.Parse(seatNumberTextBox.Text);
            passenger.NumberOfItems = int.Parse(numberOfItemsTextBox.Text);
            passenger.TotalWeight = double.Parse(totalWeightTextBox.Text);
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = passenger.Name;// Прізвище та ініціали пасажира
            dataGridView1.Rows[n].Cells[1].Value = passenger.Destination; // станція призначення
            dataGridView1.Rows[n].Cells[2].Value = passenger.CarriageNumber; // № вагона
            dataGridView1.Rows[n].Cells[3].Value = passenger.SeatNumber; // № місця
            dataGridView1.Rows[n].Cells[4].Value = passenger.NumberOfItems; // кількість речей пасажира
            dataGridView1.Rows[n].Cells[5].Value = passenger.TotalWeight; // загальна вага речей
            surnameAndInitialsTextBox.Clear();
            destinationStationTextBox.Clear();
            carriageNumberTextBox.Clear();
            seatNumberTextBox.Clear();
            numberOfItemsTextBox.Clear();
            totalWeightTextBox.Clear();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (isRowHeadersVisible)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    MessageBox.Show("Рядок видалено", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isRowHeadersVisible = !isRowHeadersVisible;
                    dataGridView1.RowHeadersVisible = isRowHeadersVisible;
                }
            }
            else
            {
                MessageBox.Show("Оберіть рядок для видалення", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRowHeadersVisible = !isRowHeadersVisible;
                dataGridView1.RowHeadersVisible = isRowHeadersVisible;
            }

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (isRowHeadersVisible)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int n = dataGridView1.SelectedRows[0].Index;

                    // Перевірка та оновлення прізвища та ініціалів, якщо поле не порожнє
                    if (!string.IsNullOrWhiteSpace(surnameAndInitialsTextBox.Text))
                    {
                        if (!Regex.IsMatch(surnameAndInitialsTextBox.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.]*$"))
                        {
                            MessageBox.Show("Некоректне прізвище та ініціали", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataGridView1.Rows[n].Cells[0].Value = surnameAndInitialsTextBox.Text;
                    }
                    // Перевірка та оновлення станції призначення, якщо поле не порожнє
                    if (!string.IsNullOrWhiteSpace(destinationStationTextBox.Text))
                    {
                        if (!Regex.IsMatch(destinationStationTextBox.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s]*$"))
                        {
                            MessageBox.Show("Некоректна станція призначення", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataGridView1.Rows[n].Cells[1].Value = destinationStationTextBox.Text;
                    }
                    // Перевірка та оновлення номера вагона, якщо поле не порожнє
                    if (!string.IsNullOrWhiteSpace(carriageNumberTextBox.Text))
                    {
                        if (!int.TryParse(carriageNumberTextBox.Text, out int carriageNumber))
                        {
                            MessageBox.Show("Некоректний номер вагона, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataGridView1.Rows[n].Cells[2].Value = carriageNumber;
                    }
                    // Перевірка та оновлення номера місця, якщо поле не порожнє
                    if (!string.IsNullOrWhiteSpace(seatNumberTextBox.Text))
                    {
                        if (!int.TryParse(seatNumberTextBox.Text, out int seatNumber))
                        {
                            MessageBox.Show("Некоректний номер місця, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataGridView1.Rows[n].Cells[3].Value = seatNumber;
                    }

                    // Перевірка та оновлення кількості речей, якщо поле не порожнє
                    if (!string.IsNullOrWhiteSpace(numberOfItemsTextBox.Text))
                    {
                        if (!int.TryParse(numberOfItemsTextBox.Text, out int numberOfItems))
                        {
                            MessageBox.Show("Некоректна кількість речей, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataGridView1.Rows[n].Cells[4].Value = numberOfItems;
                    }

                    // Перевірка та оновлення загальної ваги речей, якщо поле не порожнє
                    if (!string.IsNullOrWhiteSpace(totalWeightTextBox.Text))
                    {
                        if (!double.TryParse(totalWeightTextBox.Text, out double totalWeight))
                        {
                            MessageBox.Show("Некоректна загальна вага речей, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataGridView1.Rows[n].Cells[5].Value = totalWeight;
                    }
                    MessageBox.Show("Рядок редаговано", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isRowHeadersVisible = !isRowHeadersVisible;
                    dataGridView1.RowHeadersVisible = isRowHeadersVisible;
                }
            }
            else
            {
                MessageBox.Show("Оберіть рядок для редагування", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isRowHeadersVisible = !isRowHeadersVisible;
                dataGridView1.RowHeadersVisible = isRowHeadersVisible;
            }
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Зберегти зміни?", "Вихід з програми", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    // Зберігаємо зміни
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt.TableName = "Passenger";
                    dt.Columns.Add("Прізвище та ініціали");
                    dt.Columns.Add("Станція призначення");
                    dt.Columns.Add("№ вагона");
                    dt.Columns.Add("№ місця");
                    dt.Columns.Add("Кількість речей");
                    dt.Columns.Add("Загальна вага речей");
                    ds.Tables.Add(dt);
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        DataRow row = ds.Tables["Passenger"].NewRow();
                        row["Прізвище та ініціали"] = r.Cells[0].Value;
                        row["Станція призначення"] = r.Cells[1].Value;
                        row["№ вагона"] = r.Cells[2].Value;
                        row["№ місця"] = r.Cells[3].Value;
                        row["Кількість речей"] = r.Cells[4].Value;
                        row["Загальна вага речей"] = r.Cells[5].Value;
                        ds.Tables["Passenger"].Rows.Add(row);
                    }
                    ds.WriteXml("F:\\Passengers.xml");
                    Application.Exit();
                    break;
                case DialogResult.No:
                    // Виходимо без збереження змін
                    Application.Exit();
                    break;
                case DialogResult.Cancel:
                    // Відміна виходу
                    break;
            }

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблиця порожня", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Passenger";
                dt.Columns.Add("Прізвище та ініціали");
                dt.Columns.Add("Станція призначення");
                dt.Columns.Add("№ вагона");
                dt.Columns.Add("№ місця");
                dt.Columns.Add("Кількість речей");
                dt.Columns.Add("Загальна вага речей");
                ds.Tables.Add(dt);
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    DataRow row = ds.Tables["Passenger"].NewRow();
                    row["Прізвище та ініціали"] = r.Cells[0].Value;
                    row["Станція призначення"] = r.Cells[1].Value;
                    row["№ вагона"] = r.Cells[2].Value;
                    row["№ місця"] = r.Cells[3].Value;
                    row["Кількість речей"] = r.Cells[4].Value;
                    row["Загальна вага речей"] = r.Cells[5].Value;
                    ds.Tables["Passenger"].Rows.Add(row);
                }
                ds.WriteXml(filePath);
                MessageBox.Show("XML файл успішно збережений", "Виконано!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        List<Passenger> originalPassengers = new List<Passenger>();
        List<Passenger> passengers = new List<Passenger>();
        private void loadButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Очистіть поле перед завантаженням нового файла", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML Files (*.xml)|*.xml";
                openFileDialog.DefaultExt = "xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    if (File.Exists(filePath))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(filePath);
                        if (ds.Tables.Count == 0 || ds.Tables["Passenger"].Rows.Count == 0)
                        {
                            MessageBox.Show("Файл порожній", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        foreach (DataRow item in ds.Tables["Passenger"].Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = item["Прізвище та ініціали"];
                            dataGridView1.Rows[n].Cells[1].Value = item["Станція призначення"];
                            dataGridView1.Rows[n].Cells[2].Value = item["№ вагона"];
                            dataGridView1.Rows[n].Cells[3].Value = item["№ місця"];
                            dataGridView1.Rows[n].Cells[4].Value = item["Кількість речей"];
                            dataGridView1.Rows[n].Cells[5].Value = item["Загальна вага речей"];
                            Passenger passenger = new Passenger
                            {
                                Name = item["Прізвище та ініціали"].ToString(),
                                Destination = item["Станція призначення"].ToString(),
                                CarriageNumber = int.Parse(item["№ вагона"].ToString()),
                                SeatNumber = int.Parse(item["№ місця"].ToString()),
                                NumberOfItems = int.Parse(item["Кількість речей"].ToString()),
                                TotalWeight = double.Parse(item["Загальна вага речей"].ToString())
                            };
                            originalPassengers.Add(passenger);
                        }
                        passengers = new List<Passenger>(originalPassengers);
                        MessageBox.Show("Файл завантажено", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("XML файл не знайдено", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        
       
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
        //видача відомостей про п'ятьох пасажирів потягу (прізвище та ініціали пасажира та місце його розташування) загальна вага речей в багажі яких є найбільшою. Список пасажирів повинно бути відсортовано в алфавітному порядку;

            if (infoRadioButton.Checked)
            {
                // Конвертуємо дані з dataGridView в список
                List<Passenger> passengers = new List<Passenger>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    Passenger passenger = new Passenger();
                    passenger.Name = row.Cells[0].Value.ToString();
                    passenger.Destination = row.Cells[1].Value.ToString();
                    passenger.CarriageNumber = int.Parse(row.Cells[2].Value.ToString());
                    passenger.SeatNumber = int.Parse(row.Cells[3].Value.ToString());
                    passenger.NumberOfItems = int.Parse(row.Cells[4].Value.ToString());
                    passenger.TotalWeight = double.Parse(row.Cells[5].Value.ToString());
                    passengers.Add(passenger);
                }
                originalPassengers = new List<Passenger>(passengers);

                // Використовуємо алгоритм сортування вставками для сортування списку пасажирів за загальною вагою багажу в порядку спадання
                for (int i = 1; i < passengers.Count; i++)
                {
                    Passenger key = passengers[i];
                    int j = i - 1;

                    // Переміщуємо елементи passengers[0..i-1], які є меншими за ключ, на одну позицію вперед
                    while (j >= 0 && passengers[j].TotalWeight < key.TotalWeight)
                    {
                        passengers[j + 1] = passengers[j];
                        j = j - 1;
                    }
                    passengers[j + 1] = key;
                }

                ////Обмежуємо список до п'яти пасажирів з найбільшою загальною вагою багажу
                passengers = passengers.Take(5).ToList();

                //// Використовуємо алгоритм сортування вставками для сортування цих п'яти пасажирів за іменами в алфавітному порядку
                for (int i = 1; i < passengers.Count; i++)
                {
                    Passenger key = passengers[i];
                    int j = i - 1;

                    // Переміщуємо елементи passengers[0..i-1], які є більшими за ключ, на одну позицію вперед
                    while (j >= 0 && passengers[j].Name.CompareTo(key.Name) > 0)
                    {
                        passengers[j + 1] = passengers[j];
                        j = j - 1;
                    }
                    passengers[j + 1] = key;
                }

                // Оновлюємо дані в dataGridView2
                dataGridView2.Rows.Clear();
                foreach (Passenger passenger in passengers)
                {
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = passenger.Name;
                    dataGridView2.Rows[n].Cells[1].Value = passenger.Destination;
                    dataGridView2.Rows[n].Cells[2].Value = passenger.CarriageNumber;
                    dataGridView2.Rows[n].Cells[3].Value = passenger.SeatNumber;
                    dataGridView2.Rows[n].Cells[4].Value = passenger.NumberOfItems;
                    dataGridView2.Rows[n].Cells[5].Value = passenger.TotalWeight;
                }
            }
            //видача відомостей про пасажирів потягу(прізвище та ініціали, місце розташування, кількість речей), багаж яких містить кількість речей більше введеного значення. 
            if (Info2radioButton.Checked)
            {
                // Конвертуємо дані з dataGridView в список
                List<Passenger> passengers = new List<Passenger>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    Passenger passenger = new Passenger();
                    passenger.Name = row.Cells[0].Value.ToString();
                    passenger.Destination = row.Cells[1].Value.ToString();
                    passenger.CarriageNumber = int.Parse(row.Cells[2].Value.ToString());
                    passenger.SeatNumber = int.Parse(row.Cells[3].Value.ToString());
                    passenger.NumberOfItems = int.Parse(row.Cells[4].Value.ToString());
                    passenger.TotalWeight = double.Parse(row.Cells[5].Value.ToString());
                    passengers.Add(passenger);
                }
                originalPassengers = new List<Passenger>(passengers);
                // Використовуємо алгоритм сортування вставками для сортування списку пасажирів за кількістю речей
                for (int i = 1; i < passengers.Count; i++)
                {
                    Passenger key = passengers[i];
                    int j = i - 1;

                    // Переміщуємо елементи passengers[0..i-1], які є меншими за ключ, на одну позицію вперед
                    while (j >= 0 && passengers[j].NumberOfItems < key.NumberOfItems)
                    {
                        passengers[j + 1] = passengers[j];
                        j = j - 1;
                    }
                    passengers[j + 1] = key;
                }

                // Введене значення
                int enteredValue = int.Parse(Individual2.Text); // Припустимо, що textBox1 - це текстове поле, де ви вводите значення

                // Використовуємо алгоритм бінарного пошуку для знаходження пасажирів, кількість речей яких перевищує введене значення
                int left = 0, right = passengers.Count - 1;
                while (left <= right)
                {
                    int mid = (right + left) / 2;
                    if (passengers[mid].NumberOfItems <= enteredValue)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }

                // Обмежуємо список до пасажирів, кількість речей яких перевищує введене значення
                passengers = passengers.Skip(left).ToList();
                // Оновлюємо дані в dataGridView2
                dataGridView2.Rows.Clear();
                foreach (Passenger passenger in passengers)
                {
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = passenger.Name;
                    dataGridView2.Rows[n].Cells[1].Value = passenger.Destination;
                    dataGridView2.Rows[n].Cells[2].Value = passenger.CarriageNumber;
                    dataGridView2.Rows[n].Cells[3].Value = passenger.SeatNumber;
                    dataGridView2.Rows[n].Cells[4].Value = passenger.NumberOfItems;
                    dataGridView2.Rows[n].Cells[5].Value = passenger.TotalWeight;
                    // Якщо кількість речей пасажира менша або дорівнює введеному значенню, приховуємо рядок
                    if (passenger.NumberOfItems < enteredValue)
                    {
                        dataGridView2.Rows[n].Visible = false;
                    }

                }
                //Визначення станції призначення з найбільшою загальною вагою речей.
                if (info3radioButton.Checked)
                {
                  
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        Passenger passenger = new Passenger();
                        passenger.Name = row.Cells[0].Value.ToString();
                        passenger.Destination = row.Cells[1].Value.ToString();
                        passenger.CarriageNumber = int.Parse(row.Cells[2].Value.ToString());
                        passenger.SeatNumber = int.Parse(row.Cells[3].Value.ToString());
                        passenger.NumberOfItems = int.Parse(row.Cells[4].Value.ToString());
                        passenger.TotalWeight = double.Parse(row.Cells[5].Value.ToString());
                        passengers.Add(passenger);
                    }
                    originalPassengers = new List<Passenger>(passengers);
                    var destinations = passengers.GroupBy(p => p.Destination)
                                        .Select(group => new { Destination = group.Key, TotalWeight = group.Sum(p => p.TotalWeight) })
                                        .ToList();

                    // Сортуємо список за загальною вагою за допомогою алгоритму вставок
                    for (int i = 1; i < destinations.Count; i++)
                    {
                        var key = destinations[i];
                        int j = i - 1;

                        while (j >= 0 && destinations[j].TotalWeight > key.TotalWeight)
                        {
                            destinations[j + 1] = destinations[j];
                            j = j - 1;
                        }
                        destinations[j + 1] = key;
                    }

                    // Остання станція в відсортованому списку має найбільшу вагу
                    var destinationWithMaxWeight = destinations.Last();

                    // Оновлюємо дані в dataGridView2
                    dataGridView2.Rows.Clear();
                    foreach (Passenger passenger in passengers)
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = passenger.Name;
                        dataGridView2.Rows[n].Cells[1].Value = passenger.Destination;
                        dataGridView2.Rows[n].Cells[2].Value = passenger.CarriageNumber;
                        dataGridView2.Rows[n].Cells[3].Value = passenger.SeatNumber;
                        dataGridView2.Rows[n].Cells[4].Value = passenger.NumberOfItems;
                        if (passenger.Destination == destinationWithMaxWeight.Destination)
                        {
                            dataGridView2.Rows[n].Cells[5].Value = destinationWithMaxWeight.TotalWeight;
                        }
                        else
                        {
                            dataGridView2.Rows[n].Cells[5].Value = passenger.TotalWeight;
                        }
                    }
                }
            }
        }
    
    private void button1_Click(object sender, EventArgs e)
    {
        if (dataGridView2.Rows.Count > 0)
        {
            MessageBox.Show("Очистіть другу таблицю перед завантаженням нового файла.", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.DefaultExt = "xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (File.Exists(filePath))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(filePath);
                    if (ds.Tables.Count == 0 || ds.Tables["Passenger"].Rows.Count == 0)
                    {
                        MessageBox.Show("Файл порожній.", "Помилка.");
                        return;
                    }
                    foreach (DataRow item in ds.Tables["Passenger"].Rows)
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = item["Прізвище та ініціали"];
                        dataGridView2.Rows[n].Cells[1].Value = item["Станція призначення"];
                        dataGridView2.Rows[n].Cells[2].Value = item["№ вагона"];
                        dataGridView2.Rows[n].Cells[3].Value = item["№ місця"];
                        dataGridView2.Rows[n].Cells[4].Value = item["Кількість речей"];
                        dataGridView2.Rows[n].Cells[5].Value = item["Загальна вага речей"];
                    }
                    MessageBox.Show("Файл завантажено в другу таблицю. Виконано", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("XML файл не знайдено.", "Помилка.");
                }
            }
        }
    }
    

        private void clear2button_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблиця порожня", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            foreach (Passenger passenger in originalPassengers)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = passenger.Name;
                dataGridView2.Rows[n].Cells[1].Value = passenger.Destination;
                dataGridView2.Rows[n].Cells[2].Value = passenger.CarriageNumber;
                dataGridView2.Rows[n].Cells[3].Value = passenger.SeatNumber;
                dataGridView2.Rows[n].Cells[4].Value = passenger.NumberOfItems;
                dataGridView2.Rows[n].Cells[5].Value = passenger.TotalWeight;
            }
        }
   
    
        private void Info2radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Individual2.Visible = Info2radioButton.Checked;
        }
    }
}


  
       
