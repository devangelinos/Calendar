using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Calendar
{
    public partial class EventForm : Form
    {
        string _eventsFile = Application.StartupPath + "\\Events.Q";

        DateTime _eventDateTime;

        public EventForm(DateTime eventTime)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            _eventDateTime = eventTime;
            switchColor.SelectedIndex = 0;

            this.Text = _eventDateTime.ToLongDateString();
        }

        private void LoadEvents()
        {
            XmlSerializer xmlds = new XmlSerializer(typeof(Day[]));
            FileStream fs = new FileStream(_eventsFile, FileMode.Open);

            MainForm.Days = xmlds.Deserialize(fs) as Day[];
            fs.Close();
        }

        private void SaveEvent()
        {
            XmlSerializer xmls = new XmlSerializer(typeof(Day[]));
            FileStream fs = new FileStream(_eventsFile, FileMode.OpenOrCreate);

            Event x = new Event();
            x.Title = txtTitle.Text;
            x.Text = txtText.Text;
            x.DateTime = _eventDateTime;
            x.Sound = string.Empty;
            
            switch (switchColor.Text)
            {
                case "Red":
                    x.Color = Color.Red;
                    break;
                case "Blue":
                    x.Color = Color.Aqua;
                    break;
                case "Yellow":
                    x.Color = Color.Gold;
                    break;
                case "Green":
                    x.Color = Color.LimeGreen;
                    break;
                case "Purple":
                    x.Color = Color.MediumOrchid;
                    break;
                case "Pink":
                    x.Color = Color.Pink;
                    break;
                default:
                    x.Color = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
                    break;
            }

            if (MainForm.Days[_eventDateTime.Day].Events == null)
            {
                MainForm.Days[_eventDateTime.Day].Events = new List<Event>();
            }

            MainForm.Days[_eventDateTime.Day].Events.Add(x);

            xmls.Serialize(fs, MainForm.Days);
            fs.Close();
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(_eventsFile)) LoadEvents();
            else MainForm.CalendarEvents = new List<Day>();

            //foreach (Day x in MainForm.CalendarEvents)
            //{
            //    if (x.Events != null)
            //    {
            //        if (x.Events.Count == 0) continue;

            //        if (x.Events[_eventDateTime.Day].DateTime == _eventDateTime)
            //        {
            //            txtTitle.Text = x.Events[_eventDateTime.Day].Title;
            //            txtText.Text = x.Events[_eventDateTime.Day].Text;
            //            // ...
            //        }
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveEvent();
            this.Close();
        }
    }
}
