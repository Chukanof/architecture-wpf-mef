﻿
using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows;
using Telerik.Windows.Controls;
using XIAOWEN.DATA.Log;

namespace XIAOWEN.DATA.BaseTools
{


    public class AppConfig : IConnctionStringConfig, ISavableConfig
    {
        private static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly AppConfig Instance;
        
        public string AccesKey { get; set; }

        private string _ip;
        [Browsable(false)]
        public string IP
        {
            get
            {
                if (string.IsNullOrEmpty(_ip))
                {
                    _ip = GetLocalIPAddress();

                    //string info = string.Format("原来:{0} ,新:{1}", _ip, GetLocalIPAddress());


                    //Logger.Logger.Info(info);

                }

                return _ip;
            }
        }

        private string _machineMac;
        [Browsable(false)]
        public string Machine_Mac
        {
            get
            {
                if (string.IsNullOrEmpty(_machineMac))
                    _machineMac = GetMacAddress();

                Logger.Info("获取用户mac：：" + _machineMac);
                return _machineMac;
            }
        }

        private string _cpuID;
        [Browsable(false)]
        public string CpuID
        {
            get
            {
                if (string.IsNullOrEmpty(_cpuID))
                    _cpuID = GetCpuID();

                Logger.Info("获取用户cpuID：：" + _cpuID);
                return _cpuID;
            }
        }

        [Browsable(false)]
        public string EquipInfo
        {
            get
            {
                return Machine_Mac + "+" + CpuID;
            }
        }
        //[Browsable(false)]
        //public string Sync_Err_UserId
        //{
        //    get
        //    {
        //        return LoginName + "__" + Machine_Mac;
        //    }
        //}

        static AppConfig()
        {
            try
            {
                Instance = new AppConfig();
            }
            catch (Exception)
            {

            }
        }

        private string GetLocalIPAddress()
        {

            try
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            }
            catch (Exception ex)
            {

                Logger.Info(ex.Message);
            }

            return string.Empty;

        }

        private string GetMacAddress()
        {
            try
            {
                string macAddresses = "";
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses = nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                return macAddresses;
            }
            catch (Exception ex)
            {

                Logger.Info("GetMacAddress" + ex.ToString());
            }
            return "";
        }

