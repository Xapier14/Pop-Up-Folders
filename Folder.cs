using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using File = System.IO.File;

namespace Pop_Up_Folders
{
    public static class UACChecker
    {
        private const int ERROR_ELEVATION_REQUIRED = 740;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CreateProcess(string lpApplicationName,
           string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
           ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles,
           CreationFlags dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
           [In] ref STARTUPINFO lpStartupInfo,
           out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [Flags]
        private enum CreationFlags : uint
        {
            CREATE_SUSPENDED = 0x4
        }

        public static bool RequiresElevation(string Filename)
        {
            bool requiresElevation;
            bool success;
            int win32error;

            PROCESS_INFORMATION pInfo = new PROCESS_INFORMATION();
            STARTUPINFO sInfo = new STARTUPINFO();
            SECURITY_ATTRIBUTES pSec = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES tSec = new SECURITY_ATTRIBUTES();

            pSec.nLength = Marshal.SizeOf(pSec);
            tSec.nLength = Marshal.SizeOf(tSec);

            success = CreateProcess(null, Filename,
                ref pSec, ref tSec, false, CreationFlags.CREATE_SUSPENDED,
                IntPtr.Zero, null, ref sInfo, out pInfo);

            if (success)
                requiresElevation = false;
            else
            {
                win32error = Marshal.GetLastWin32Error();
                if (win32error == ERROR_ELEVATION_REQUIRED)
                    requiresElevation = true;
                else
                    throw new Win32Exception(win32error);
            }


            TerminateProcess(pInfo.hProcess, 0);
            CloseHandle(pInfo.hThread);
            CloseHandle(pInfo.hProcess);

            return requiresElevation;
        }
    }
    public partial class Folder : Form
    {
        public double TargetOpacity = 0;
        public double OpacityInc = 0;
        public Size TargetSize = new Size(0, 0);
        public int IncW = 0, IncH = 0;
        public bool Loaded = false, Set = false;
        public bool InvertedX = false, InvertedY = false;
        public int r_add = 0;
        public Point TargetLocation;
        public static Control Highlight = null;
        public Dictionary<Control, string> Con = new Dictionary<Control, string>();
        public bool initstart = true;
        public Folder(string FolderName)
        {
            TargetLocation = Cursor.Position;
            if (!File.Exists(Environment.CurrentDirectory + @"\Folders\" + FolderName.ToLower() + @"\info.ini"))
            {
                MessageBox.Show("Missing folder.");
                Environment.Exit(200);
            }
            Dictionary<string, string> f = Parser.ParseINI(Environment.CurrentDirectory + @"\Folders\" + FolderName + @"\info.ini", "Info");
            f.TryGetValue("Folder Name", out string fname);
            if (GetAKeyState((int)Keys.ShiftKey) != 0 && GetAKeyState((int)Keys.ControlKey) == 0)
            {
                Process.Start(Environment.CurrentDirectory + @"\Folders\" + FolderName);
                Environment.Exit(1);
            }
            if (GetAKeyState((int)Keys.ShiftKey) != 0 && GetAKeyState((int)Keys.ControlKey) != 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to unregister and delete this Pop-Up folder?", $"Delete '{fname}'?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Directory.Delete(Environment.CurrentDirectory + @"\Folders\" + FolderName, true);
                    MessageBox.Show("Folder has been deleted.");
                }
                Environment.Exit(2);
            }
            InitializeComponent();
            Dictionary<string, string> gensettings = Parser.ParseINI(Environment.CurrentDirectory + @"\settings.ini", "General");
            gensettings.TryGetValue("WindowMaxHeight", out string smh);
            gensettings.TryGetValue("WindowMaxWidth", out string smw);
            int mw = int.Parse(smw), mh = int.Parse(smh);
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Folders\" + FolderName);
            L_Title.Text = fname;
            if (L_Title.Width > mw) mw = L_Title.Width;
            Text = fname;
            int iw = 0, ih = 0;
            int items = 0;
            foreach (string fi in files)
            {
                if (new FileInfo(fi).Name != "info.ini")
                {
                    Icon i = Icon.ExtractAssociatedIcon(fi);
                    PictureBox p = new PictureBox();
                    p.Image = i.ToBitmap();
                    p.Size = i.Size;
                    iw = i.Size.Width;
                    ih = i.Size.Height;
                    FlowPanel.Controls.Add(p);
                    p.MouseDoubleClick += new MouseEventHandler(ItemDClick);
                    p.MouseHover += new EventHandler(Tooltips);
                    p.MouseLeave += new EventHandler(ItemLeave);
                    items++;
                    Con.Add(p, fi);
                }
            }
            int row = 1;
            int iperrow = (int)Math.Floor((double)mw / (double)iw + (double)4);
            while (items > iperrow)
            {
                items -= iperrow;
                row++;
            }
            int curh = row * ih + 4;
            if (mh < curh) mh = curh;
            TargetSize = new Size(mw, mh);
            TargetOpacity = 0.85;
            int TimeSet = 250 / 20;
            OpacityInc = 0.03;
            IncW = mw / TimeSet;
            IncH = mh / TimeSet;
            Size = new Size(0, 0);
            Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            TopMost = true;
            Opacity = 0;
            FlowPanel.Size = new Size(mw, mh - L_Title.Height);
            Size ScreenSize = Screen.FromControl(this).Bounds.Size;
            if (Cursor.Position.X + mw + 32 >= ScreenSize.Width) InvertedX = true;
            if (Cursor.Position.Y + mh + 32 >= ScreenSize.Height) InvertedY = true;
            Console.WriteLine("Screen W:{0}, H:{1}", ScreenSize.Width, ScreenSize.Height);
            Console.WriteLine("Window OpCorner X:{0}, Y:{1}", Cursor.Position.X, Cursor.Position.Y);
            Console.WriteLine("Inverted X:{0}, Y:{1}", InvertedX, InvertedY);
            new Thread(new ThreadStart(UpdateHighlights)).Start();
        }

        public void ItemLeave(object sender, EventArgs e)
        {
            if (sender == Highlight) Highlight = null;
        }

        public void UpdateHighlights()
        {
            Console.WriteLine("OpenForms: {0}", Application.OpenForms.Count);
            while (Application.OpenForms.Count > 0 || initstart)
            {
                foreach (Control c in FlowPanel.Controls)
                {
                    if (c != Highlight)
                    {
                        c.BackColor = BackColor;
                    }
                    else
                    {
                        c.BackColor = Color.LightBlue;
                    }
                }
                if (Application.OpenForms.Count > 0) initstart = false;
            }
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int vKeyCode);

        public static short GetAKeyState(int vKey)
        {
            if (BitConverter.GetBytes(GetAsyncKeyState(vKey))[1] == 0x80)
            {
                return 1;
            } else
            {
                return 0;
            }
        }

        private void Tooltips(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            Con.TryGetValue((PictureBox)sender, out string file);
            tt.SetToolTip((PictureBox)sender, new FileInfo(file).Name.Replace(".lnk", ""));
            Highlight = (PictureBox)sender;
        }
        public void ItemDClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FlowPanel.Controls.Remove((PictureBox)sender);
                Con.TryGetValue((PictureBox)sender, out string file);
                File.Delete(file);
                return;
            }
            if (e.Clicks > 1)
            {
                try
                {
                    Con.TryGetValue((PictureBox)sender, out string file);
                    Process prc = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(file);
                    if (file.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase))
                    {
                        psi = new ProcessStartInfo(GetTargetPath(file));
                        psi.WorkingDirectory = GetWorkingDirectory(file);
                        psi.Arguments = GetArgument(file);
                    }
                    if (psi.FileName.EndsWith(".exe") || psi.FileName.EndsWith(".msi"))
                    {
                        if (UACChecker.RequiresElevation(psi.FileName)) psi.Verb = "runas";
                    }
                    if (psi.FileName == "")
                    {
                        psi.FileName = file;
                    }
                    psi.UseShellExecute = true;
                    psi.RedirectStandardOutput = false;
                    psi.RedirectStandardInput = false;
                    psi.CreateNoWindow = false;
                    prc.StartInfo = psi;
                    prc.Start();
                    Application.Exit();
                
                } catch (Exception ex)
                {
                    using (StreamWriter sw = File.AppendText(Environment.CurrentDirectory + @"\log.txt"))
                    {
                        sw.WriteLine("[Error] Execution error with folder {0}!", L_Title.Text);
                        foreach (string line in ex.Message.Split('\n'))
                        {
                            sw.WriteLine("[Error] {0}", line);
                        }
                    }
                    DialogResult dr = MessageBox.Show(ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    if (dr == DialogResult.OK) Environment.Exit(1);
                    Thread.Sleep(2000);
                }
            }
        }
        private static string GetArgument(string lnkpath)
        {
            if (System.IO.File.Exists(lnkpath))
            {
                // WshShellClass shell = new WshShellClass();
                WshShell shell = new WshShell(); //Create a new WshShell Interface
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(lnkpath); //Link the interface to our shortcut

                return link.Arguments;
            }
            throw new FileNotFoundException("File not found!");
        }
        private static string GetWorkingDirectory(string lnkpath)
        {
            if (System.IO.File.Exists(lnkpath))
            {
                // WshShellClass shell = new WshShellClass();
                WshShell shell = new WshShell(); //Create a new WshShell Interface
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(lnkpath); //Link the interface to our shortcut

                return link.WorkingDirectory;
            }
            throw new FileNotFoundException("File not found!");
        }
        private static string GetTargetPath(string lnkpath)
        {
            if (System.IO.File.Exists(lnkpath))
            {
                // WshShellClass shell = new WshShellClass();
                WshShell shell = new WshShell(); //Create a new WshShell Interface
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(lnkpath); //Link the interface to our shortcut

                return link.TargetPath;
            }
            throw new FileNotFoundException("File not found!");
        }
        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("Opacity: {0}, Size: {1},{2}", Opacity, Size.Width,Size.Height);
            if (!InvertedX && !InvertedY)
            {
                Location = TargetLocation;
                if (Size.Width < TargetSize.Width) Size = new Size(Size.Width + IncW, Size.Height);
                if (Size.Height < TargetSize.Height && Size.Width >= TargetSize.Width && Opacity < TargetOpacity) Size = new Size(Size.Width, Size.Height + IncH);
            }
            if (!InvertedX && InvertedY)
            {
                if (Size.Width < TargetSize.Width)
                { 
                    Size = new Size(Size.Width + IncW, Size.Height);
                    Location = new Point(TargetLocation.X, TargetLocation.Y);
                }
                if (Size.Height -32 < TargetSize.Height && Size.Width >= TargetSize.Width && Opacity < TargetOpacity)
                { 
                    Size = new Size(Size.Width, Size.Height + IncH);
                    Location = new Point(TargetLocation.X, Location.Y - IncH);
                }
            }
            if (InvertedX && !InvertedY)
            {
                if (Size.Width -32 < TargetSize.Width)
                {
                    Size = new Size(Size.Width + IncW, Size.Height);
                    Location = new Point(TargetLocation.X - IncW - r_add, TargetLocation.Y);
                    r_add += IncW;
                }
                if (Size.Height < TargetSize.Height && Size.Width >= TargetSize.Width && Opacity < TargetOpacity)
                {
                    Size = new Size(Size.Width, Size.Height + IncH);
                    Location = new Point(TargetLocation.X - TargetSize.Width, TargetLocation.Y);
                    //Location = new Point(Location.X, Location.Y - Siz);
                }
            }
            if (InvertedX && InvertedY)
            {
                if (Size.Width - 32 < TargetSize.Width)
                {
                    Size = new Size(Size.Width + IncW, Size.Height);
                    Location = new Point(Location.X - IncW, TargetLocation.Y);
                }
                if (Size.Height -32 < TargetSize.Height && Size.Width >= TargetSize.Width && Opacity < TargetOpacity)
                {
                    Size = new Size(Size.Width, Size.Height + IncH);
                    Location = new Point(TargetLocation.X - TargetSize.Width, TargetLocation.Y - IncH);
                    //Location = new Point(Location.X, Location.Y - Siz);
                }
            }
            if (Opacity < TargetOpacity) Opacity += OpacityInc;
            this.Refresh();
            if (Size.Width >= TargetSize.Width && Size.Height >= TargetSize.Height && Opacity >= TargetOpacity)
            {
                Loaded = true;
                FadeTimer.Enabled = false;
                if (InvertedX) Location = new Point(TargetLocation.X - TargetSize.Width, Location.Y);
                if (InvertedY) Location = new Point(Location.X, TargetLocation.Y - TargetSize.Height);
                this.Refresh();
                //CloseTimer.Start();
            }
            if (!Loaded && !Set)
            {
                //Location = TargetLocation;
                Set = true;
            }
        }

        private void CurCheck_Tick(object sender, EventArgs e)
        {
            if (GetAKeyState((int)Keys.Escape) != 0)
            {
                Environment.Exit(3);
            }
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Folder_MLeave(object sender, EventArgs e)
        {
            if (Loaded) Application.Exit();
        }

        private void FlowPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
