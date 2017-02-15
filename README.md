# architecture-wpf-mef
使用Prism.Wpf和Prism.Mef组件构成的Windows桌面应用程序

#### 1、创建新的解决方案
#### 2、添加WPF Project
#### 3、添加Plugin Project，并修改其应用程序属性OutputType为：ClassLibrary
     设置：1：Build Events -> Post-build event command line ：（该命令创建Plugins文件到主项目SHELL的bin\debug\目录下）
		if not exist "$(SolutionDir)XIAOWEN.SHELL\bin\Debug\Plugins" mkdir "$(SolutionDir)XIAOWEN.SHELL\bin\Debug\Plugins" copy "$(TargetPath)" "$(SolutionDir)XIAOWEN.SHELL\bin\Debug\Plugins"
		2：然后修改Post-build event command line为：
		xcopy /Y /I "$(TargetPath)" "$(SolutionDir)XIAOWEN.SHELL\bin\Debug\Plugins"
#### 4、生成的Plugins\目录及项目*.dll文件
#### 5、添加Log4Net，并进行配置及填写通用Logger类，该Log数据操作，防止在项目DATA项目中，目的是将对数据的操作工能均置于该项目下，使得项目结构清晰易读
#### 6、添加Prism和Mef组件，该组件可通过NuGet获取，NeGet会自动下载所有依赖组件
#### 7、在运行Release版本的时候，我们想要做更多的事情，包括使用mef下的MefBootstrapper，
#### 8、添加INFRASTRUCTURE Project，并为其引用Prism.Wpf
