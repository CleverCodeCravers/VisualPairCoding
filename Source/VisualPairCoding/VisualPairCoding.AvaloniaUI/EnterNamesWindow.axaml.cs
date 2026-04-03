using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
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
        private bool isTotalDurationActivated = false;
        private readonly SessionConfigurationFileHandler _fileHandler = new();
        private readonly SessionConfigurationFolderHandler _folderHandler = new();

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
            _folderHandler.SaveAsRecentSession(
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
            
            var sessionConfiguration = _folderHandler.LoadRecentSession(subMenuHeader);
            
            LoadSessionIntoGui(sessionConfiguration);
        }

        private void OnActivated(object? sender, EventArgs e)
        {
            MenuItem recentSessionsMenuItem = GetRecentMenuItem();

            var configs = _folderHandler.GetRecentSessionNames();

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
                await MessageBoxHelper.ShowError(this, "Error", validationMessage);
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
            catch (Exception ex)
            {
                await MessageBoxHelper.ShowError(this, "Session Error", ex.Message);
            }

            if (_autostart) _autostart= false;

            Show();
        }

        private async void LoadSessionIntoGui(string fileName)
        {
            try
            {
                var session = _fileHandler.Load(fileName);

                LoadSessionIntoGui(session);
            }
            catch (Exception ex)
            {
                await MessageBoxHelper.ShowError(this, "Loading Error", "Error Loading configuration File: " + ex.Message);
            }
        }

        public void LoadSessionIntoGui(SessionConfiguration session)
        {
            participant1.Focus();

            for (var i = 1; i <= MaxParticipants; i++)
            {
                var control = GetParticipantTextBox(i);
                if (control != null)
                {
                    control.Text = (i - 1 < session.Participants.Length)
                        ? session.Participants[i - 1].Trim()
                        : "";
                }
            }

            minutesPerTurn.Value = session.SessionLength;
        }

        private const int MaxParticipants = 10;

        private TextBox? GetParticipantTextBox(int index)
        {
            return this.FindControl<TextBox>("participant" + index);
        }

        private void RandomizeParticipants(object? sender, RoutedEventArgs args)
        {
            var participants = GetParticipants();
            Shuffle(participants);

            for (var i = 1; i <= MaxParticipants; i++)
            {
                var control = GetParticipantTextBox(i);
                if (control != null)
                {
                    control.Text = (i - 1 < participants.Length) ? participants[i - 1] : "";
                }
            }
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

        private string[] GetParticipants()
        {
            var participants = new List<string>();

            for (var i = 1; i <= MaxParticipants; i++)
            {
                var control = GetParticipantTextBox(i);
                var text = control?.Text ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(text))
                    participants.Add(text);
            }

            return participants.ToArray();
        }

        public async void LoadSessionConfiguration(object? sender, RoutedEventArgs args)
        {
            var fileTypes = new List<FilePickerFileType>
            {
                new("VPC Session")
                {
                    Patterns = new[] { "*.vpcsession" }
                },
                FilePickerFileTypes.All
            };

            var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open VPC Session",
                FileTypeFilter = fileTypes,
                AllowMultiple = false
            });

            if (files.Count > 0)
            {
                LoadSessionIntoGui(files[0].Path.LocalPath);
            }
        }
        public async void SaveSessionConfiguration(object? sender, RoutedEventArgs args)
        {
            var participants = GetParticipants();

            var fileTypes = new List<FilePickerFileType>
            {
                new("VPC Session")
                {
                    Patterns = new[] { "*.vpcsession" }
                }
            };

            var file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Save VPC Session",
                FileTypeChoices = fileTypes,
                SuggestedFileName = string.Join("_", participants),
                DefaultExtension = "vpcsession"
            });

            if (file != null)
            {
                try
                {
                    _fileHandler.Save(file.Path.LocalPath, new SessionConfiguration(participants, (int)minutesPerTurn.Value!));
                    await MessageBoxHelper.ShowInfo(this, "Config Saved", "Session Configuration saved successfully!");
                }
                catch (Exception ex)
                {
                    await MessageBoxHelper.ShowError(this, "Error Saving Config", "Error saving configuration File: " + ex.Message);
                }
            } 
            else
            {
                await MessageBoxHelper.ShowError(this, "Error Saving Config", "Could not save Configuration File");

            }
        }

        public void NewSessionClick(object sender, RoutedEventArgs args)
        {
            var session = new SessionConfiguration(Array.Empty<string>(), 7);
            LoadSessionIntoGui(session);
        }
    }
}
