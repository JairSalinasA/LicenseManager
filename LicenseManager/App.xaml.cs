using System;
using System.Threading.Tasks;
using System.Windows;
using LicenseManager.Presentation.Views;

namespace LicenseManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Mostrar splash screen INMEDIATAMENTE
            SplashScreenManager.Show("License Manager", "Iniciando aplicación...");

            // Inicializar en segundo plano
            Task.Run(async () =>
            {
                await InitializeApplicationAsync();
            });
        }

        private async Task InitializeApplicationAsync()
        {
            try
            {
                // Etapa 1
                await Task.Delay(500);
                SplashScreenManager.UpdateStatus("Cargando configuración...");

                // Etapa 2
                await Task.Delay(700);
                SplashScreenManager.UpdateStatus("Inicializando módulos...");

                // Etapa 3
                await Task.Delay(600);
                SplashScreenManager.UpdateStatus("Conectando con base de datos...");

                // Etapa 4
                await Task.Delay(800);
                SplashScreenManager.UpdateStatus("Preparando interfaz...");

                // Etapa 5
                await Task.Delay(400);
                SplashScreenManager.UpdateStatus("¡Casi listo!");

                // Dar un momento final
                await Task.Delay(300);

                // Cerrar splash screen y esperar a que termine la animación
                SplashScreenManager.Close();

                // IMPORTANTE: Esperar a que el splash se cierre completamente
                await Task.Delay(500); // Tiempo de la animación de cierre

                // Mostrar ventana principal en el thread de UI
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ShowMainWindow();
                });
            }
            catch (Exception ex)
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Error: {ex.Message}");
                });
            }
        }

        private void ShowMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            mainWindow.Activate(); // Forzar que la ventana tome el foco
            mainWindow.Focus();
        }
    }
}