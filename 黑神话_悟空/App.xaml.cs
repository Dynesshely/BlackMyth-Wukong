using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;

namespace 黑神话_悟空
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Random Random = new();

        public static string? WorkBase = Path.GetDirectoryName(
            Process.GetCurrentProcess().MainModule?.FileName);

        public static SoundPlayer SoundPlayer = new();
    }
}
