﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace SAaOR_labs.ViewModel
{
    public abstract class LabViewModelBase : ViewModelBase
    {
        private string _inputFile, _outputFile;
        private Visibility _progressBarVisibility, _buttonVisibility;

        #region properties

        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                _inputFile = value;
                OnPropertyChanged(nameof(InputFile));
            }
        }

        public string OutputFile
        {
            get { return _outputFile; }
            set
            {
                _outputFile = value;
                OnPropertyChanged(nameof(OutputFile));
            }
        }

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        public Visibility ButtonVisibility
        {
            get { return _buttonVisibility; }
            set
            {
                _buttonVisibility = value;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }

        #endregion

        #region constructors

        protected LabViewModelBase()
        {
            ButtonVisibility = Visibility.Visible;
            ProgressBarVisibility = Visibility.Collapsed;
        }

        #endregion

        #region commands

        public ICommand OpenFileDialog => new RelayCommand(OpenFileDialogExecute);

        public ICommand SaveFileDialog => new RelayCommand(SaveFileDialogExecute);

        public ICommand DoAction => new RelayCommand(DoActionExecute);

        #endregion

        #region methods

        private void OpenFileDialogExecute()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open File",
                InitialDirectory = Directory.GetCurrentDirectory(),
                RestoreDirectory = false,
                DefaultExt = "in",
                Filter = "Input files (*.csv)|*.csv"
            };
            dialog.ShowDialog();
            InputFile = dialog.FileName;
        }

        private void SaveFileDialogExecute()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Save File As",
                InitialDirectory = Directory.GetCurrentDirectory(),
                RestoreDirectory = false,
                DefaultExt = "out",
                Filter = "Output files (*.csv)|*.csv"
            };
            dialog.ShowDialog();
            OutputFile = dialog.FileName;
        }

        protected virtual void DoActionBodyExecute()
        {
        }

        private async void DoActionExecute()
        {
            ButtonVisibility = Visibility.Collapsed;
            ProgressBarVisibility = Visibility.Visible;
            try
            {
                await Task.Delay(100);
                await Task.Factory.StartNew(DoActionBodyExecute);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                ButtonVisibility = Visibility.Visible;
                ProgressBarVisibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}