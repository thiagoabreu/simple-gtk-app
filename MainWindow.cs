using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GtkTeste
{
    internal class MainWindow : ApplicationWindow
    {
        [UI] private readonly Button _button1 = null;
        [UI] private readonly Label _label1 = null;

        private int _counter;

        public MainWindow(Application app) : this(app, new Builder("MainWindow.glade")) { }

        private MainWindow(Application app, Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            Application = app;
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _button1.Clicked += Button1_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            ((GLib.Application) Application).Quit();
        }

        private void Button1_Clicked(object sender, EventArgs a)
        {
            _label1.Text = $"Hello World! This button has been clicked {++_counter} time(s).";
        }
    }
}