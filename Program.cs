using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Pop_Up_Folders
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Arguments)
        {
            //MessageBox.Show(Join(Arguments, ' '));
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Folders")) Directory.CreateDirectory(Environment.CurrentDirectory + @"\Folders");
            //Assess arguments
            /*
            foreach(string s in Arguments)
            {
                Console.WriteLine(s);
            }
            bool Quoting = false;
            List<string> args = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach(string a in Arguments)
            {
                if (!Quoting)
                {
                    if (sb.Length > 0) sb = new StringBuilder();
                    if (!a.Contains('"'.ToString()) || (a.StartsWith('"'.ToString()) && a.EndsWith('"'.ToString())))
                    {
                        args.Add(a);
                    } else if (a.StartsWith('"'.ToString()))
                    {
                        Console.WriteLine("Quoting");
                        Quoting = true;
                        sb.Append(a + " ");
                    }
                } else
                {
                    if (a.EndsWith('"'.ToString()))
                    {
                        sb.Append(a);
                        args.Add(sb.ToString());
                        Quoting = false;
                    } else
                    {
                        sb.Append(a + " ");
                    }
                }
            }
            foreach(string s in args)
            {
                Console.WriteLine(s);
            }
            Environment.Exit(20);
            */
            //Assess
            bool FolderMode = false, SettingsMode = true, SetFolder = false, ListMode = false;
            string fld = "";
            foreach(string a in Arguments)
            {
                if (!SetFolder)
                {
                    if (a.StartsWith("-"))
                    {
                        switch (a.Replace("-", ""))
                        {
                            case "f":
                                //Folder Mode
                                SetFolder = true;
                                break;
                            case "s":
                                //Settings mode
                                SettingsMode = true;
                                break;
                            default:
                                Console.WriteLine("Unknown flag: {0}", a);
                                break;
                        }
                    }
                } else
                {
                    if (FolderMode)
                    {
                        if (File.Exists(a))
                        {
                            File.Move(a, Environment.CurrentDirectory + @"\Folders\" + fld + @"\" + new FileInfo(a).Name);
                            //MessageBox.Show("Moved file.");
                            Environment.Exit(0);
                        }
                    }
                    fld = a;
                    FolderMode = true;
                }
            }
            if (SettingsMode && FolderMode) SettingsMode = false;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (SettingsMode)
            {
                Application.Run(new SettingsForm());
            }
            if (FolderMode)
            {
                if (!Directory.Exists(Environment.CurrentDirectory + @"\Folders\" + fld))
                {
                    //folder does not exist
                    MessageBox.Show($"Folder not registered.\nArguments: {string.Join(" ", Arguments)}");
                } else
                {
                    if (!ListMode)
                    {
                        Application.Run(new Folder(fld));
                    } else
                    {
                        Application.Run(new Folder_NewType(fld));
                    }
                }
            }
        }
        private static int Count(string src, char check)
        {
            int i = 0;
            foreach(char c in src)
            {
                if (c == check) ++i;
            }
            return i;
        }
        private static bool DivByTwo(int value)
        {
            return (value % 2) == 0;
        }
        private static string Join(string[] src, char sep)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in src)
            {
                sb.Append(s + sep.ToString());
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
