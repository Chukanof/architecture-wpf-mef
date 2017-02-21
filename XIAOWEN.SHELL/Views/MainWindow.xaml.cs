using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Shapes;
using Telerik.Windows;
using XIAOWEN.SHELL.ViewModels;

namespace XIAOWEN.SHELL.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    [Export]
    public partial class MainWindow : Window, IPartImportsSatisfiedNotification
    {
        public MainWindow()
        {
            InitializeComponent();

            this.LayoutUpdated += MainWindow_LayoutUpdated;
        }

        private void MainWindow_LayoutUpdated(object sender, EventArgs e)
        {
            SofaContainerContent1.UpdateLayout();
        }

        [Import(AllowRecomposition = false)]
        public MainWindowViewModel MainWindowViewModel
        {
            get { return this.DataContext as MainWindowViewModel; }
            set { this.DataContext = value; }
        }

        /// <summary>
        /// Implement for IPartImportsSatisfiedNotification interface
        /// </summary>
        public void OnImportsSatisfied()
        {

        }

        private void TopMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void OpendWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OpendWindow_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void OpendWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void OpendWindowListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void RadMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void RadMenuItemClose_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void imgElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TxtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Handle_Click(object sender, RadRoutedEventArgs e)
        {

        }

        private void OnRadMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 弹出框不透明处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadWindowOpacity_Click(object sender, RadRoutedEventArgs e)
        {

        }
    }
}
