using Blue10SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blue10SdkWpfExample
{
    /// <summary>
    /// Interaction logic for DocumentAction.xaml
    /// </summary>
    public partial class DocumentActionWindow : Window
    {
        public DocumentActionWindow(B10DeskHelper pB10DH, DocumentAction pDocAction)
        {
            InitializeComponent();
            B10DH = pB10DH;
            DocAction = pDocAction;
        }

        private B10DeskHelper B10DH { get; set; }
        private DocumentAction DocAction { get; set; }

        private void CloseNoSave(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //
    }
}
