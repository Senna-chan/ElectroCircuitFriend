using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElectroCircuitFriend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string AssetDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
        public static string ComponentAssets = Path.Combine(AssetDir, "ComponentAssets");
    }
}
