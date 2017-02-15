using System;
using System.Windows;
using XIAOWEN.DATA.Log;

namespace XIAOWEN.SHELL.Config
{
    public class AppRunEnvirenmentConfig
    {
        /// <summary>
        /// 在Debug版本下运行的模式
        /// 主要功能，记录日志
        /// </summary>
        internal static void RunInDebugMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;

            //是否能登录成功
            try
            {
                if (LoginWindow.Execute())
                {

                    Logger.Info(DateTime.Now.ToShortTimeString() + "引导完成");
                }
                else
                {
                    Application.Current.Shutdown();
                    Logger.Info("引导登录失败，程序退出");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("引导窗体错误，退出", ex);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 在Release版本下运行的模式
        /// </summary>
        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                MyBootstrapperConfig bootstrapper = new MyBootstrapperConfig();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null) return;

            MessageBox.Show(ex.Message);
            MessageBox.Show(ex.InnerException.Message);

            Environment.Exit(1);
        }
    }
}
