using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace XIAOWEN.SHELL.ViewModels
{
    /// <summary>
    /// 继承自BindableBase，在Prism4.0的版本中为NotificationObject
    /// 设置属性SetProperty(ref str1,value)，在prism4.0的版本中为RaisePropertyChanged("propertyName")
    /// 
    /// </summary>
    [Export]
    public class MainWindowViewModel : BindableBase
    {
        string _demoStr;
        public string DemoStr
        {
            get { return _demoStr; }
            set
            {
                SetProperty(ref _demoStr, value);
            }
        }

        public IEnumerable<string> AllcolorsDemo { get; private set; }

        public ICommand DemoCommand { get; private set; }


        readonly IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