        /// <summary>
        /// 获取CPU ID
        /// </summary>
        /// <returns></returns>
        private string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码 
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch (Exception ex)
            {
                Logger.Info("GetCpuID" + ex.ToString());
                //return "unknow";
            }
            return "";
        }

        /// <summary>
        /// 本类最终是对这个字段的内容进行修改
        /// </summary>
        private Configuration _innerConfig;
        private AppConfig()
        {
            this.ReadConfig();
        }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string LoginName
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["LoginName"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【LoginName】读取失败");
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["LoginName"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["LoginPwd"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【LoginPwd】读取失败");
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["LoginPwd"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }
        /// <summary>
        /// 登录服务器地址
        /// </summary>
        public string LoginHost
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["LoginHost"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【LoginHost】读取失败");
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["LoginHost"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 登录服务器端口
        /// </summary>
        public string LoginPort
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["LoginPort"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【LoginPort】读取失败");
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["LoginPort"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }

        public int Port
        {
            get { return Convert.ToInt32(LoginPort); }
        }

        /// <summary>
        /// 客户端端口
        /// </summary>
        public string StartPort
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["StartPort"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【StartPort】读取失败");
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["StartPort"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }
        public int StartPortInt
        {
            get { return Convert.ToInt32(StartPort); }
        }
        /// <summary>
        /// 默认恢复上次窗口
        /// </summary>
        public bool IsResumeLastWindow
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(_innerConfig.AppSettings.Settings["IsResumeLastWindow"].Value);
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【IsResumeLastWindow】读取失败");
                }
                return false;
            }
            set
            {
                try
                {
                    _innerConfig.AppSettings.Settings["IsResumeLastWindow"].Value = value.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 阈值
        /// </summary>
        public string Threshold
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["Threshold"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【Threshold】读取失败");
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["Threshold"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 读取配置信息到内存中
        /// </summary>
        private void ReadConfig()
        {
            this._innerConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="isOnlySysInfo">是否只保存基本系统设置信息。</param>
        public bool Save(bool isOnlySysInfo)
        {
            try
            {
                this.SaveConfig(isOnlySysInfo);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 把内存中的值，存储到配置文件中
        /// </summary>
        private void SaveConfig(bool isOnlySysInfo)
        {
            if (!isOnlySysInfo)
            {
                //this.SaveDataBaseConfig();
                //this.SaveRemoteAddress();
                this.SaveLoginUserInfo();
            }

            this._innerConfig.Save();

            if (!isOnlySysInfo)
            {
                //刷新配置文件
                ConfigurationManager.RefreshSection("configuration");

            }
            else
            {
                //刷新配置文件
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string AppShortVersion
        {
            get
            {
                try
                {
                    var assembly = Assembly.GetEntryAssembly();

                    return assembly.GetName().Version.ToString();
                }
                catch (Exception)
                {
                    Logger.Error("AppShortVersion");
                }

                return string.Empty;
            }
        }
        #region 保存当前登录用户信息。

        private void SaveLoginUserInfo()
        {

        }

        #endregion

        #region 系统风格
        
        public Theme CurrentTheme
        {
            get
            {
                try
                {
                    return String2Theme(_innerConfig.AppSettings.Settings["DefaultTheme"].Value);
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【DefaultTheme】读取失败");
                }
                return null;
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["DefaultTheme"].Value = Theme2String(value);
                }
                catch (Exception)
                {

                }
            }
        }

        public string CurrentLayout
        {
            get
            {
                try
                {
                    return _innerConfig.AppSettings.Settings["DefaultLayout"].Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("配置文件读取错误！");
                    Logger.Error("配置节点【DefaultLayout】读取失败");
                }
                return "Layout";
            }
            set
            {
                try
                {
                    this._innerConfig.AppSettings.Settings["DefaultLayout"].Value = value;
                }
                catch (Exception)
                {

                }
            }
        }

        public string Theme2String(Theme theme)
        {
            return theme.ToString();
        }

        public Theme String2Theme(string themeStr)
        {
            if (string.IsNullOrEmpty(themeStr))
            {
                return new Windows7Theme();

            }
            //Office Black - this is the default theme. No assembly is required for it.
            //Office Blue - requires Telerik.Windows.Themes.Office_Blue.dll.
            //Office Silver - requires Telerik.Windows.Themes.Office_Silver.dll.
            //Expression Dark - requires Telerik.Windows.Themes.Expression_Dark.dll.
            //Summer - requires Telerik.Windows.Themes.Summer.dll.
            //Vista - requires Telerik.Windows.Themes.Vista.dll.
            //Windows7 - requires Telerik.Windows.Themes.Windows7.dll.
            //Transparent - requires Telerik.Windows.Themes.Transparent.dll
            //Metro - requires Telerik.Windows.Themes.Metro.dll
            switch (themeStr.ToUpperInvariant())
            {
                case "VISTA":
                    return new VistaTheme();
                case "OFFICEBLACK":
                    return new Office_BlackTheme();
                case "TRANSPARENT":
                    return new TransparentTheme();
                case "WINDOWS7":
                    return new Windows7Theme();
                case "SUMMER":
                    return new SummerTheme();
                case "OFFICESILVER":
                    return new Office_SilverTheme();
                case "OFFICEBLUE":
                    return new Office_BlueTheme();
                case "EXPRESSIONDARK":
                    return new Expression_DarkTheme();
                case "WINDOWS8":
                    return new Windows8Theme();
                case "WINDOWS8TOUCH":
                    return new Windows8TouchTheme();
                default:
                    return new Windows8Theme();
            }
        }

        #endregion
    }
}
