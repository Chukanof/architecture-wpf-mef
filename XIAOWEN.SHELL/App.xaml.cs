using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using XIAOWEN.DATA.BaseTools;
using XIAOWEN.DATA.Log;
using XIAOWEN.SHELL.Config;

namespace XIAOWEN.SHELL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            #if (DEBUG)
                        RunInDebugMode();
            #else
                        RunInReleaseMode();
            #endif
        }

        /// <summary>
        /// Debug 方式下运行应用程序
        /// </summary>
        private static void RunInDebugMode()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            StyleManager.ApplicationTheme = AppConfig.Instance.CurrentTheme;
            try
            {
                if (LoginWindow.Execute())
                {
                    Logger.Info(DateTime.Now.ToShortDateString() + "引导完成");
                }
                else
                {
                    Application.Current.Shutdown();
                    Logger.Info("引导登录失败，程序退出.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("引导窗体错误，退出", ex);
                Application.Current.Shutdown();
            }
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
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


        /// <summary>
        /// 初始化资源文件目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="assemblyFullName"></param>
        public static void LoadResourceDictionary(string path, string assemblyFullName)
        {
            string dictionaryName = string.Format("/{0};component/{1}", assemblyFullName, path);
            var uri = new Uri(dictionaryName, UriKind.Relative);
            var dictionary = (ResourceDictionary)Application.LoadComponent(uri);
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

    }
}
