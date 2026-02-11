using System;
using System.Threading.Tasks;
using System.Windows;
using LicenseManager.Presentation.Views;

namespace LicenseManager
{
    public partial class App : Application
    {
        private static bool _isDarkTheme = false;

        public static bool IsDarkTheme => _isDarkTheme;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Cargar preferencia de tema guardada
            LoadThemePreference();

            // Mostrar splash screen
            SplashScreenManager.Show("License Manager", "Iniciando aplicación...");

            Task.Run(async () => await InitializeApplicationAsync());
        }

        private async Task InitializeApplicationAsync()
        {
            try
            {
                await Task.Delay(500);
                SplashScreenManager.UpdateStatus("Cargando configuración...");
                await Task.Delay(700);
                SplashScreenManager.UpdateStatus("Inicializando módulos...");
                await Task.Delay(600);
                SplashScreenManager.UpdateStatus("Conectando con base de datos...");
                await Task.Delay(800);
                SplashScreenManager.UpdateStatus("Preparando interfaz...");
                await Task.Delay(400);
                SplashScreenManager.UpdateStatus("¡Casi listo!");
                await Task.Delay(300);

                SplashScreenManager.Close();
                await Task.Delay(500);

                await Dispatcher.InvokeAsync(ShowMainWindow);
            }
            catch (Exception ex)
            {
                await Dispatcher.InvokeAsync(() =>
                    MessageBox.Show($"Error: {ex.Message}"));
            }
        }

        private void ShowMainWindow()
        {
            var mainWindow = new MainWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            mainWindow.Show();
            mainWindow.Activate();
            mainWindow.Focus();
        }

        #region Theme Management

        private void LoadThemePreference()
        {
            try
            {
                _isDarkTheme = LicenseManager.Properties.Settings.Default.IsDarkTheme;
                ApplyTheme(_isDarkTheme);
            }
            catch
            {
                _isDarkTheme = false;
            }
        }

        public static void ToggleTheme()
        {
            _isDarkTheme = !_isDarkTheme;
            ApplyTheme(_isDarkTheme);

            // Guardar preferencia
            LicenseManager.Properties.Settings.Default.IsDarkTheme = _isDarkTheme;
            LicenseManager.Properties.Settings.Default.Save();
        }

        private static void ApplyTheme(bool isDark)
        {
            try
            {
                var app = Current;

                // Limpiar diccionarios actuales
                app.Resources.MergedDictionaries.Clear();

                // 1. Cargar el tema correspondiente
                if (isDark)
                {
                    app.Resources.MergedDictionaries.Add(
                        new ResourceDictionary
                        {
                            Source = new Uri("pack://application:,,,/LicenseManager;component/Resources/Styles/DarkTheme.xaml", UriKind.Absolute)
                        }
                    );
                }
                else
                {
                    app.Resources.MergedDictionaries.Add(
                        new ResourceDictionary
                        {
                            Source = new Uri("pack://application:,,,/LicenseManager;component/Resources/Styles/Colors.xaml", UriKind.Absolute)
                        }
                    );
                }

                // 2. Cargar los estilos comunes
                app.Resources.MergedDictionaries.Add(
                    new ResourceDictionary
                    {
                        Source = new Uri("pack://application:,,,/LicenseManager;component/Resources/Styles/AppStyles.xaml", UriKind.Absolute)
                    }
                );

                // 3. Actualizar todas las ventanas
                ForceThemeUpdate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al aplicar tema: {ex.Message}");
            }
        }

        private static void ForceThemeUpdate()
        {
            foreach (Window window in Current.Windows)
            {
                if (window is MainWindow mainWindow)
                {
                    mainWindow.OnThemeChanged();
                }
            }
        }

        #endregion
    }
}