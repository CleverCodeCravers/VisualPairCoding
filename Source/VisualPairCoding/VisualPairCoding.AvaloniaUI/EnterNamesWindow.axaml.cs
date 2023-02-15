using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisualPairCoding.BL;
using VisualPairCoding.Infrastructure;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class EnterNamesForm : Window
    {
        private bool _autostart = false;

        public EnterNamesForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public EnterNamesForm(bool autostart)
        {
            _autostart = autostart;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Opened += OnActivated;
        }

        private void OnActivated(object? sender, EventArgs e)
        {

            if (_autostart)
            {
                RoutedEventArgs? args = e as RoutedEventArgs;
                this.StartForm(sender, args);
            }
        }

        public void CloseWindow(object? sender, RoutedEventArgs args)
        {
            Close();
        }

        public async void StartForm(object? sender, RoutedEventArgs args)
        {

            var participants = GetParticipants();
            var session = new PairCodingSession(participants.ToArray(), (int)minutesPerTurn.Value);

            var validationMessage = session.Validate();

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error", validationMessage, MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error, WindowStartupLocation.CenterScreen);
                messageBoxStandardWindow?.Show();
                return;
            }

            Hide();

            try
            {
                RunSessionForm sessionForm = new(session);
                sessionForm.Show();
                // Let the session window start in about the location we have right now on the screen
                sessionForm.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
                sessionForm.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                sessionForm.Hide();

                var tcs = new TaskCompletionSource<object>();
                sessionForm.Closed += (s, e) => tcs.SetResult(null);
                sessionForm.Show();
                await tcs.Task;
            }
            catch
            {
            }

            if (_autostart) _autostart= false;

            Show();
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
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Loading Error", "Error Loading configuration File: " + ex.Message, MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error, WindowStartupLocation.CenterScreen);
                messageBoxStandardWindow.Show();
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

        private void RandomizeParticipants(object? sender, RoutedEventArgs args)
        {
            var participants = GetParticipants();


            participant1.Text = "";
            participant2.Text = "";
            participant3.Text = "";
            participant4.Text = "";
            participant5.Text = "";
            participant6.Text = "";
            participant7.Text = "";
            participant8.Text = "";
            participant9.Text = "";
            participant10.Text = "";

            Shuffle(participants);

            if (participants.Count > 0) participant1.Text = participants[0];
            if (participants.Count > 1) participant2.Text = participants[1];
            if (participants.Count > 2) participant3.Text = participants[2];
            if (participants.Count > 3) participant4.Text = participants[3];
            if (participants.Count > 4) participant5.Text = participants[4];
            if (participants.Count > 5) participant6.Text = participants[5];
            if (participants.Count > 6) participant7.Text = participants[6];
            if (participants.Count > 7) participant8.Text = participants[7];
            if (participants.Count > 8) participant9.Text = participants[8];
            if (participants.Count > 9) participant10.Text = participants[9];
        }


        private static Random random = new();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static void AddParticipantIfAvailable(string name, List<string> participants)
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
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open VPC Session",
                Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name = "VPC Session",
                    Extensions = new List<string> { "vpcsession" }
                },
                new FileDialogFilter
                {
                    Name = "All Files",
                    Extensions = new List<string> { "*" }
                }
    },
                AllowMultiple = false
            };

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
            saveFileDialog.Filters?.Add(new FileDialogFilter { Name = "VPC Session", Extensions = { "vpcsession" } });
            saveFileDialog.InitialFileName = string.Join("_", participants);

            var result = await saveFileDialog.ShowAsync(this);

            if (!string.IsNullOrWhiteSpace(result))
            {
                try
                {
                    SessionConfigurationFileHandler.Save(result, new SessionConfiguration(participants, (int)minutesPerTurn.Value));
                    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Config Saved", "Session Configuration saved successfully!", MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Success, WindowStartupLocation.CenterScreen);
                    messageBoxStandardWindow?.Show();
                }
                catch (Exception ex)
                {
                    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error Saving Config", "Error saving configuration File: " + ex.Message, MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error, WindowStartupLocation.CenterScreen);
                    messageBoxStandardWindow?.Show();
                }

            } else
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error Saving Config", "Could not save Configuration File ", MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error, WindowStartupLocation.CenterScreen);
                messageBoxStandardWindow?.Show();

            }
        }

        public void NewSessionClick(object sender, RoutedEventArgs args)
        {
            var session = new SessionConfiguration(new List<string>(), 7);
            LoadSessionIntoGui(session);
        }

    }
}
