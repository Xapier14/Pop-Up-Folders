using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IniParser;
using IniParser.Parser;
using IniParser.Model;
using System.IO;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;
using Microsoft.CSharp;
using File = System.IO.File;

namespace Pop_Up_Folders
{
    public partial class SettingsForm : Form
    {
        public static List<string> flist = new List<string>();
        public SettingsForm()
        {
            int mw = 0, mh = 0;
            InitializeComponent();
            bool init = false;
            while (!init)
            {
                try
                {
                    Dictionary<string, string> inidata = Parser.ParseINI(Environment.CurrentDirectory + @"\settings.ini", "General");
                    string smw, smh;
                    inidata.TryGetValue("WindowMaxWidth", out smw);
                    inidata.TryGetValue("WindowMaxHeight", out smh);
                    mw = int.Parse(smw);
                    mh = int.Parse(smh);
                    init = true;
                }
                catch
                {
                    try
                    {
                        Dictionary<string, string> nd = new Dictionary<string, string>();
                        nd.Add("WindowMaxWidth", "750");
                        nd.Add("WindowMaxHeight", "300");
                        mw = 750;
                        mh = 300;
                        Parser.WriteINI(Environment.CurrentDirectory + @"\settings.ini", Parser.DictToSData(nd, "General"));
                        init = true;
                    }
                    catch { }
                }
            }
            TextBox_MH.Text = mh.ToString();
            TextBox_MW.Text = mw.ToString();
        }

        private void FlowPanel_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (!flist.Contains(file))
                {
                    Icon i = Icon.ExtractAssociatedIcon(file);
                    PictureBox p = new PictureBox();
                    p.Image = i.ToBitmap();
                    p.Size = i.Size;
                    FlowPanel.Controls.Add(p);
                    flist.Add(file);
                }
            }
        }

        private void FlowPanel_DEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                int mw = int.Parse(TextBox_MW.Text);
                int mh = int.Parse(TextBox_MH.Text);

                Dictionary<string, string> nd = new Dictionary<string, string>();
                nd.Add("WindowMaxWidth", mw.ToString());
                nd.Add("WindowMaxHeight", mh.ToString());
                Parser.WriteINI(Environment.CurrentDirectory + @"\settings.ini", Parser.DictToSData(nd, "General"));
                MessageBox.Show("Saved to file.");
            }
            catch
            {
                MessageBox.Show("Invalid text fields.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TextBox_CallName.TextLength > 0 && TextBox_DName.TextLength > 0 && FlowPanel.Controls.Count > 0)
            {
                SaveDialog.FileName = "";
                SaveDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Some required fields are missing!");
            }
        }

        private static void Shortcut(string dest, string src)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!File.Exists(Environment.CurrentDirectory + @"\folder.ico"))
            {
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + @"\folder.ico",FileMode.Create))
                {
                    DefaultIcons.FolderLarge.Save(fs);
                }
            }
            using (StreamWriter writer = new StreamWriter(dest))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + src);
                writer.WriteLine("IconIndex=0");
                writer.WriteLine("IconFile=" + Environment.CurrentDirectory + @"\folder.ico");
            }
        }
        private void CreateShortcut(string dest, string target, string Description, string arguments)
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(dest);
            shortcut.Description = Description;
            shortcut.TargetPath = target;
            shortcut.Arguments = arguments;
            shortcut.IconLocation = Environment.CurrentDirectory + @"\folder.ico";
            shortcut.WorkingDirectory = Environment.CurrentDirectory;
            shortcut.Save();
        }
        private void SaveDialog_FileOk(object sender, CancelEventArgs e)
        {
            TextBox_CallName.Text.Replace(" ", "_");
            if (Directory.Exists(Environment.CurrentDirectory + @"\Folders\" + TextBox_CallName.Text)) Directory.Delete(Environment.CurrentDirectory + @"\Folders\" + TextBox_CallName.Text, true);
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Folders\" + TextBox_CallName.Text);
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("Folder Name", TextBox_DName.Text);
            Parser.WriteINI(Environment.CurrentDirectory + @"\Folders\" + TextBox_CallName.Text + @"\info.ini", Parser.DictToSData(d, "Info"));
            foreach (string f in flist)
            {
                File.Copy(f, Environment.CurrentDirectory + @"\Folders\" + TextBox_CallName.Text + @"\" + (new FileInfo(f).Name));
            }
            CreateShortcut(SaveDialog.FileName, System.Reflection.Assembly.GetExecutingAssembly().Location, "Open " + TextBox_DName.Text + " as a Pop-up Folder.", "-f " + TextBox_CallName.Text);
            //MessageBox.Show($"Registered Folder ({TextBox_DName.Text})");
            TextBox_DName.Text = "";
            TextBox_CallName.Text = "";
            FlowPanel.Controls.Clear();
        }

        private void UI_Timer_Tick(object sender, EventArgs e)
        {
            ItemCountLabel.Text = "Item Count: " + FlowPanel.Controls.Count;
        }

    }
    public static class DefaultIcons
    {
        private static readonly Lazy<Icon> _lazyFolderIcon = new Lazy<Icon>(FetchIcon, true);

        public static Icon FolderLarge
        {
            get { return _lazyFolderIcon.Value; }
        }

        private static Icon FetchIcon()
        {
            var tmpDir = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())).FullName;
            var icon = ExtractFromPath(tmpDir);
            Directory.Delete(tmpDir);
            return icon;
        }

        private static Icon ExtractFromPath(string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            SHGetFileInfo(
                path,
                0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                SHGFI_ICON | SHGFI_LARGEICON);
            return System.Drawing.Icon.FromHandle(shinfo.hIcon);
        }

        //Struct used by SHGetFileInfo function
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x000000001;
    }
    public static class Parser
    {
        public static Dictionary<string, string> ParseINI(string fileName, string sectionName)
        {
            if (!File.Exists(fileName)) throw new Exception("File does not exist.");
            FileIniDataParser p = new FileIniDataParser();
            IniData id = p.ReadFile(fileName);
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach(SectionData s in id.Sections)
            {
                if (s.SectionName == sectionName)
                {
                    foreach(KeyData kd in s.Keys)
                    {
                        ret.Add(kd.KeyName, kd.Value);
                    }
                }
            }
            return ret;
        }
        public static SectionData DictToSData(Dictionary<string, string> data, string sectionName)
        {
            SectionData ret = new SectionData(sectionName);
            foreach (KeyValuePair<string, string> kv in data)
            {
                ret.Keys.AddKey(kv.Key, kv.Value);
            }
            return ret;
        }
        public static void WriteINI(string fileName, params SectionData[] sections)
        {
            FileIniDataParser p = new FileIniDataParser();
            IniData id = new IniData();
            foreach(SectionData d in sections)
            {
                id.Sections.Add(d);
            }
            if (File.Exists(fileName)) File.Delete(fileName);
            p.WriteFile(fileName, id);
        }
    }
}
