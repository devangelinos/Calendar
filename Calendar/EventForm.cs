using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
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
            byte[] bytes = File.ReadAllBytes(_eventsFile);

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(bytes);

            MainForm.CalendarEvents = bf.Deserialize(ms) as List<Day>;
        }

        private void SaveEvent()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

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

            bf.Serialize(ms, MainForm.CalendarEvents);
            byte[] bytes = ms.ToArray();

            File.WriteAllBytes(_eventsFile, bytes);
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(_eventsFile)) LoadEvents();
            else MainForm.CalendarEvents = new List<Day>();

            foreach (Day x in MainForm.CalendarEvents)
            {
                if (x.Events != null)
                {
                    if (x.Events.Count == 0) continue;

                    if (x.Events[_eventDateTime.Day].DateTime == _eventDateTime)
                    {
                        txtTitle.Text = x.Events[_eventDateTime.Day].Title;
                        txtText.Text = x.Events[_eventDateTime.Day].Text;
                        // ...
                    }
                }
            }
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
