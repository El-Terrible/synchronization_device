using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kursach_1._1
{
    public partial class AddForm : Form
    {
        #region Раздел перемен
        /// <summary>
        /// Объект класса C_WinPoc 
        /// </summary>
        C_WinPoc MyPoc = new C_WinPoc();
        /// <summary>
        /// название устройств
        /// </summary>
        List<string> Drivers = new List<string>();
        /// <summary>
        /// Ескость устройств
        /// </summary>
        List<double> DriverEmkost = new List<double>();
        /// <summary>
        /// Свободно
        /// </summary>
        List<double> E_free = new List<double>();
        /// <summary>
        /// название устройств
        /// </summary>
        List<string> DriversName = new List<string>();

        string disk = "";
        string Path_for_copy = ""; 
       
        C_XML myXML = new C_XML(); 

        #endregion

        #region Конструктор
        public AddForm()
        {
            InitializeComponent();
            disckLabel = "";
            Path = ""; 
            
        }
        #endregion

        #region WinProc
        
        
        /// <summary>
        /// Метод перехвата сообщение 
        /// </summary>        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            bool tf = false; 
            MyPoc.My_WndProc("AddForm", m, ref tf);

            if (tf == true)
            {
                Table_update(); 
            }
        }
        #endregion

        #region обноволение список дисков
        public void update()
        {
            

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            Drivers.Clear();
            DriversName.Clear();
            DriverEmkost.Clear();
            E_free.Clear(); 
            
           
            foreach (DriveInfo d in allDrives)
            {
                if ((d.DriveType.ToString() == "Removable") || d.DriveType.ToString() == "Fixed")
                    Drivers.Add(string.Format("{0} ({1})", string.IsNullOrEmpty(d.VolumeLabel) ? "Локальный диск" : d.VolumeLabel, d.Name));
                DriversName.Add(d.Name); 
                if (d.IsReady)
                {
                    DriverEmkost.Add(d.TotalSize/1024/1024);
                    E_free.Add(d.AvailableFreeSpace / 1024 / 1024); 
                    
                }
            }
            for (int i = 0; i <= Drivers.Count - 1; i++)
            {
                ListViewItem itm = new ListViewItem();
                itm.SubItems.Add(Drivers[i]);
                itm.SubItems.Add(DriverEmkost[i].ToString() + "  МБ");
                itm.SubItems.Add(E_free[i].ToString() + "  МБ");
                 
                Drivers_listview.Items.Add(itm); 
                
            }
            
        }
        #endregion

        private void AddForm_Load(object sender, EventArgs e)
        {
            update();
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {         
            FolderBrowserDialog opendl = new FolderBrowserDialog();
            string rndSerial = Rnd_for_Serial();           
            string FileName = "";
            string driver_name = tb_driverName.Text; 
            try
            {
                FileName = DriversName[Drivers_listview.SelectedIndices[0]]; 
                if (File.Exists(FileName + "Serial.txt"))
                {
                     MessageBox.Show("Диск уже выбран для синхронизации");                   
                    
                }
                else
                {                  
                   if (opendl.ShowDialog() == DialogResult.OK)
                    {
                        FileStream f = new FileStream(FileName + "Serial.txt", FileMode.Create);
                        StreamWriter s = new StreamWriter(f);
                        s.WriteLine(rndSerial);
                        s.Close();                        
                        myXML.Save_To_MainDB(rndSerial, driver_name, opendl.SelectedPath);
                        disckLabel = FileName;
                        Path = opendl.SelectedPath; 

                        DirectoryInfo cDirs = new DirectoryInfo(opendl.SelectedPath);
                        FileInfo[] file = cDirs.GetFiles("*.*", SearchOption.AllDirectories);
                        

                        foreach (FileInfo dir in file)
                        {

                           // //CopyFile(dir.FullName, FileName);
                             
                            //Task ts = new Task(() => { CopyFile(dir.FullName, FileName); Add(); });                           
                            //int id = ts.Id;

                            //idtheard.Add(id); 
                            //ts.Start();                    
                           

                        }

                       
                        

                    }
                }
            }
            catch
            {
                if (FileName == "")
                {
                    MessageBox.Show("Не выбранно диск для синхронизации");
                }
            }
        }
        

        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
        

        public void Table_update()
        {
            Drivers_listview.Items.Clear();
            update(); 
        }

        public string disckLabel
        {
            get { return disk; }
            set { disk = value; }
        }

        public string Path
        {
            get { return Path_for_copy; }
            set { Path_for_copy = value; }
        }
        #region Генерато
        private string Rnd_for_Serial()
        {
            long r;
            Random rnd = new Random();
            r = rnd.Next(111111111, 999999999);
            return r.ToString();
        }
        #endregion
        
    }
}
