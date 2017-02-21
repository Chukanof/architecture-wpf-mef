using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using XIAOWEN.DATA.Log;
using XIAOWEN.SHELL.Config;

namespace XIAOWEN.SHELL
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public Action hideDelegate;
        Action<string> showDelegate;
        public event Action<string> ShowLoadProcess;

        public SplashWindow()
        {
            InitializeComponent();
            hideDelegate = new Action(hideText);
            showDelegate = new Action<string>(showText);
        }

        private void hideText()
        {
            //BeginStoryboard(Hideboard);
        }

        private void showText(string text)
        {
            //if (ShowLoadProcess != null)
            //    ShowLoadProcess.Invoke(text);

            txtLoading.Text = text;
            //BeginStoryboard(Showboard);
        }

        public void SystemStartLoaded()
        {
            Thread thread = new Thread(load);
            thread.Start();
        }

        void load()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Render, showDelegate, "引导初始化......");

            Dispatcher.Invoke(hideDelegate);
            this.Dispatcher.Invoke(showDelegate, "载入程序中......");
            MyBootstrapperConfig bootstrapper = new MyBootstrapperConfig();
            bootstrapper.OwnerDispater = this.Dispatcher;
            bootstrapper.ShowDelegateEvent = showText;
            bootstrapper.Run();
            Logger.Info("已引导完成");
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
            {
                Logger.Info("已进入主界面显示");
                Application.Current.MainWindow.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Show();
                Logger.Info("显示最后一步进度");
                this.Dispatcher.Invoke(showDelegate, "显示界面......");
            });

            Dispatcher.Invoke(DispatcherPriority.Loaded, (Action)delegate { Close(); });
        }
    }
}
