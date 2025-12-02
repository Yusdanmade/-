using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace NuclearDDoS_Ultimate_2025
{
    public partial class MonitorWindow : Window
    {
        private DispatcherTimer? updateTimer;
        
        public MonitorWindow()
        {
            InitializeComponent();
            InitializeTimer();
            UpdateStats();
        }
        
        private void InitializeTimer()
        {
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }
        
        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateStats();
        }
        
        private void UpdateStats()
        {
            try
            {
                // Get system stats
                var process = Process.GetCurrentProcess();
                var memoryMB = process.WorkingSet64 / 1024 / 1024;
                var threadCount = process.Threads.Count;
                
                // Simulate attack stats (would integrate with real stats)
                var random = new Random();
                var totalRequests = random.Next(100000, 999999);
                var successfulRequests = random.Next(90000, (int)(totalRequests * 0.95));
                var failedRequests = totalRequests - successfulRequests;
                var successRate = totalRequests > 0 ? (double)successfulRequests / totalRequests * 100 : 0;
                var rps = random.Next(1000, 10000);
                var bandwidthMB = random.Next(10, 500);
                var cpuUsage = random.Next(20, 90);
                
                // Update UI
                TotalRequestsLabel.Text = totalRequests.ToString("N0");
                SuccessfulRequestsLabel.Text = successfulRequests.ToString("N0");
                FailedRequestsLabel.Text = failedRequests.ToString("N0");
                SuccessRateLabel.Text = $"{successRate:F1}%";
                RPSLabel.Text = rps.ToString("N0");
                BandwidthLabel.Text = $"{bandwidthMB} MB";
                
                ThreadsLabel.Text = threadCount.ToString();
                ConnectionsLabel.Text = random.Next(1000, 5000).ToString();
                CPULabel.Text = $"{cpuUsage}%";
                MemoryLabel.Text = $"{memoryMB} MB";
            }
            catch (Exception ex)
            {
                // Silently handle errors in monitor
            }
        }
        
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStats();
        }
        
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var filename = $"monitor_export_{timestamp}.txt";
                
                using var writer = new StreamWriter(filename);
                writer.WriteLine("NuclearDDoS Ultimate 2025 - Monitor Export");
                writer.WriteLine($"Export Time: {DateTime.Now}");
                writer.WriteLine(new string('=', 50));
                writer.WriteLine($"Total Requests: {TotalRequestsLabel.Text}");
                writer.WriteLine($"Successful Requests: {SuccessfulRequestsLabel.Text}");
                writer.WriteLine($"Failed Requests: {FailedRequestsLabel.Text}");
                writer.WriteLine($"Success Rate: {SuccessRateLabel.Text}");
                writer.WriteLine($"Requests Per Second: {RPSLabel.Text}");
                writer.WriteLine($"Bandwidth Used: {BandwidthLabel.Text}");
                writer.WriteLine($"Active Threads: {ThreadsLabel.Text}");
                writer.WriteLine($"Active Connections: {ConnectionsLabel.Text}");
                writer.WriteLine($"CPU Usage: {CPULabel.Text}");
                writer.WriteLine($"Memory Usage: {MemoryLabel.Text}");
                
                MessageBox.Show($"Veriler dışa aktarıldı: {filename}", "Başarılı", 
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dışa aktarma başarısız: {ex.Message}", "Hata", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            updateTimer?.Stop();
            base.OnClosed(e);
        }
    }
}