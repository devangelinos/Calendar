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
        string _eventsFile = Application.StartupPath + "\\Events.Q";

        Day[] _days = new Day[31];
        List<Button> _dayButtons = new List<Button>();

        int _currentYear = DateTime.Today.Year;
        int _currentMonth = DateTime.Today.Month;
        int _currentDay = DateTime.Today.Day;
        int _currentHour = DateTime.Today.Hour;

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

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            LoadDayButtonsList();

            LoadMonthView(_currentYear, _currentMonth);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void LoadMonthView(int year, int month)
        {
            DateTime dt = new DateTime(year, month, 1);

            for (int i = 0; i < _days.Length; i++)
            {
                while (dt.Date.Month == month)
                {
                    _days[i].Caption = dt.ToString("ddd dd", CultureInfo.CreateSpecificCulture("en-US"));

                    _dayButtons[i].Text = _days[i].Caption;
                    if (_days[i].Event != null)
                    {
                        _dayButtons[i].BackColor = _days[i].Event.Color;
                    }

                    dt = dt.AddDays(1);
                    break;
                }
            }
        }

        //private void LoadEvents()
        //{
        //    byte[] bytes = File.ReadAllBytes(_eventsFile);

        //    BinaryFormatter bf = new BinaryFormatter();
        //    MemoryStream ms = new MemoryStream(bytes);

        //    _events = bf.Deserialize(ms) as List<Event>;
        //}
    }
}
