using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;

namespace DocHandler.Views
{
    public partial class RecentLocationsDialog : Window
    {
        public string SelectedLocation { get; private set; }
        private ObservableCollection<LocationItem> LocationItems { get; set; }

        public RecentLocationsDialog(List<string> recentLocations)
        {
            InitializeComponent();
            
            // Convert recent locations to LocationItems
            LocationItems = new ObservableCollection<LocationItem>();
            
            foreach (var location in recentLocations.Take(10)) // Show up to 10 recent locations
            {
                if (Directory.Exists(location))
                {
                    LocationItems.Add(new LocationItem 
                    { 
                        FullPath = location
                    });
                }
            }
            
            LocationsList.ItemsSource = LocationItems;
            
            // Select first item if available
            if (LocationItems.Any())
            {
                LocationsList.SelectedIndex = 0;
            }
        }



        private void LocationsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LocationsList.SelectedItem is LocationItem item)
            {
                SelectedLocation = item.FullPath;
                DialogResult = true;
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationsList.SelectedItem is LocationItem item)
            {
                SelectedLocation = item.FullPath;
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Select save location for documents",
                UseDescriptionForTitle = true
            };
            
            // Set initial directory to selected item if any
            if (LocationsList.SelectedItem is LocationItem selectedItem)
            {
                dialog.SelectedPath = selectedItem.FullPath;
            }
            
            if (dialog.ShowDialog() == true)
            {
                SelectedLocation = dialog.SelectedPath;
                DialogResult = true;
            }
        }

        private class LocationItem
        {
            public string FullPath { get; set; }
        }
    }
}