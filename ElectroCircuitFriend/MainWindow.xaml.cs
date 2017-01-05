using ElectroCircuitFriend.Helpers;
using ElectroCircuitFriend.Models;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Windows;
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

        private void SaveVoltageSettings(object sender, RoutedEventArgs e)
        {
            var newVoltage = new Volt(double.Parse(txtVoltage.Text), ckbMillivolts.IsChecked.Value);

            BackgroundWorker bw = new BackgroundWorker();
            // what to do in the background thread
            bw.DoWork += delegate
            {
                using (var db = new DataContext())
                {
                    db.Volts.AddOrUpdate(newVoltage);
                    db.SaveChanges();
                }
            };
            bw.RunWorkerCompleted += delegate
            {
                lblStatusText.Text = "Voltage saved";
            };
            bw.RunWorkerAsync();
        }

        private void SaveAmpSettings(object sender, RoutedEventArgs e)
        {
            var newAmpare = new Amp(double.Parse(txtAmp.Text), ckbMilliAmp.IsChecked.Value);

            BackgroundWorker bw = new BackgroundWorker();
            // what to do in the background thread
            bw.DoWork += delegate
            {
                using (var db = new DataContext())
                {
                    db.Amps.AddOrUpdate(newAmpare);
                    db.SaveChanges();
                }
            };
            bw.RunWorkerCompleted += delegate
            {
                lblStatusText.Text = "Ampere saved";
            };
            bw.RunWorkerAsync();
        }

        private void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                txtImageName.Text = fileDialog.FileName;
            }
        }

        private void OnDataSheetButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                txtDataSheet.Text = fileDialog.FileName;
            }
        }

        private void SaveComponentOnClick(object sender, RoutedEventArgs e)
        {
            var selectedCategory = (ComponentCategories)((ComboboxItem)cmbComponentCategory.SelectedItem).Value;
            var newComponent = new Component(selectedCategory, txtComponentName.Text, txtComponentDescription.Text,
                txtImageName.Text, txtDataSheet.Text, int.Parse(txtStockAmount.Text), int.Parse(txtUsedAmount.Text));

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
    }
}