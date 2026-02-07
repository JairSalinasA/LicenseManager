using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LicenseManager.Presentation.Views
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<LicenseViewModel> _allLicenses;
        private ObservableCollection<LicenseViewModel> _filteredLicenses;
        private bool _isDarkTheme = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            LoadTheme();
        }

        private void InitializeData()
        {
            // Crear datos de ejemplo para las licencias
            _allLicenses = new ObservableCollection<LicenseViewModel>
            {
                new LicenseViewModel
                {
                    LicenseId = "LIC-8923-AB7F",
                    Client = "Innovate Corp",
                    Product = "DesignPro Suite",
                    ExpirationDate = DateTime.Now.AddDays(45),
                    Status = "Activa",
                    StatusColor = new SolidColorBrush(Color.FromRgb(16, 185, 129)), // Green
                    DaysRemaining = "+45 días",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(59, 130, 246)) // Blue
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-5678-CD9E",
                    Client = "DataSystems LLC",
                    Product = "AnalyticsMaster",
                    ExpirationDate = DateTime.Now.AddDays(-25),
                    Status = "Expirada",
                    StatusColor = new SolidColorBrush(Color.FromRgb(239, 68, 68)), // Red
                    DaysRemaining = "Vencida",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(239, 68, 68))
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-3456-EF2G",
                    Client = "CloudTech Inc.",
                    Product = "SecuritySoft v3.2",
                    ExpirationDate = DateTime.Now.AddDays(5),
                    Status = "Por vencer",
                    StatusColor = new SolidColorBrush(Color.FromRgb(245, 158, 11)), // Orange
                    DaysRemaining = "5 días",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(245, 158, 11))
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-9012-GH8I",
                    Client = "MobileFirst Solutions",
                    Product = "AppBuilder Pro",
                    ExpirationDate = DateTime.Now.AddDays(-15),
                    Status = "Expirada",
                    StatusColor = new SolidColorBrush(Color.FromRgb(239, 68, 68)),
                    DaysRemaining = "Vencida",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(239, 68, 68))
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-6789-IJ3K",
                    Client = "WebCraft Studios",
                    Product = "DesignPro Suite",
                    ExpirationDate = DateTime.Now.AddDays(12),
                    Status = "Por vencer",
                    StatusColor = new SolidColorBrush(Color.FromRgb(245, 158, 11)),
                    DaysRemaining = "12 días",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(245, 158, 11))
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-2345-KL7M",
                    Client = "TechNova Solutions",
                    Product = "DataAnalyzer Pro",
                    ExpirationDate = DateTime.Now.AddDays(76),
                    Status = "Activa",
                    StatusColor = new SolidColorBrush(Color.FromRgb(16, 185, 129)),
                    DaysRemaining = "+76 días",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(59, 130, 246))
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-7890-MN2P",
                    Client = "Global Systems Inc.",
                    Product = "Enterprise Manager",
                    ExpirationDate = DateTime.Now.AddDays(3),
                    Status = "Por vencer",
                    StatusColor = new SolidColorBrush(Color.FromRgb(245, 158, 11)),
                    DaysRemaining = "3 días",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(245, 158, 11))
                },
                new LicenseViewModel
                {
                    LicenseId = "LIC-4567-OP9Q",
                    Client = "Digital Dynamics",
                    Product = "CloudBackup Suite",
                    ExpirationDate = DateTime.Now.AddDays(119),
                    Status = "Activa",
                    StatusColor = new SolidColorBrush(Color.FromRgb(16, 185, 129)),
                    DaysRemaining = "+119 días",
                    DaysRemainingColor = new SolidColorBrush(Color.FromRgb(59, 130, 246))
                }
            };

            _filteredLicenses = new ObservableCollection<LicenseViewModel>(_allLicenses);
            LicensesDataGrid.ItemsSource = _filteredLicenses;
        }

        #region Theme Management

        private void LoadTheme()
        {
            // Cargar tema guardado o usar tema claro por defecto
            _isDarkTheme = false;
            ApplyTheme();
        }

        private void ToggleTheme(object sender, RoutedEventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (_isDarkTheme)
            {
                // Aplicar tema oscuro
                ThemeIcon.Text = "☀️";
                // Aquí puedes agregar más cambios de tema
                // Por ejemplo, cambiar colores de recursos
            }
            else
            {
                // Aplicar tema claro
                ThemeIcon.Text = "🌙";
            }
        }

        #endregion

        #region Navigation

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navegando al Dashboard", "Navegación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NavigateToLicenses(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navegando a Licencias", "Navegación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NavigateToProducts(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navegando a Productos", "Navegación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NavigateToClients(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navegando a Clientes", "Navegación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NavigateToSettings(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navegando a Configuración", "Navegación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Estás seguro de que quieres cerrar sesión?",
                                        "Cerrar Sesión",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Sesión cerrada", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);
                // Aquí puedes agregar la lógica para cerrar la aplicación o volver al login
            }
        }

        #endregion

        #region UI Interactions

        private void ToggleSidebar(object sender, RoutedEventArgs e)
        {
            // Implementar lógica para ocultar/mostrar sidebar
            MessageBox.Show("Toggle Sidebar", "UI", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ToggleUserMenu(object sender, RoutedEventArgs e)
        {
            // Implementar menú desplegable de usuario
            MessageBox.Show("Menú de usuario", "UI", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowNotifications(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tienes 3 notificaciones sin leer:\n\n" +
                          "1. Nueva licencia generada\n" +
                          "2. Licencia próxima a vencer\n" +
                          "3. Cliente actualizado",
                          "Notificaciones",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        private void ContactSupport(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Contactando soporte...\n\nEmail: soporte@licensepro.com\nTeléfono: +1 234 567 890",
                          "Soporte",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        #endregion

        #region License Management

        private void GenerateNewLicense(object sender, RoutedEventArgs e)
        {
            // Generar una nueva licencia
            var random = new Random();
            var newLicense = new LicenseViewModel
            {
                LicenseId = $"LIC-{random.Next(1000, 9999)}-NEW",
                Client = "Nuevo Cliente",
                Product = "Producto Demo",
                ExpirationDate = DateTime.Now.AddDays(365),
                Status = "Activa",
                StatusColor = new SolidColorBrush(Color.FromRgb(16, 185, 129)),
                DaysRemaining = "+365 días",
                DaysRemainingColor = new SolidColorBrush(Color.FromRgb(59, 130, 246))
            };

            _allLicenses.Insert(0, newLicense);
            _filteredLicenses.Insert(0, newLicense);

            MessageBox.Show($"Nueva licencia generada: {newLicense.LicenseId}",
                          "Licencia Creada",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        private void ViewLicenseDetails(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var license = (button?.DataContext as LicenseViewModel);

            if (license != null)
            {
                MessageBox.Show($"Detalles de la Licencia:\n\n" +
                              $"ID: {license.LicenseId}\n" +
                              $"Cliente: {license.Client}\n" +
                              $"Producto: {license.Product}\n" +
                              $"Expira: {license.ExpirationDate:dd/MM/yyyy}\n" +
                              $"Estado: {license.Status}",
                              "Detalles de Licencia",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private void EditLicense(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var license = (button?.DataContext as LicenseViewModel);

            if (license != null)
            {
                MessageBox.Show($"Editando licencia: {license.LicenseId}",
                              "Editar Licencia",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private void RenewLicense(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var license = (button?.DataContext as LicenseViewModel);

            if (license != null)
            {
                var result = MessageBox.Show($"¿Renovar la licencia {license.LicenseId} por 365 días?",
                                           "Renovar Licencia",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    license.ExpirationDate = DateTime.Now.AddDays(365);
                    license.Status = "Activa";
                    license.StatusColor = new SolidColorBrush(Color.FromRgb(16, 185, 129));
                    license.DaysRemaining = "+365 días";
                    license.DaysRemainingColor = new SolidColorBrush(Color.FromRgb(59, 130, 246));

                    LicensesDataGrid.Items.Refresh();

                    MessageBox.Show("Licencia renovada exitosamente",
                                  "Éxito",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
            }
        }

        private void ViewAllLicenses(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mostrando todas las licencias en vista detallada",
                          "Ver Todas",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        #endregion

        #region Filtering

        private void FilterStatus_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_allLicenses == null) return;

            var selectedIndex = FilterStatusCombo.SelectedIndex;

            _filteredLicenses.Clear();

            IEnumerable<LicenseViewModel> filtered = _allLicenses;

            switch (selectedIndex)
            {
                case 0: // Todas
                    filtered = _allLicenses;
                    break;
                case 1: // Solo activas
                    filtered = _allLicenses.Where(l => l.Status == "Activa");
                    break;
                case 2: // Solo vencidas
                    filtered = _allLicenses.Where(l => l.Status == "Expirada");
                    break;
                case 3: // Por vencer
                    filtered = _allLicenses.Where(l => l.Status == "Por vencer");
                    break;
            }

            foreach (var license in filtered)
            {
                _filteredLicenses.Add(license);
            }
        }

        #endregion
    }

    #region ViewModel Classes

    public class LicenseViewModel
    {
        public string LicenseId { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DaysRemaining { get; set; }
        public string Status { get; set; }
        public SolidColorBrush StatusColor { get; set; }
        public SolidColorBrush DaysRemainingColor { get; set; }
    }

    #endregion
}
