using Gtk;

namespace GtkTeste
{
    internal class MainApplication : Application
    {
        private ApplicationWindow _mainWindow;

        public MainApplication() : base("org.example.application", GLib.ApplicationFlags.None) { }

        protected override void OnActivated()
        {
            BuildUi();
            SetupActions();
        }

        private void SetupActions()
        {
            var quitAction = new GLib.SimpleAction("quit", null);
            quitAction.Activated += (s, e) => _mainWindow.Close();
            AddAction(quitAction);
        }

        private void BuildUi()
        {
            var menuTemplate = @"
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

            AppMenu = new GLib.MenuModel(menuBuilder.GetObject("app_menu").Handle);

            var headerBar = new HeaderBar()
            {
                Title = "Gtk on Windows",
                Subtitle = "With GtkSharp",
                ShowCloseButton = true
            };

            _mainWindow = new MainWindow(this)
            {
                IconName = "applications-development",
                Titlebar = headerBar,
            };

            _mainWindow.ShowAll();
        }
    }
}