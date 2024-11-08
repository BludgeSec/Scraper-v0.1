using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scraper_v0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Ensure InitializeComponent() is called
            InitializeListView(); // Initialize ListView columns

            // Get the local IP and display it in the Label
            string localIP = GetLocalIP();
            locIp.Text = "Local IP: " + localIP; // Display the local IP address
        }

        // Button click event to start the ping sweep
        private async void button1_Click(object sender, EventArgs e)
        {
            // Clear previous results
            networkLst.Items.Clear();

            // Get the local IP address for ping sweep
            string localIP = GetLocalIP();
            if (string.IsNullOrEmpty(localIP))
            {
                MessageBox.Show("Unable to retrieve local IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Perform the ping sweep
            await PerformPingSweep(localIP);
        }

        // Perform an asynchronous ping sweep
        private async Task PerformPingSweep(string baseIP)
        {
            // Extract the subnet from the base IP (e.g., "192.168.1.")
            string subnet = baseIP.Substring(0, baseIP.LastIndexOf('.') + 1);
            List<Task> pingTasks = new List<Task>();

            // Ping all addresses in the subnet (e.g., 192.168.1.1 to 192.168.1.254)
            for (int i = 1; i <= 254; i++)
            {
                string ipAddress = subnet + i;
                pingTasks.Add(PingAsync(ipAddress));
            }

            // Wait for all pings to complete
            await Task.WhenAll(pingTasks);
        }

        // Asynchronous ping method
        private async Task PingAsync(string ipAddress)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(ipAddress, 1000); // 1000ms timeout
                    if (reply.Status == IPStatus.Success)
                    {
                        string hostName = GetHostName(ipAddress);
                        long responseTime = reply.RoundtripTime;
                        string os = GetOperatingSystem(reply);

                        // Update the ListView with the active IP address
                        this.Invoke(new Action(() =>
                        {
                            ListViewItem item = new ListViewItem(ipAddress);
                            item.SubItems.Add(hostName);
                            item.SubItems.Add(os); // Add OS info
                            item.SubItems.Add(responseTime.ToString() + " ms");
                            networkLst.Items.Add(item);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pinging {ipAddress}: {ex.Message}");
            }
        }

        // Get the hostname of a given IP address
        private string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
                return hostEntry.HostName;
            }
            catch (SocketException)
            {
                return "Unknown";
            }
        }

        // Determine OS based on TTL value
        private string GetOperatingSystem(PingReply reply)
        {
            try
            {
                // Start with TTL-based OS detection
                int ttl = reply.Options.Ttl;
                string os = "Unknown";

                // Some typical TTL values associated with certain OSs
                if (ttl <= 64)
                {
                    os = "Linux/Unix";  // Linux/Unix usually have TTL of 64
                }
                else if (ttl == 128)
                {
                    os = "Windows";     // Windows usually have TTL of 128
                }
                else if (ttl == 255)
                {
                    os = "MacOS";       // MacOS usually has TTL of 255
                }

                return os;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        // Initialize ListView columns
        private void InitializeListView()
        {
            networkLst.View = View.Details;
            networkLst.Columns.Add("IP Address", 100);
            networkLst.Columns.Add("Hostname", 180);
            networkLst.Columns.Add("Operating System", 180); // New column for OS
            networkLst.Columns.Add("Response Time (ms)", 120);
        }

        // Get the local IP address of the machine
        private string GetLocalIP()
        {
            string localIP = string.Empty;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        // Check for IPv4 addresses (ignore IPv6)
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIP = ip.Address.ToString();
                            break;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(localIP))
                    break;
            }
            return localIP;
        }
    }
}
