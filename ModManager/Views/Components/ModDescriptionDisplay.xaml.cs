﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Imya.Enums;
using Imya.Models;
using Imya.Models.ModMetadata;
using Imya.UI.Utils;
using Imya.Utils;

namespace Imya.UI.Components
{
    /// <summary>
    /// Displays mod readme and some meta data.
    /// </summary>
    public partial class ModDescriptionDisplay : UserControl, INotifyPropertyChanged
    {
        #region FieldBacking
        private Mod _mod;
        private bool _showKnownIssues;
        private bool _showDescription;
        private bool _showCreatorName;
        private bool _showVersion;
        private bool _showDlcDeps;
        private bool _showImage;
        private bool _showModID;
        private double _descriptionTextWidth;
        private double _knownIssueTextWidth;

        private DlcId[] _DlcIds = Array.Empty<DlcId>();
        #endregion

        #region Fields

        // Needs retrigger of OnPropertyChanged on language change
        public DlcId[] DlcIds
        {
            get => _DlcIds;
            set
            {
                _DlcIds = value;
                OnPropertyChanged(nameof(DlcIds));
            }
        }
        public Mod Mod
        {
            get => _mod;
            private set
            {
                _mod = value;
                OnPropertyChanged(nameof(Mod));
            }
        }

        public bool ShowKnownIssues {
            get => _showKnownIssues;
            set
            {
                _showKnownIssues = value;
                OnPropertyChanged(nameof(ShowKnownIssues));
            }
        }

        public bool ShowDescription
        {
            get => _showDescription;
            set
            {
                _showDescription = value;
                OnPropertyChanged(nameof(ShowDescription));
            }
        }

        public bool ShowCreatorName
        {
            get => _showCreatorName;
            set
            {
                _showCreatorName = value;
                OnPropertyChanged(nameof(ShowCreatorName));
            }
        }

        public bool ShowVersion
        {
            get => _showVersion;
            set
            {
                _showVersion = value;
                OnPropertyChanged(nameof(ShowVersion));
            }
        }

        public bool ShowDlcDeps
        {
            get => _showDlcDeps;
            set
            {
                _showDlcDeps = value;
                OnPropertyChanged(nameof(ShowDlcDeps));
            }
        }

        public bool ShowImage { 
            get=> _showImage;
            set
            { 
                _showImage = value;
                OnPropertyChanged(nameof(ShowImage));
            }
        }

        public bool ShowModID
        {
            get => _showModID;
            set
            {
                _showModID = value;
                OnPropertyChanged(nameof(ShowModID));
            }
        }

        public double DescriptionTextWidth {
            get => _descriptionTextWidth;
            set
            {
                _descriptionTextWidth = value;
                OnPropertyChanged(nameof(DescriptionTextWidth));
            }
        }

        public double KnownIssueTextWidth {
            get => _knownIssueTextWidth;
            set
            {
                _knownIssueTextWidth = value;
                OnPropertyChanged(nameof(KnownIssueTextWidth));
            }
        }

        #endregion

        public TextManager TextManager { get; } = TextManager.Instance;

        public ModDescriptionDisplay()
        {
            InitializeComponent();
            DataContext = this;
            TextManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private DlcId[] GetDlcDependencies(Dlc[]? dependencies)
        {
            if (dependencies is not null)
            {
                return dependencies.Select(x => x.DLC).Where(x => x is DlcId).Select(x => (DlcId)x).OrderBy(x => x).ToArray();
            }
            else return new DlcId[0];
        }


        public void SetDisplayedMod(Mod m)
        {
            Mod = m;

            bool Exists = m is not null;

            ShowKnownIssues = Exists && m.HasKnownIssues;
            ShowDescription = Exists && m.HasDescription ;
            ShowCreatorName = Exists && m.HasCreator ;
            ShowVersion = Exists && m.HasVersion ;
            ShowDlcDeps = Exists && m.HasDlcDependencies;
            ShowModID = Exists && Properties.Settings.Default.ModCreatorMode && m.HasModID;

            DlcIds = GetDlcDependencies(m?.DlcDependencies);

            //the default behavior for images is different: If the mod does not have an image, it will show a placeholder. 
            //Only hide the image in case there is no displayed mod.
            ShowImage = Exists;
        }

        public void OnCopyModIDClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(Mod.ModID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not access windows clipboard.");
            }
        }

        private void OnLanguageChanged(ApplicationLanguage language)
        {
            // force update of DLC ids
            DlcIds = DlcIds.ToArray();
        }

        #region INotifyPropertyChangedMembers
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OnSizeChanged(object sender, SizeChangedEventArgs s) 
        {
            DescriptionTextWidth = BaseGrid.ActualWidth > 20 ? BaseGrid.ActualWidth - 20 : 20;
            KnownIssueTextWidth = BaseGrid.ActualWidth > 50 ? BaseGrid.ActualWidth - 50 : 50;
        }
        #endregion
    }
}
