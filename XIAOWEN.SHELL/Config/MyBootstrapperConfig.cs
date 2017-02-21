using Prism.Mef;
using System.Windows.Threading;
using System.ComponentModel.Composition.Hosting;
using System.Collections.Generic;
using System;
using Prism.Modularity;
using Prism.Regions;
using System.Linq;
using System.Windows;
using XIAOWEN.SHELL.Views;
using SING.Modules.Desktop;
using XIAOWEN.INFRASTRUCTURE.Behaviors;

namespace XIAOWEN.SHELL.Config
{
    /// <summary>
    /// 程序启动
    /// 记载Desktop及相关模块的组件，即Plugins
    /// </summary>
    public class MyBootstrapperConfig : MefBootstrapper
    {
        public System.Windows.Threading.Dispatcher OwnerDispater { get; set; }
        public Action<string> ShowDelegateEvent;

        /// <summary>
        /// 记录应用程序运行日志
        /// </summary>
        /// <param name="runWithDefaultConfiguration"></param>
        public override void Run(bool runWithDefaultConfiguration)
        {
            this.Logger = this.CreateLogger();

            if (this.Logger == null)
            {
                OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化日志出错！");

            }
            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化日志......");

            this.ModuleCatalog = this.CreateModuleCatalog();
            if (this.ModuleCatalog == null)
            {
                OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化日志出错！");
            }

            this.ConfigureModuleCatalog();
            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "注册相关目录......");

            AggregateCatalog = this.CreateAggregateCatalog();

            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "注册窗体......");
            this.ConfigureAggregateCatalog();

            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "扫描相关目录，......");
            this.RegisterDefaultTypesIfMissing();
            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化类型......");

            this.Container = this.CreateContainer();

            if (this.Container == null)
            {
                OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化IOC出错！");
            }
            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化IOC......");

            this.ConfigureContainer();

            this.ConfigureServiceLocator();
            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化服务......");

            this.ConfigureRegionAdapterMappings();

            this.ConfigureDefaultRegionBehaviors();

            this.RegisterFrameworkExceptionTypes();
            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化相关模块，......");

            IEnumerable<Lazy<object, object>> exports = this.Container.GetExports(typeof(IModuleManager), null, null);

            if ((exports != null) && (exports.Count() > 0))//注意这里的Count()方法需要依赖System.linq包
            {
                this.InitializeModules();
            }

            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化主窗体......");

            if (OwnerDispater != null)
                OwnerDispater.Invoke(DispatcherPriority.Normal,
                                     (Action)delegate ()
                                     {
                                         this.Shell = this.CreateShell();
                                         OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化完成......");
                                         if (this.Shell != null)
                                         {
                                             //((Window) Shell).Opacity = 0.3;
                                             RegionManager.SetRegionManager(this.Shell, this.Container.GetExportedValue<IRegionManager>());
                                             OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化完成......");

                                             RegionManager.UpdateRegions();

                                             OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化完成......");
                                             this.InitializeShell();
                                             OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化完成......");
                                         }
                                     });

            OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化完成......");
        }

        protected override void ConfigureAggregateCatalog()
        {
            try
            {
                base.ConfigureAggregateCatalog();

                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MyBootstrapperConfig).Assembly));
                //Desktop
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DesktopModule).Assembly));
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));

                this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog("Plugins"));
                //DirectoryCatalog catalog = new DirectoryCatalog("Modules");

                OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化Plugins......");

                //catalog = new DirectoryCatalog("AddIn");
                //this.AggregateCatalog.Catalogs.Add(catalog);
                //OwnerDispater.Invoke(DispatcherPriority.Render, ShowDelegateEvent, "初始化AddIn......");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Visibility = Visibility.Hidden;
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            //这里有问题
            //
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));
            return factory;
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MainWindow>();
        }

        public override void RegisterDefaultTypesIfMissing()
        {

        }

        protected virtual RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            return null;
        }
    }
}
