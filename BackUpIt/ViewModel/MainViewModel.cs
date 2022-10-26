using BackUpIt.Model;
using BackUpIt.Properties;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BackUpIt.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Авторизовался ли пользователь
        /// </summary>
        public bool LoggedIn { get; set; } = false;

        /// <summary>
        /// Путь копируемого файла
        /// </summary>
        private Model.File copiedFile = new Model.File();
        public string CopiedFile
        {
            get { return copiedFile.Path; }
            set
            {
                copiedFile.Path = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбрать путь файла для копирования
        /// </summary>
        public ICommand SelectPathToCopy
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var dlg = new CommonOpenFileDialog();
                    dlg.Title = "My Title";
                    dlg.IsFolderPicker = true;

                    dlg.AddToMostRecentlyUsedList = false;
                    dlg.AllowNonFileSystemItems = false;
                    dlg.EnsureFileExists = true;
                    dlg.EnsurePathExists = true;
                    dlg.EnsureReadOnly = false;
                    dlg.EnsureValidNames = true;
                    dlg.Multiselect = false;
                    dlg.ShowPlacesList = true;

                    if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        CopiedFile = dlg.FileName;
                    }
                });
            }
        }

        /// <summary>
        /// Получить токен
        /// </summary>
        public ICommand Login
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var destURL = "https://oauth.yandex.ru/authorize?response_type=token&client_id=21a90a035d194877ba691409eecb8ee1";
                    var sInfo = new System.Diagnostics.ProcessStartInfo(destURL)
                    {
                        UseShellExecute = true,
                    };
                    System.Diagnostics.Process.Start(sInfo);
                });
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}