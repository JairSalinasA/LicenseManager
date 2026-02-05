using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LicenseManager.Presentation.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private int _totalLicenses = 1248;
        private int _activeLicenses = 984;
        private int _expiredLicenses = 42;
        private decimal _monthlyRevenue = 24580.00m;

        public int TotalLicenses
        {
            get => _totalLicenses;
            set { _totalLicenses = value; OnPropertyChanged(); }
        }

        public int ActiveLicenses
        {
            get => _activeLicenses;
            set { _activeLicenses = value; OnPropertyChanged(); }
        }

        public int ExpiredLicenses
        {
            get => _expiredLicenses;
            set { _expiredLicenses = value; OnPropertyChanged(); }
        }

        public decimal MonthlyRevenue
        {
            get => _monthlyRevenue;
            set { _monthlyRevenue = value; OnPropertyChanged(); }
        }

        public ObservableCollection<LicenseViewModel> Licenses { get; set; }
        public ObservableCollection<ActivityViewModel> Activities { get; set; }

        public ICommand GenerateLicenseCommand { get; }
        public ICommand BulkGenerateCommand { get; }
        public ICommand ExportDataCommand { get; }
        public ICommand CheckSystemHealthCommand { get; }

        public DashboardViewModel()
        {
            Licenses = new ObservableCollection<LicenseViewModel>();
            Activities = new ObservableCollection<ActivityViewModel>();

            GenerateLicenseCommand = new RelayCommand(GenerateLicense);
            BulkGenerateCommand = new RelayCommand(BulkGenerate);
            ExportDataCommand = new RelayCommand(ExportData);
            CheckSystemHealthCommand = new RelayCommand(CheckSystemHealth);

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            // Licencias de ejemplo
            Licenses.Add(new LicenseViewModel
            {
                Id = "LIC-001",
                Customer = "TechCorp S.A.",
                Type = "Enterprise",
                ActivationDate = new DateTime(2023, 10, 15),
                ExpiryDate = new DateTime(2024, 10, 15),
                Status = LicenseStatus.Active
            });

            Licenses.Add(new LicenseViewModel
            {
                Id = "LIC-002",
                Customer = "StartUp Innovadora",
                Type = "Profesional",
                ActivationDate = new DateTime(2023, 11, 5),
                ExpiryDate = new DateTime(2024, 5, 5),
                Status = LicenseStatus.Active
            });

            Licenses.Add(new LicenseViewModel
            {
                Id = "LIC-003",
                Customer = "Universidad Nacional",
                Type = "Académica",
                ActivationDate = new DateTime(2023, 9, 20),
                ExpiryDate = new DateTime(2024, 9, 20),
                Status = LicenseStatus.Active
            });

            // Actividades de ejemplo
            Activities.Add(new ActivityViewModel
            {
                Type = ActivityType.Activation,
                Description = "Nueva licencia activada para TechCorp S.A.",
                Time = "Hace 2 horas",
                LicenseId = "LIC-001"
            });

            Activities.Add(new ActivityViewModel
            {
                Type = ActivityType.Renewal,
                Description = "Licencia renovada para Startup Innovadora",
                Time = "Hace 5 horas",
                LicenseId = "LIC-002"
            });
        }

        private void GenerateLicense()
        {
            MessageBox.Show("Funcionalidad: Generar Nueva Licencia", "Dashboard",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BulkGenerate()
        {
            MessageBox.Show("Funcionalidad: Generación Masiva", "Dashboard",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportData()
        {
            MessageBox.Show("Funcionalidad: Exportar Datos", "Dashboard",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CheckSystemHealth()
        {
            MessageBox.Show("Estado del sistema:\n\n• Servidor: ONLINE\n• Base de datos: CONECTADA\n• Rendimiento: ÓPTIMO",
                          "Salud del Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class LicenseViewModel : INotifyPropertyChanged
    {
        private string _id;
        private string _customer;
        private string _type;
        private DateTime _activationDate;
        private DateTime _expiryDate;
        private LicenseStatus _status;

        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Customer
        {
            get => _customer;
            set { _customer = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public DateTime ActivationDate
        {
            get => _activationDate;
            set { _activationDate = value; OnPropertyChanged(); }
        }

        public DateTime ExpiryDate
        {
            get => _expiryDate;
            set { _expiryDate = value; OnPropertyChanged(); }
        }

        public LicenseStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case LicenseStatus.Active:
                        return "Activa";
                    case LicenseStatus.Expired:
                        return "Expirada";
                    case LicenseStatus.Inactive:
                        return "Inactiva";
                    default:
                        return "Desconocido";
                }
            }
        }

        public Brush StatusColor
        {
            get
            {
                switch (Status)
                {
                    case LicenseStatus.Active:
                        return Brushes.Green;
                    case LicenseStatus.Expired:
                        return Brushes.Red;
                    case LicenseStatus.Inactive:
                        return Brushes.Gray;
                    default:
                        return Brushes.Black;
                }
            }
        }

        public Brush StatusBackground
        {
            get
            {
                switch (Status)
                {
                    case LicenseStatus.Active:
                        return new SolidColorBrush(Color.FromArgb(30, 76, 175, 80));
                    case LicenseStatus.Expired:
                        return new SolidColorBrush(Color.FromArgb(30, 244, 67, 54));
                    case LicenseStatus.Inactive:
                        return new SolidColorBrush(Color.FromArgb(30, 158, 158, 158));
                    default:
                        return Brushes.Transparent;
                }
            }
        }

        public ICommand ViewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RenewCommand { get; }

        public LicenseViewModel()
        {
            ViewCommand = new RelayCommand(ViewLicense);
            EditCommand = new RelayCommand(EditLicense);
            RenewCommand = new RelayCommand(RenewLicense);
        }

        private void ViewLicense()
        {
            MessageBox.Show($"Ver detalles de: {Id}\nCliente: {Customer}", "Detalles de Licencia",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditLicense()
        {
            MessageBox.Show($"Editar licencia: {Id}", "Editar Licencia",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RenewLicense()
        {
            MessageBox.Show($"Renovar licencia: {Id}", "Renovar Licencia",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ActivityViewModel : INotifyPropertyChanged
    {
        private ActivityType _type;
        private string _description;
        private string _time;
        private string _licenseId;

        public ActivityType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Time
        {
            get => _time;
            set { _time = value; OnPropertyChanged(); }
        }

        public string LicenseId
        {
            get => _licenseId;
            set { _licenseId = value; OnPropertyChanged(); }
        }

        public string Icon
        {
            get
            {
                switch (Type)
                {
                    case ActivityType.Activation:
                        return "Key";
                    case ActivityType.Renewal:
                        return "Refresh";
                    case ActivityType.Deactivation:
                        return "CloseCircle";
                    default:
                        return "Information";
                }
            }
        }

        public Brush Color
        {
            get
            {
                switch (Type)
                {
                    case ActivityType.Activation:
                        return Brushes.Green;
                    case ActivityType.Renewal:
                        return Brushes.Orange;
                    case ActivityType.Deactivation:
                        return Brushes.Red;
                    default:
                        return Brushes.Blue;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public enum LicenseStatus
    {
        Active,
        Expired,
        Inactive
    }

    public enum ActivityType
    {
        Activation,
        Renewal,
        Deactivation
    }
}