﻿using System.Windows;
using Imya.Utils;
using Imya_UI.Properties;
using Imya.Models;

namespace Imya_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Setup Managers
            TextManager TextManager = new TextManager(Settings.Default.LANGUAGE_FILE_PATH);
            ModDirectoryManager ModDirectoryManager = new ModDirectoryManager(Settings.Default.MOD_DIRECTORY_PATH);
            TextManager.Instance.ChangeLanguage(ApplicationLanguage.English);
        }
    }
}
