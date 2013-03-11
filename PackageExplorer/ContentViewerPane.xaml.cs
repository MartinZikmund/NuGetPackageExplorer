﻿using System.Windows;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;
using NuGetPackageExplorer.Types;

namespace PackageExplorer
{
    /// <summary>
    /// Interaction logic for ContentViewerPane.xaml
    /// </summary>
    public partial class ContentViewerPane : UserControl
    {
        public ContentViewerPane()
        {
            InitializeComponent();

            SyntaxHighlightingHelper.RegisterHightingExtensions();

            // set the Syntax Highlighting definitions
            LanguageBox.ItemsSource = HighlightingManager.Instance.HighlightingDefinitions;

            // disable unnecessary editor features
            contentBox.Options.CutCopyWholeLine = false;
            contentBox.Options.EnableEmailHyperlinks = false;
            contentBox.Options.EnableHyperlinks = false;
            contentBox.TextArea.SelectionCornerRadius = 0;
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var info = (FileContentInfo) DataContext;
            if (info != null && info.IsTextFile)
            {
                LanguageBox.SelectedItem = SyntaxHighlightingHelper.GuessHighligtingDefinition(info.File.Name);
                contentBox.ScrollToHome();
                contentBox.Load(StreamUtility.ToStream((string) info.Content));
            }
            else
            {
                contentBox.Clear();
            }
        }
    }
}