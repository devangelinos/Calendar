using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

namespace Calendar
{
    public partial class MainForm : Form
    {
        int _currentYear = DateTime.Today.Year;
        int _currentMonth = DateTime.Today.Month;
        int _currentDay = DateTime.Today.Day;
        int _currentHour = DateTime.Today.Hour;

        string _eventsFile = Application.StartupPath + "\\events.dat";

        List<Event> _events;

        List<Button> _dayButtons;

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(_eventsFile)) LoadEvents();

            LoadDayButtonsList();
            LoadMonthView();
            LoadSwitchYears();
            LoadSwitchMonths();
        }

        private void LoadEvents()
        {
            byte[] bytes = File.ReadAllBytes(_eventsFile);

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(bytes);

            _events = bf.Deserialize(ms) as List<Event>;
        }

        private void LoadSwitchMonths()
        {
            switchMonth.Items.Add("January");
            switchMonth.Items.Add("February");
            switchMonth.Items.Add("March");
            switchMonth.Items.Add("April");
            switchMonth.Items.Add("May");
            switchMonth.Items.Add("June");
            switchMonth.Items.Add("July");
            switchMonth.Items.Add("August");
            switchMonth.Items.Add("September");
            switchMonth.Items.Add("October");
            switchMonth.Items.Add("November");
            switchMonth.Items.Add("December");

            switchMonth.Text = DateTime.Today.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private void LoadSwitchYears()
        {
            int i = 1918;
            int max = 2118;

            do
            {
                switchYear.Items.Add(i++);
            }
            while (i <= max);

            switchYear.Text = _currentYear.ToString();
        }

        private void LoadDayButtonsList()
        {
            _dayButtons = new List<Button>();

            _dayButtons.Add(btnDay1);
            _dayButtons.Add(btnDay2);
            _dayButtons.Add(btnDay3);
            _dayButtons.Add(btnDay4);
            _dayButtons.Add(btnDay5);
            _dayButtons.Add(btnDay6);
            _dayButtons.Add(btnDay7);
            _dayButtons.Add(btnDay8);
            _dayButtons.Add(btnDay9);
            _dayButtons.Add(btnDay10);
            _dayButtons.Add(btnDay11);
            _dayButtons.Add(btnDay12);
            _dayButtons.Add(btnDay13);
            _dayButtons.Add(btnDay14);
            _dayButtons.Add(btnDay15);
            _dayButtons.Add(btnDay16);
            _dayButtons.Add(btnDay17);
            _dayButtons.Add(btnDay18);
            _dayButtons.Add(btnDay19);
            _dayButtons.Add(btnDay20);
            _dayButtons.Add(btnDay21);
            _dayButtons.Add(btnDay22);
            _dayButtons.Add(btnDay23);
            _dayButtons.Add(btnDay24);
            _dayButtons.Add(btnDay25);
            _dayButtons.Add(btnDay26);
            _dayButtons.Add(btnDay27);
            _dayButtons.Add(btnDay28);
            _dayButtons.Add(btnDay29);
            _dayButtons.Add(btnDay30);
            _dayButtons.Add(btnDay31);
        }

        private void LoadMonthView(int year = 0, int month = 0)
        {
            if (year == 0) year = _currentYear;
            if (month == 0) month = _currentMonth;

            DateTime dt = new DateTime(year, month, 1);
            
            foreach (Button x in _dayButtons)
            {
                x.Text = string.Empty;

                while (dt.Month == month)
                {
                    x.Text = dt.ToString("ddd dd", CultureInfo.CreateSpecificCulture("en-US"));
                    x.Tag = dt;

                    if (_events != null)
                    {
                        foreach (Event y in _events)
                        {
                            if (y.DateTime == dt)
                            {
                                x.BackColor = y.Color;
                            }
                        }
                    }

                    dt = dt.AddDays(1);
                    break;
                }

                if (string.IsNullOrEmpty(x.Text)) x.Visible = false;
                else x.Visible = true;
            }
        }

        private void switchMonth_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(switchYear.Text))
            {
                LoadMonthView(Convert.ToInt32(switchYear.Text), switchMonth.SelectedIndex + 1);
            }
        }

        private void switchYear_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(switchYear.Text))
            {
                LoadMonthView(Convert.ToInt32(switchYear.Text), switchMonth.SelectedIndex + 1);
            }
        }

        private void btnDays_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            EventForm f = new EventForm((DateTime)button.Tag);
            f.ShowDialog();

            // ...
        }
    }
}
