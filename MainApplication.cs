using GLib;
using Gtk;
using Application = Gtk.Application;

namespace GtkTeste
{
    internal class MainApplication : Application
    {
        private ApplicationWindow _mainWindow;

        public MainApplication() : base("org.example.application", ApplicationFlags.None) { }

        protected override void OnActivated()
        {
            BuildUi();
            SetupActions();
        }

        private void SetupActions()
        {
            var quitAction = new SimpleAction("quit", null);
            quitAction.Activated += (s, e) => _mainWindow.Close();
            AddAction(quitAction);
        }

        private void BuildUi()
        {
            const string menuTemplate = @"
      <interface>
        <menu id='app_menu'>
          <submenu>
            <attribute name='label'>_File</attribute>
            <item>
              <attribute name='label'>_Quit</attribute>
              <attribute name='action'>app.quit</attribute>
              <attribute name='accel'>&lt;Primary&gt;q</attribute>
            </item>
          </submenu>
        </menu>
      </interface>
            ";

            var menuBuilder = new Builder();
            menuBuilder.AddFromString(menuTemplate);
            var menuModel = new MenuModel(menuBuilder.GetObject("app_menu").Handle);
            
            const string windowTitle = "Gtk on Windows";
            var headerBar = new HeaderBar
            {
                Title = windowTitle,
                Subtitle = "With GtkSharp",
                ShowCloseButton = true
            };

            _mainWindow = new MainWindow(this)
            {
                Title = windowTitle,
                IconName = "applications-development",
                ShowMenubar = false
            };


            if (PrefersAppMenu())
            {
                AppMenu = menuModel;
                _mainWindow.Titlebar = headerBar;
            }
            else
            {
                Menubar = menuModel;
                _mainWindow.ShowMenubar = true;
            }
            
            _mainWindow.ShowAll();
        }
    }
}