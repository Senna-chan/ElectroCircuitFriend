using ElectroCircuitFriend.Helpers;
using ElectroCircuitFriend.Models;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Component = ElectroCircuitFriend.Models.Component;

namespace ElectroCircuitFriend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < Enum.GetNames(typeof(ComponentCategories)).Length; i++)
            {
                var str = Enum.GetName(typeof(ComponentCategories), i);
                cmbComponentCategory.Items.Add(new ComboboxItem(i, str));
            }
            cmbComponentCategory.SelectedIndex = 0;
        }

        private void OnTextChange(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !GenericHelpers.IsTextAllowed(e.Text);
        }

        private void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog {Multiselect = false};
            if (fileDialog.ShowDialog() == true)
            {
                txtImageName.Text = fileDialog.FileName;
            }
        }

        private void OnDataSheetButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog {Multiselect = false};
            if (fileDialog.ShowDialog() == true)
            {
                txtDataSheet.Text = fileDialog.FileName;
            }
        }

        private void OnPinoutImageButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog {Multiselect = false};
            if (fileDialog.ShowDialog() == true)
            {
                txtComponentPinout.Text = fileDialog.FileName;
            }
        }

        private async void SaveComponentOnClick(object sender, RoutedEventArgs e)
        {
            var selectedCategory = (ComponentCategories)((ComboboxItem)cmbComponentCategory.SelectedItem).Value;
            string datasheetFileName;
            if (txtDataSheet.Text != string.Empty)
            {
                datasheetFileName = txtComponentName.Text + "-datasheet.pdf";
                if (txtDataSheet.Text.StartsWith("http") || txtDataSheet.Text.StartsWith("ftp"))
                {
                    using (var webclient = new WebClient())
                    {
                        lblStatusText.Text = "Downloading PDF.";
                        await webclient.DownloadFileTaskAsync(new Uri(txtDataSheet.Text),
                            Path.Combine(App.ComponentAssets, datasheetFileName));
                        lblStatusText.Text = "Downloaded PDF.";
                    }
                }
                else
                {
                    File.Copy(txtDataSheet.Text, Path.Combine(App.ComponentAssets, datasheetFileName));
                }
            }
            else
            {
                datasheetFileName = "";
            }

            string componentImageFileName;
            if (txtImageName.Text != string.Empty)
            {
                componentImageFileName = txtComponentName.Text + "-image.png";
                if (txtImageName.Text.StartsWith("http") || txtImageName.Text.StartsWith("ftp"))
                {
                    using (var webclient = new WebClient())
                    {
                        lblStatusText.Text = "Downloading Image.";
                        await webclient.DownloadFileTaskAsync(new Uri(txtImageName.Text),
                            Path.Combine(App.ComponentAssets, datasheetFileName));
                        lblStatusText.Text = "Downloaded Image.";
                    }
                }
                else
                {
                    File.Copy(txtImageName.Text, Path.Combine(App.ComponentAssets, datasheetFileName));
                }
            }
            else
            {
                componentImageFileName = "";
            }

            string componentPinoutFileName;
            if (txtComponentPinout.Text != string.Empty)
            {
                componentPinoutFileName = txtComponentName.Text + "-pinoutImage.png";
                if (txtComponentPinout.Text.StartsWith("http") || txtComponentPinout.Text.StartsWith("ftp"))
                {
                    using (var webclient = new WebClient())
                    {
                        lblStatusText.Text = "Downloading Pinout Image.";
                        await webclient.DownloadFileTaskAsync(new Uri(txtComponentPinout.Text),
                            Path.Combine(App.ComponentAssets, datasheetFileName));
                        lblStatusText.Text = "Downloaded Pinout Image.";
                    }
                }
                else
                {
                    File.Copy(txtComponentPinout.Text, Path.Combine(App.ComponentAssets, componentPinoutFileName), false);
                }
            }
            else
            {
                componentPinoutFileName = "";
            }

            var newComponent = new Component();
            newComponent.ComponentCategory = selectedCategory;
            newComponent.Name = txtComponentName.Text;
            newComponent.Description = txtComponentDescription.Text;
            newComponent.ComponentImage = txtImageName.Text;
            newComponent.DataSheet = datasheetFileName;
            newComponent.ComponentPinoutImage = componentPinoutFileName;
            newComponent.InStock = int.Parse(txtStockAmount.Text);
            newComponent.Used = int.Parse(txtUsedAmount.Text);

            BackgroundWorker bw = new BackgroundWorker();
            // what to do in the background thread
            bw.DoWork += delegate
            {
                using (var db = new DataContext())
                {
                    db.Components.AddOrUpdate(c => c.Name, newComponent);
                    var savedChanges = db.SaveChanges();
                }
            };
            bw.RunWorkerCompleted += delegate {
                lblStatusText.Text = "Component saved";
            };
            bw.RunWorkerAsync();
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            if(((TextBox) sender).Text == "Enter path or URL") ((TextBox) sender).Text = string.Empty;
        }
    }
}