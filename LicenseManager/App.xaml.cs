using System;
using System.Windows;
using System.Windows.Threading;
using LicenseManager.Presentation.Views;

namespace LicenseManager
{
    public partial class App : Application
    {
        private SplashWindow _splash;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Mostrar Splash Screen
            _splash = new SplashWindow();
            _splash.Show();

            // Inicializar aplicación en segundo plano
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            // Usar Timer para simular carga
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3); // 3 segundos de splash
            timer.Tick += (sender, args) =>
            {
                timer.Stop();

                // Cerrar Splash suavemente
                _splash.CloseSmoothly();

                // Dar tiempo a que se cierre
                var closeTimer = new DispatcherTimer();
                closeTimer.Interval = TimeSpan.FromMilliseconds(600);
                closeTimer.Tick += (s, e) =>
                {
                    closeTimer.Stop();
                    ShowMainWindow();
                };
                closeTimer.Start();
            };
            timer.Start();
        }

        private void ShowMainWindow()
        {
            // Mostrar ventana principal
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Opcional: centrar ventana
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}