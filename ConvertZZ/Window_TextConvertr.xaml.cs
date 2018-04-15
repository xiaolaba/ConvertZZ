﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ConvertZZ
{
    /// <summary>
    /// TextConvertr.xaml 的互動邏輯
    /// </summary>
    public partial class Window_TextConvertr : Window, INotifyPropertyChanged
    {
        public Window_TextConvertr()
        {            
            DataContext = this;            
            InitializeComponent();
        }
        Encoding[] encoding = new Encoding[2];
        private void Encoding_Selected(object sender, RoutedEventArgs e)
        {
            RadioButton radiobutton = ((RadioButton)sender);
            switch (radiobutton.GroupName)
            {

            }
        }
        private void Button_Clear_Clicked(object sender, RoutedEventArgs e)
        {
            FileList.Clear();
        }
        private void Button_SelectFile_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog() { Multiselect = true, CheckFileExists = false, CheckPathExists = true, ValidateNames = false };
            fileDialog.InitialDirectory = App.Settings.FileConvert.DefaultPath;
            fileDialog.FileName = "　";
            if (fileDialog.ShowDialog() == true)
            {
                foreach (string str in fileDialog.FileNames)
                {
                    if (Path.GetFileName(str) == "　" && System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(str)))
                    {
                        string folderpath = System.IO.Path.GetDirectoryName(str);
                        List<string> childFileList = System.IO.Directory.GetFiles(folderpath).ToList();
                        childFileList.ForEach(x => FileList.Add(new FileList_Line() { isChecked = true, FileName = System.IO.Path.GetFileName(x), FilePath = folderpath }));
                    }
                    else if (File.Exists(str))
                    {
                        FileList.Add(new FileList_Line() { isChecked = true, FileName = System.IO.Path.GetFileName(str), FilePath = Path.GetDirectoryName(str) });
                    }
                }
                listview.ItemsSource = FileList;
            }
        }


        private ObservableCollection<FileList_Line> _FileList = new ObservableCollection<FileList_Line>();

        public ObservableCollection<FileList_Line> FileList { get => _FileList; set { _FileList = value; OnPropertyChanged("FileList"); } }

        public class FileList_Line
        {
            public int ID { get; set; }
            public bool isChecked { get; set; }     //or IsSelected maybe? whichever name you want  
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

       
    }
}
