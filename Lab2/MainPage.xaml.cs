using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async void HandleOpenFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.List,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".txt");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stringContent = await FileIO.ReadLinesAsync(file);
                editor.Text = string.Join(System.Environment.NewLine, stringContent.ToArray());
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    CloseButtonText = "Close"
                };
                contentDialog.Title = "Action failed.";
                contentDialog.Content = "Please choose a file to open!";
                await contentDialog.ShowAsync();
            }
        }

        public async void HandleSaveFile()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "New Document";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            ContentDialog contentDialog = new ContentDialog
            {
                CloseButtonText = "Close"
            };
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, editor.Text);
                contentDialog.Title = "Action success.";
                contentDialog.Content = "Save file success.";
            }
            else
            {
                contentDialog.Title = "Action failed.";
                contentDialog.Content = "Please choose a file to save!";
            }

            await contentDialog.ShowAsync();
        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuFlyoutItem;
            switch (menuItem.Tag)
            {
                case "new":

                    break;

                case "open":
                    HandleOpenFile();
                    break;

                case "save":
                    HandleSaveFile();
                    break;

                case "exit":

                    break;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("sample.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            Windows.Storage.StorageFile file = await storageFolder.GetFileAsync("sample.txt");
            Debug.WriteLine(file.Path);
            ContentDialog contentDialog = new ContentDialog
            {
                CloseButtonText = "Close"
            };

            await FileIO.WriteTextAsync(file, editor.Text);
            contentDialog.Title = "Action success.";
            contentDialog.Content = "Save file to local storage folder success.";
            await contentDialog.ShowAsync();
        }
    }
}