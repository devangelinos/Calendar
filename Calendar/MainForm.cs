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
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calendar
{
    public partial class MainForm : Form
    {
        internal static Day[] Days = new Day[31];
        //internal static List<Day> CalendarEvents;

        string _eventsFile = Application.StartupPath + "\\Events.Q";

        List<Button> _dayButtons = new List<Button>();

        int _currentYear = DateTime.Today.Year;
        int _currentMonth = DateTime.Today.Month;
        int _currentDay = DateTime.Today.Day;
        int _currentHour = DateTime.Today.Hour;

        private void LoadSwitchHandlers()
        {
            switchYear.SelectedIndexChanged += switchYear_SelectedIndexChanged;
            switchMonth.SelectedIndexChanged += switchMonth_SelectedIndexChanged;
        }

        private void LoadSwitchYear()
        {
            int min = 1918;

            do
            {
                switchYear.Items.Add(min++);
            }
            while (min <= 2118);
        }

        private void LoadSummary()
        {
            panelSumamry.Controls.Clear();

            foreach (Day x in Days)
            {
                if (x.Events == null) continue;
                foreach (Event y in x.Events)
                {
                    if (y.DateTime.Month == _currentMonth && y.DateTime.Year == _currentYear)
                    {
                        Label l = new Label();

                        l.AutoSize = true;
                        l.Font = new Font("Segoe UI Semibold", 14);
                        l.Text = string.Format("{0}: {1}\n({2} - {3})", y.Title, y.Text, y.DateTime.ToLongDateString(), y.DateTime.ToLongTimeString());
                        l.Location = new Point(10, panelSumamry.Controls.Count * 80);

                        panelSumamry.Controls.Add(l);
                    }
                }
            }
        }

        private void LoadSwitchMonth()
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
        }

        private void LoadDayButtonsList()
        {
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

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            LoadCalendarEvents();

            LoadSwitchYear();
            LoadSwitchMonth();

            switchYear.Text = _currentYear.ToString();
            switchMonth.Text = DateTime.Today.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-US"));
            
            LoadDayButtonsList();

            LoadSummary();
            LoadMonthView(_currentYear, _currentMonth);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSwitchHandlers();
        }

        private void LoadMonthView(int year, int month)
        {
            DateTime dt = new DateTime(year, month, 1);

            for (int i = 0; i < Days.Length; i++)
            {
                while (dt.Month == month)
                {
                    Days[i].Caption = dt.ToString("ddd dd", CultureInfo.CreateSpecificCulture("en-US"));

                    _dayButtons[i].Text = Days[i].Caption;

                    if (Days[i].Events == null) Days[i].Events = new List<Event>();
                    
                    if (Days[i].Events.Count > 0)
                    {
                        foreach (Event x in Days[i].Events)
                        {
                            if (x.DateTime.Year == dt.Year && x.DateTime.Month == dt.Month)
                            {
                                _dayButtons[i].BackColor = Color.FromArgb(255, x.R, x.G, x.B);
                            }
                            else
                            {
                                _dayButtons[i].BackColor = Color.FromArgb(255, 20, 20, 20);
                            }
                        }
                    }

                    dt = dt.AddDays(1);
                    break;
                }
            }
        }

        private void switchMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(switchYear.Text))
            {
                LoadMonthView(Convert.ToInt32(switchYear.Text), switchMonth.SelectedIndex + 1);
            }
        }

        private void switchYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(switchYear.Text))
            {
                LoadMonthView(Convert.ToInt32(switchYear.Text), switchMonth.SelectedIndex + 1);
            }
        }

        private void LoadCalendarEvents()
        {
            if (File.Exists(_eventsFile))
            {
                XmlSerializer xmlds = new XmlSerializer(typeof(Day[]));
                FileStream fs = new FileStream(_eventsFile, FileMode.Open);

                Days = xmlds.Deserialize(fs) as Day[];
                fs.Close();
            }
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            Button x = (Button)sender;
            
            DateTime dt = new DateTime(Convert.ToInt32(switchYear.Text), switchMonth.SelectedIndex + 1, Convert.ToInt32(Regex.Replace(x.Text, "[^0-9 _]", "")));

            EventForm f = new EventForm(dt);
            f.ShowDialog();

            if (!string.IsNullOrEmpty(switchYear.Text))
            {
                LoadMonthView(Convert.ToInt32(switchYear.Text), switchMonth.SelectedIndex + 1);
            }

            LoadSummary();
        }
    }
}
