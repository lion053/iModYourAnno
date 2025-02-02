﻿using Imya.Models;
using Imya.Models.ModTweaker;
using Imya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imya.UI.Views
{
    /// <summary>
    /// Interaktionslogik für ModTweakerView.xaml
    /// </summary>
    public partial class ModTweakerView : UserControl
    {
        public ModTweakerView()
        {
            InitializeComponent();

            ModSelection.ModList_SelectionChanged += TweakerFileView.UpdateCurrentDisplay;
        }
    }
}
