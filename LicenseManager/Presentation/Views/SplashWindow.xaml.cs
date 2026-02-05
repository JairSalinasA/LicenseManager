using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace LicenseManager.Presentation.Views
{
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            this.Loaded += SplashWindow_Loaded;
        }

        private void SplashWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Aplicar el efecto blur de Windows
            EnableBlur();

            // Iniciar animaciones de entrada
            StartEntranceAnimations();
        }

        private void StartEntranceAnimations()
        {
            // Animación del logo (desde la izquierda)
            var logoStoryboard = (Storyboard)this.Resources["SlideInFromLeft"];
            Storyboard.SetTarget(logoStoryboard, LogoElement);
            logoStoryboard.Begin();

            // Animación del status text (desde la izquierda)
            var statusStoryboard = logoStoryboard.Clone();
            Storyboard.SetTarget(statusStoryboard, StatusText);
            statusStoryboard.Begin();

            // Animación del copyright (desde la derecha)
            var copyrightStoryboard = (Storyboard)this.Resources["SlideInFromRight"];
            Storyboard.SetTarget(copyrightStoryboard, CopyrightText);
            copyrightStoryboard.Begin();

            // Animación del centro (fade in)
            var centerStoryboard = (Storyboard)this.Resources["FadeInCenter"];
            Storyboard.SetTarget(centerStoryboard, CenterElement);
            centerStoryboard.Begin();
        }

        private void EnableBlur()
        {
            try
            {
                var windowHelper = new WindowInteropHelper(this);

                var accent = new AccentPolicy
                {
                    AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
                };

                var accentStructSize = Marshal.SizeOf(accent);
                var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                Marshal.StructureToPtr(accent, accentPtr, false);

                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                    SizeOfData = accentStructSize,
                    Data = accentPtr
                };

                SetWindowCompositionAttribute(windowHelper.Handle, ref data);
                Marshal.FreeHGlobal(accentPtr);
            }
            catch
            {
                // Si falla el blur, la ventana se verá bien de todas formas
            }
        }

        public void UpdateStatus(string status)
        {
            Dispatcher.Invoke(() =>
            {
                if (StatusText != null)
                    StatusText.Text = status;
            });
        }

        public void CloseSmoothly()
        {
            Dispatcher.Invoke(() =>
            {
                // Desactivar Topmost ANTES de cerrar
                this.Topmost = false;

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(20);
                double opacity = 1.0;

                timer.Tick += (s, e) =>
                {
                    opacity -= 0.05;
                    this.Opacity = opacity;

                    if (opacity <= 0)
                    {
                        timer.Stop();
                        Close();
                    }
                };
                timer.Start();
            });
        }

        #region Windows API para Blur

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
        }

        internal enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        #endregion
    }
}