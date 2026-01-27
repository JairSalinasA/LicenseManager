using System;
using System.Windows;
using System.Windows.Threading;

namespace LicenseManager.Presentation.Views
{
    public partial class SplashWindow : Window
    {
        private DispatcherTimer _textTimer;
        private int _dotCount = 0;

        public SplashWindow()
        {
            InitializeComponent();
            StartLoadingAnimation();
        }

        private void StartLoadingAnimation()
        {
            // Animación de puntos "Loading..."
            _textTimer = new DispatcherTimer();
            _textTimer.Interval = TimeSpan.FromMilliseconds(500);
            _textTimer.Tick += (s, e) =>
            {
                _dotCount = (_dotCount + 1) % 4;
                LoadingText.Text = "Loading" + new string('.', _dotCount);
            };
            _textTimer.Start();
        }

        public void CloseSmoothly()
        {
            // Parar animación
            _textTimer?.Stop();

            // Animación simple de desvanecimiento
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
        }
    }
}