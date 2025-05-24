using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisualPairCoding.BL;
using VisualPairCoding.Infrastructure;
using static System.Collections.Specialized.BitVector32;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class EnterNamesForm : Window
    {
        private bool _autostart = false;
        private bool isTotalDurationActivated = false;
        public EnterNamesForm()
        {
            InitializeComponent();
        }

        public EnterNamesForm(bool autostart, string configPath) 
        {
            InitializeComponent();

            _autostart = autostart;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Opened += OnActivated;
            Closed += OnClosed;

            if (!string.IsNullOrEmpty(configPath))
            {
                LoadSessionIntoGui(configPath);                
            }
        }

        private MenuItem GetRecentMenuItem()
        {
            return this.FindControl<MenuItem>("recentMenuItem")!;
        }

        private TimeSpan GetSessionTotalDuration()
        {
            if (timePicker.SelectedTime.HasValue)
            {
                return timePicker.SelectedTime.Value;
            }

            return new TimeSpan(0, 0, 0);
        }


        private void TotalDurationCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;

            if (checkbox.IsChecked == true)
            {
                timePicker.IsVisible = true;
                isTotalDurationActivated = true;
            }

        }


        private void TotalDurationCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox)sender;

            if (checkbox.IsChecked == false)
            {
                timePicker.IsVisible = false;
                isTotalDurationActivated = false;
            }

        }

        private void OnClosed(object? sender, EventArgs e)
        {
            SessionConfigurationFolderHandler.SaveAsRecentSession(
                new SessionConfiguration(GetParticipants(), (int)(minutesPerTurn.Value ?? 1))
            );
        }

        private void OpenAboutForm(object? sender, RoutedEventArgs args)
        {
            AboutForm aboutForm = new();
            aboutForm.Show();
        }

        private void OnMenuItemClicked(object? sender, RoutedEventArgs e)
        {
            MenuItem clickedMenuItem = (MenuItem)e.Source!;
            string subMenuHeader = clickedMenuItem.Header!.ToString()!;
            
            var sessionConfiguration = SessionConfigurationFolderHandler.LoadRecentSession(subMenuHeader);
            
            LoadSessionIntoGui(sessionConfiguration);
        }

        private void OnActivated(object? sender, EventArgs e)
        {
            MenuItem recentSessionsMenuItem = GetRecentMenuItem();

            var configs = SessionConfigurationFolderHandler.GetRecentSessionNames();

            recentSessionsMenuItem.Items.Clear();
            foreach(var config in configs)
            {
                recentSessionsMenuItem.Items.Add(config);
            }

            if (configs.Length > 0) recentSessionsMenuItem.Click += OnMenuItemClicked;
            recentSessionsMenuItem.IsEnabled = configs.Length > 0;
            
            recentSessionsMenuItem.SelectedIndex = 0;

            if (_autostart)
            {
                RoutedEventArgs? args = e as RoutedEventArgs;
                this.StartForm(sender, args!);
            }
        }

        public void CloseWindow(object? sender, RoutedEventArgs args)
        {
            Close();
        }

        public async void StartForm(object? sender, RoutedEventArgs args)
        {
            var participants = GetParticipants();
            var totalDuration = GetSessionTotalDuration();
            var session = new PairCodingSession(participants, (int)minutesPerTurn.Value!, GetSessionTotalDuration());
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
                RunSessionForm sessionForm = new(session, isTotalDurationActivated);
                sessionForm.Show();
                // Let the session window start in about the location we have right now on the screen
                sessionForm.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
                sessionForm.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                sessionForm.Hide();

                var tcs = new TaskCompletionSource<object>();
                sessionForm.Closed += (s, e) => tcs.SetResult(null!);
                sessionForm.Show();
                await tcs.Task;
            }
            catch
            {
            }

            if (_autostart) _autostart= false;

            Show();
        }

        private void LoadSessionIntoGui(string fileName)
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

            for (var i = 1; i <= session.Participants.Length; i++)
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

            if (participants.Length > 0) participant1.Text = participants[0];
            if (participants.Length > 1) participant2.Text = participants[1];
            if (participants.Length > 2) participant3.Text = participants[2];
            if (participants.Length > 3) participant4.Text = participants[3];
            if (participants.Length > 4) participant5.Text = participants[4];
            if (participants.Length > 5) participant6.Text = participants[5];
            if (participants.Length > 6) participant7.Text = participants[6];
            if (participants.Length > 7) participant8.Text = participants[7];
            if (participants.Length > 8) participant9.Text = participants[8];
            if (participants.Length > 9) participant10.Text = participants[9];
        }

        private static readonly Random random = new();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        private static void AddParticipantIfAvailable(string name, List<string> participants)
        {
            if (!string.IsNullOrWhiteSpace(name))
                participants.Add(name);
        }

        private string[] GetParticipants()
        {
            var participants = new List<string>();
            AddParticipantIfAvailable(participant2.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant1.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant3.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant4.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant5.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant6.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant7.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant8.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant9.Text ?? string.Empty, participants);
            AddParticipantIfAvailable(participant10.Text ?? string.Empty, participants);

            return participants.ToArray();
        }

        public async void LoadSessionConfiguration(object? sender, RoutedEventArgs args)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Open VPC Session",
                Filters = new List<FileDialogFilter>
                {
                    new()
                    {
                        Name = "VPC Session",
                        Extensions = new List<string> { "vpcsession" }
                    },
                    new()
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

            SaveFileDialog saveFileDialog = new();
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
            } 
            else
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Error Saving Config", "Could not save Configuration File ", MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error, WindowStartupLocation.CenterScreen);
                messageBoxStandardWindow?.Show();

            }
        }

        public void NewSessionClick(object sender, RoutedEventArgs args)
        {
            var session = new SessionConfiguration(Array.Empty<string>(), 7);
            LoadSessionIntoGui(session);
        }
    }
}
