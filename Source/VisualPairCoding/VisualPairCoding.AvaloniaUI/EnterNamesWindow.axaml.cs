using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using VisualPairCoding.Infrastructure;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        public void CloseWindow(object? sender, RoutedEventArgs args)
        {
            Close();
        }

        public async void StartForm(object? sender, RoutedEventArgs args)
        {

            var participants = GetParticipants();
            Console.WriteLine(participants.ToArray());

        }

        public void LoadSessionIntoGui(string fileName)
        {
            try
            {
                var session = SessionConfigurationFileHandler.Load(fileName);

                LoadSessionIntoGui(session);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Loading configuration File: " + ex.Message);
            }
        }

        public void LoadSessionIntoGui(SessionConfiguration session)
        {
            participant1.Focus();

            for (var i = 1; i <= 10; i++)
            {
                var control = this.FindControl<TextBox>("participant" + i); 

                if (control != null)
                {
                    control.Text = "";
                }
            }

            for (var i = 1; i <= session.Participants.Count; i++)
            {
                var control = this.FindControl<TextBox>("participant" + i);

                if (control != null)
                {
                    control.Text = session.Participants[i - 1].Trim();
                }
            }

            minutesPerTurn.Value = session.SessionLength;
        }

        private void AddParticipantIfAvailable(string name, List<string> participants)
        {
            if (!string.IsNullOrWhiteSpace(name))
                participants.Add(name);
        }

        private List<string> GetParticipants()
        {
            var participants = new List<string>();
            AddParticipantIfAvailable(participant1.Text, participants);
            AddParticipantIfAvailable(participant2.Text, participants);
            AddParticipantIfAvailable(participant3.Text, participants);
            AddParticipantIfAvailable(participant4.Text, participants);
            AddParticipantIfAvailable(participant5.Text, participants);
            AddParticipantIfAvailable(participant6.Text, participants);
            AddParticipantIfAvailable(participant7.Text, participants);
            AddParticipantIfAvailable(participant8.Text, participants);
            AddParticipantIfAvailable(participant9.Text, participants);
            AddParticipantIfAvailable(participant10.Text, participants);

            return participants;
        }

        public async void LoadSessionConfiguration(object? sender, RoutedEventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filters = new List<FileDialogFilter>()
            {
                new FileDialogFilter()
                {
                    Name="Json",
                    Extensions=new List<string>()
                    {
                        "json"
                    }
                }
            };
            openFileDialog.Title = "config.json";
            openFileDialog.AllowMultiple = false;

            var result = await openFileDialog.ShowAsync(this);

            if (result?.Length > 0)
            {
                LoadSessionIntoGui(result[0]);
            }

        }
        public async void SaveSessionConfiguration(object? sender, RoutedEventArgs args)
        {
            var participants = GetParticipants();


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters = new List<FileDialogFilter>()
            {
                new FileDialogFilter()
                {
                    Name="Json",
                    Extensions=new List<string>()
                    {
                        "json"
                    }
                }
            };
            saveFileDialog.Title = "config.json";


            var result = await saveFileDialog.ShowAsync(this);

            if (!string.IsNullOrWhiteSpace(result))
            {
                try
                {
                    SessionConfigurationFileHandler.Save(result, new SessionConfiguration(participants, (int)minutesPerTurn.Value));
                    //MessageBox.Show("Session Configuration saved successfully!");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error saving configuration File: " + ex.Message);
                }
      
            }
        }
    }
}
