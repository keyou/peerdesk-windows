using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace PeerDesk
{
    public partial class MainWindow : Form
    {
        private const String host_ = "127.0.0.1";
        private const int port_ = 13334;
        private const int screen_width_ = 1920;
        private const int screen_height_ = 1080;
        private Channel channel_ = new Channel();
        private Dictionary<string, object> cmd_ = new Dictionary<string,object>();
        private string tmp_file_ = Path.Combine(Path.GetTempPath(), "peerdesk.txt");

        public MainWindow()
        {
            InitializeComponent();
            ClientSize = new Size(screen_width_ / 2, screen_height_ / 2);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Debug.WriteLine("handle= {0}", Handle);
            BeginInvoke(new Action(() =>
            {
                if(File.Exists(tmp_file_))
                {
                    using (var reader = new StreamReader(tmp_file_))
                    {
                        textIp.Text = reader.ReadLine();
                        textIp.SelectionStart = textIp.Text.Length;
                        textIp.SelectionLength = 0;
                    }
                }
            }));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            cmd_.Clear();
            cmd_.Add("type", "button");
            cmd_.Add("x", 1.0 * e.X / ClientSize.Width * screen_width_);
            cmd_.Add("y", 1.0 * e.Y / ClientSize.Height * screen_height_);
            cmd_.Add("button", e.Button == MouseButtons.Left ? "left" : e.Button == MouseButtons.Right ? "right" : "middle");
            cmd_.Add("state", "down");
            var data = channel_.SendCommand(cmd_);
            Debug.Write(data);
            this.ActiveControl = null;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            cmd_.Clear();
            cmd_.Add("type", "button");
            cmd_.Add("x", 1.0 * e.X / ClientSize.Width * screen_width_);
            cmd_.Add("y", 1.0 * e.Y / ClientSize.Height * screen_height_);
            cmd_.Add("button", e.Button == MouseButtons.Left?"left":e.Button == MouseButtons.Right?"right":"middle");
            cmd_.Add("state", "up");
            var data = channel_.SendCommand(cmd_);
            Debug.Write(data);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            cmd_.Clear();
            cmd_.Add("type", "move");
            cmd_.Add("x", 1.0 * e.X / ClientSize.Width * screen_width_);
            cmd_.Add("y", 1.0 * e.Y / ClientSize.Height * screen_height_);
            var data = channel_.SendCommand(cmd_);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            cmd_.Clear();
            cmd_.Add("type", "scroll");
            cmd_.Add("x", 0);
            cmd_.Add("y", e.Delta < 0 ? -5 : 5);
            var data = channel_.SendCommand(cmd_);
        }

        private static Dictionary<string, string> key_maps_ = new Dictionary<string, string>() {
            {"\r","enter"},
            {"\t","tab"},
            {"menu","alt"},
            {"\b","backspace"},
            {"next","pagedown"},
            {"lwin","command"},
            {"rwin","command"},
        };

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var key = KeyCodeToUnicode(e.KeyCode);
            if(key == string.Empty) key = e.KeyCode.ToString().ToLower();
            if(key_maps_.ContainsKey(key)) key = key_maps_[key];
            if (key.EndsWith("key")) key = key.Replace("key", string.Empty);
            if (key.StartsWith("numpad")) key = key.Replace("numpad", string.Empty);
            if (key.StartsWith("oem")) key = key.Replace("oem", string.Empty);
            if(Regex.IsMatch(key,"^d\\d+")) key = key.TrimStart('d');
            cmd_.Clear();
            cmd_.Add("type", "keyboard");
            cmd_.Add("key",  key);
            cmd_.Add("state", "down");
            //cmd_.Add("modifier", 1.0 * e.Y / ClientSize.Height * screen_height_);
            var data = channel_.SendCommand(cmd_);
            Debug.Write(data);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            var key = KeyCodeToUnicode(e.KeyCode);
            if (key == string.Empty) key = e.KeyCode.ToString().ToLower();
            if (key_maps_.ContainsKey(key)) key = key_maps_[key];
            if (key.EndsWith("key")) key = key.Replace("key", string.Empty);
            if (key.StartsWith("numpad")) key = key.Replace("numpad", string.Empty);
            if (key.StartsWith("oem")) key = key.Replace("oem", string.Empty);
            if (Regex.IsMatch(key, "^d\\d+")) key = key.TrimStart('d');
            cmd_.Clear();
            cmd_.Add("type", "keyboard");
            cmd_.Add("key", key);
            cmd_.Add("state", "up");
            //cmd_.Add("modifier", 1.0 * e.Y / ClientSize.Height * screen_height_);
            var data = channel_.SendCommand(cmd_);
            Debug.Write(data);
        }
        
        private void textIp_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                connectBtn_Click(sender, e);
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("ip: " + textIp.Text);
                Text = "PeerDesk: Connecting...";
                if(!channel_.Connect(textIp.Text, port_))
                {
                    throw new Exception("Connect failed");
                }
                var process = Process.Start("mpv.exe",
                    string.Format("--wid={0} --autofit-larger={1} tcp://{2}:13333 --no-keepaspect --no-keepaspect-window " +
                    "--profile=low-latency --video-latency-hacks=yes --no-config --input-vo-keyboard=no --no-input-cursor --idle=yes " +
                    "--osc=no --no-cache --untimed --no-correct-pts --fps=30 ", Handle, 192 * 7, host_));
                Text = string.Format("PeerDesk: tcp://{1}:{2} ({0})", Handle, host_, port_);
                Debug.WriteLine("mpv args: " + process.StartInfo.Arguments);
                textIp.ReadOnly = true;
                textIp.Enabled = false;
                this.ActiveControl = null;
                connectBtn.Enabled = false;
                titleLayoutPanel.Enabled = false;

                using (StreamWriter sw = new StreamWriter(tmp_file_))
                {
                    sw.WriteLine(textIp.Text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Text = "PeerDesk: Connect failed";
            }
        }

        #region KeyCodeToUnicode
        public string KeyCodeToUnicode(Keys key)
        {
            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus = GetKeyboardState(keyboardState);

            if (!keyboardStateStatus)
            {
                return "";
            }
            // 禁止 shift
            keyboardState[(int)Keys.ShiftKey] = 0;

            uint virtualKeyCode = (uint)key;
            uint scanCode = MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);

            StringBuilder result = new StringBuilder();
            ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, (int)5, (uint)0, inputLocaleIdentifier);

            return result.ToString();
        } 

        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        #endregion


    }

    class Channel
    {
        private TcpClient tcp_client_ = new TcpClient();

        public Channel()
        {
            tcp_client_.NoDelay = true;
            tcp_client_.SendTimeout = 5000;
            tcp_client_.ReceiveTimeout = 5000;
        }

        public bool Connect(string host,int port)
        {
            if(!tcp_client_.ConnectAsync(host, port).Wait(5000))
            {
                tcp_client_.Close();
                tcp_client_ = new TcpClient();
                return false;
            }
            return true;
        }

        public string SendCommand(Dictionary<string,object> cmd)
        {
            string data = "{";
            foreach (var item in cmd)
	        {
                if (item.Value is string)
                    data += string.Format("\"{0}\":\"{1}\",", item.Key, item.Value);
                else if (item.Value is int || item.Value is double)
                    data += string.Format("\"{0}\":{1},", item.Key, item.Value);
                else if (item.Value is bool)
                    data += string.Format("\"{0}\":{1},", item.Key, (bool)item.Value == true ? "true" : "false");
                else Debug.WriteLine("receive illegal data: {0}",item.Value);
	        }
            if (data != "{") data = data.Substring(0, data.Length - 1);
            data += "}\n";

            try
            {
                if (tcp_client_.Connected)
                    tcp_client_.GetStream().Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return data;
        }

    }
}
