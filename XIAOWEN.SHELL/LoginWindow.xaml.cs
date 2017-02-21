using System;
using System.Windows;
using XIAOWEN.DATA.Log;

namespace XIAOWEN.SHELL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            MessageBox.Show("Login");


            SplashWindow tempOpen = new SplashWindow();
            tempOpen.ShowLoadProcess += tempOpen_ShowLoadProcess;

            tempOpen.SystemStartLoaded();
        }

        private void tempOpen_ShowLoadProcess(string obj)
        {
            //this.progressReport.Value = this.progressReport.Value + 10;
            //Logger.Info(txt + "---->" + progressReport.Value);
            //if (this.progressReport.Value >= 100)
            //    this.Close();
        }

        public bool Result { get; set; }

        /// <summary>
        /// 应用程序执行
        /// 防止应用程序登陆后异常退出
        /// </summary>
        /// <returns></returns>
        public static bool Execute()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;  //防止登录后Application退出
            LoginWindow loginWin = new LoginWindow();
            loginWin.ShowDialog();
            try
            {
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
            catch (Exception ex)
            {
                Logger.Info("应用程序启动期间出现异常：{0}", ex.Message);
            }

            return loginWin.Result;
        }
    }
}
