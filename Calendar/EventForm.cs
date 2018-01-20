﻿using System;
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
                    x.R = 255;
                    x.G = 0;
                    x.B = 0;
                    break;
                case "Blue":
                    x.R = 0;
                    x.G = 0;
                    x.B = 240;
                    break;
                case "Yellow":
                    x.R = 255;
                    x.G = 215;
                    x.B = 0;
                    break;
                case "Green":
                    x.R = 50;
                    x.G = 205;
                    x.B = 50;
                    break;
                case "Purple":
                    x.R = 186;
                    x.G = 85;
                    x.B = 211;
                    break;
                case "Pink":
                    x.R = 230;
                    x.G = 230;
                    x.B = 250;
                    break;
                default:
                    x.R = 20;
                    x.G = 20;
                    x.B = 20;
                    break;
            }

            if (MainForm.Days[_eventDateTime.Day - 1].Events == null)
            {
                MainForm.Days[_eventDateTime.Day - 1].Events = new List<Event>();
            }

            MainForm.Days[_eventDateTime.Day - 1].Events.Add(x);

            xmls.Serialize(fs, MainForm.Days);
            fs.Close();
        }

        private void AddEventForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(_eventsFile)) LoadEvents();

            foreach (Day x in MainForm.Days)
            {
                if (x.Events != null)
                {
                    if (x.Events.Count == 0) continue;

                    if (x.Events[_eventDateTime.Day - 1].DateTime == _eventDateTime)
                    {
                        txtTitle.Text = x.Events[_eventDateTime.Day - 1].Title;
                        txtText.Text = x.Events[_eventDateTime.Day - 1].Text;
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
