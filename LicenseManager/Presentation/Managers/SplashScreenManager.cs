using System;
using System.Windows;
using System.Windows.Threading;

namespace LicenseManager.Presentation.Views
{
    public static class SplashScreenManager
    {
        private static SplashWindow _splashWindow;

        public static void Show(string title = "License Manager",
                               string status = "Starting...")
        {
            // Si ya está mostrado, no hacer nada
            if (_splashWindow != null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _splashWindow = new SplashWindow();
                _splashWindow.UpdateStatus(status);
                _splashWindow.Show();
            });
        }

        public static void UpdateStatus(string status)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _splashWindow?.UpdateStatus(status);
            });
        }

        // Método UpdateProgress mantenido por compatibilidad pero no hace nada
        // Los puntos animados se manejan automáticamente en XAML
        public static void UpdateProgress(double progress)
        {
            // Ya no es necesario - la animación de puntos es automática
            // Mantenemos el método para evitar errores de compilación
        }

        public static void Close()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_splashWindow != null)
                {
                    // Desactivar Topmost primero para que la MainWindow pueda aparecer encima
                    _splashWindow.Topmost = false;

                    // Iniciar animación de cierre
                    _splashWindow.CloseSmoothly();

                    _splashWindow = null;
                }
            });
        }
    }
}