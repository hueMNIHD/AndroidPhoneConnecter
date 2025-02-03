using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PhoneConnecter
{
    public partial class Phone : Form
    {
        public Phone()
        {
            InitializeComponent();
        }

        string Power = string.Empty, VoiceUp = string.Empty, VoiceDown = string.Empty, Home = string.Empty, Back = string.Empty, History = string.Empty, Scrcpy = string.Empty;

        IntPtr ScrcpyForm = IntPtr.Zero;

        Process MainProcess = new Process();

        Process ScrcpyBackground = new Process();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hChildern, IntPtr hParent);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool rePaint);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            Power = "adb shell input keyevent 26";
            VoiceUp = "adb shell input keyevent 24";
            VoiceDown = "adb shell input keyevent 25";
            Home = "adb shell input keyevent 3";
            Back = "adb shell input keyevent 4";
            History = "adb shell input keyevent 187";
            Scrcpy = "scrcpy";
            MainProcess.StartInfo.FileName = "cmd.exe";
            MainProcess.StartInfo.RedirectStandardInput = true;
            MainProcess.StartInfo.RedirectStandardOutput = true;
            MainProcess.StartInfo.CreateNoWindow = true;
            MainProcess.Start();
            ScrcpyBackground.StartInfo.FileName = "cmd.exe";
            ScrcpyBackground.StartInfo.RedirectStandardInput = true;
            ScrcpyBackground.StartInfo.CreateNoWindow = true;
            ScrcpyBackground.Start();
            textBox1.Width = panel2.Width - 6;
            textBox1.Location = new Point(3, textBox1.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine(Power);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine(VoiceUp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine(VoiceDown);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine(Back);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine(Home);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine(History);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine("taskkill /F /IM adb.exe /T");
            ScrcpyBackground.StandardInput.WriteLine(Scrcpy);
            for (int i = 0; i <= 256; i++)
            {
                Thread.Sleep(100);
                if (i == 256)
                {
                    MessageBox.Show("超时");
                    MainProcess.StandardInput.WriteLine("taskkill /F /IM adb.exe /T");
                    break;
                }
                if (FindWindow("SDL_app", textBox1.Text) != IntPtr.Zero)
                {
                    ScrcpyForm = FindWindow("SDL_app", textBox1.Text);
                    SetParent(ScrcpyForm, panel1.Handle);
                    button7.Visible = false;
                    button9.Visible = true;
                    textBox1.Visible = false;
                    break;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MainProcess.StandardInput.WriteLine("adb reboot");
            }
            MainProcess.StandardInput.WriteLine("taskkill /F /IM adb.exe /T");
            Application.Exit();
            Environment.Exit(0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process newProcess = new Process();
            newProcess.StartInfo.FileName = "cmd.exe";
            newProcess.StartInfo.RedirectStandardInput = true;
            newProcess.StartInfo.CreateNoWindow = true;
            newProcess.Start();
            newProcess.StandardInput.WriteLine(Scrcpy);
            for (int i = 0; i <= 256; i++)
            {
                Thread.Sleep(100);
                if (i == 256)
                {
                    MessageBox.Show("超时");
                    MainProcess.StandardInput.WriteLine("taskkill /F /IM adb.exe /T");
                    break;
                }
                if (FindWindow("SDL_app", textBox1.Text) != IntPtr.Zero)
                {
                    ScrcpyForm = FindWindow("SDL_app", textBox1.Text);
                    SetParent(ScrcpyForm, panel1.Handle);
                    break;
                }
            }
        }

        private void tableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            textBox1.Width = panel2.Width - 6;
            textBox1.Location = new Point(3, textBox1.Location.Y);
        }

        private void Phone_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainProcess.StandardInput.WriteLine("taskkill /F /IM adb.exe /T");
            Application.Exit();
            Environment.Exit(0);
        }

        bool HasMaxed = false;

        private void button10_Click(object sender, EventArgs e)
        {
            if (HasMaxed)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                HasMaxed = false;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                HasMaxed = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine("adb shell settings put system user_rotation 0");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine("adb shell settings put system user_rotation 1");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine("adb shell settings put system user_rotation 2");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MainProcess.StandardInput.WriteLine("adb shell settings put system user_rotation 3");
        }
    }
}
