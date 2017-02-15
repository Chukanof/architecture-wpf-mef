using System.ComponentModel.Composition;
using System.Windows.Controls;
using XIAOWEN.PLUGIN1.ViewModels;

namespace XIAOWEN.PLUGIN1.Views
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    [Export(typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainViewControl : UserControl
    {
        public MainViewControl()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public ViewModel ViewModel
        {
            get { return this.DataContext as ViewModel; }
            set { this.DataContext = value; }
        }
    }
}
