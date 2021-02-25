using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics; 

namespace kursach_1._1
{
    public partial class MainForm : Form
    {
        #region разель перемен 
        /// <summary>
        /// Объект класса C_WinPoc 
        /// </summary>
        C_WinPoc MyPoc = new C_WinPoc();
        /// <summary>
        /// Объект класса C_CopyFile
        /// </summary>
        C_CopyFile MyCopy = new C_CopyFile();
        /// <summary>
        /// Список запушеных потоков
        /// </summary>
        List<int> idtheard = new List<int>();
        /// <summary>
        /// индетификатор устройств
        /// </summary>
        List<string> Aserial = new List<string>();
        /// <summary>
        /// список для название устройств
        /// </summary>
        List<string> Aname = new List<string>();
        /// <summary>
        /// путь к исходным файлам
        /// </summary>
        List<string> Apath = new List<string>();
        /// <summary>
        /// Объект класса С_XML
        /// </summary>
        C_XML My_xml = new C_XML();
        #endregion

        #region Конструктор
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion


        #region добавленые
        /// <summary>
        /// метод для открытые формы добавленые 
        /// </summary>     
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();
            form.ShowDialog();
            if (form.Path != "" & form.disckLabel != "")
            {
                DirectoryInfo cDirs = new DirectoryInfo(form.Path);
                FileInfo[] file = cDirs.GetFiles("*.*", SearchOption.AllDirectories);

                foreach (FileInfo dir in file)
                {                  
                    Task ts = new Task(() => { MyCopy.CopyFile(dir.FullName, form.disckLabel); Add(); });
                    int id = ts.Id;
                    idtheard.Add(id);
                    
                    ts.Start();                  
                }
                Aserial.Clear();
                Aname.Clear();
                Apath.Clear();
                My_xml.XmlLoad_From_MainDB(Aserial, Aname, Apath);
                lv_main.Items.Clear();
                update_Main_table(Aserial, Aname, Apath);
            }

        }
        #endregion

        #region удаленые
        /// <summary>
        /// метод для открытые формы удаленые
        /// </summary>        
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i = lv_main.SelectedIndices[0];
                if (i > 0)
                {
                    DialogResult result;
                    result = MessageBox.Show("Удалить устройство из БД?", "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        string ser_for_del = lv_main.Items[i].SubItems[1].Text;
                        My_xml.del_XLM(ser_for_del);
                        Aserial.Clear();
                        Aname.Clear();
                        Apath.Clear();
                        My_xml.XmlLoad_From_MainDB(Aserial, Aname, Apath);
                        lv_main.Items.Clear();
                        update_Main_table(Aserial, Aname, Apath);
                    }
                }

            }
            catch
            {
                MessageBox.Show("Не выбран элемент для удаленые");
            }
        }
        #endregion

        #region Метод перехвата сообщение
        /// <summary>
        /// Метод перехвата сообщение 
        /// </summary>        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            bool tf=false;
            string new_disck = ""; 
            MyPoc.My_WndProc("Main", m, ref tf, ref new_disck);
            if (new_disck != "")
            {
                file_search(new_disck); 
            }
            if (tf == true)
            {
                Aserial.Clear();
                Aname.Clear();
                Apath.Clear();
                My_xml.XmlLoad_From_MainDB(Aserial, Aname, Apath);
                lv_main.Items.Clear(); 
                update_Main_table(Aserial, Aname, Apath); 
                
            }

           
        }
        #endregion

        #region вспомагательные методы
        int k = 0; 
        /// <summary>
        /// открытые формы из панел задач
        /// </summary>        
        private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm f = new MainForm();
            forIco.Visible = false; 
               if(k==1)
                   f.Show();
               k = 0; 
            
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            k = 1; 
        }
        private void выходToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult result; 
            forIco.Visible = false;
            if (idtheard.Count > 0)
            {
                result = MessageBox.Show("", "Идет синхронизация. Перервать ?", MessageBoxButtons.YesNo);
                if (result == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// удаленые выполненных потоков из списка
        /// </summary>
        private void Add()
        {            
            idtheard.Remove(Convert.ToInt16(Task.CurrentId));            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            {
                toolStripStatusLabel2.Text = idtheard.Count.ToString(); 
            }   
        }
        #endregion

        #region загрузка фармы
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("MainDB"))
            {
                timer1.Start();
                Aserial.Clear();
                Aname.Clear();
                Apath.Clear();
                My_xml.XmlLoad_From_MainDB(Aserial, Aname, Apath);
                lv_main.Items.Clear();
                update_Main_table(Aserial, Aname, Apath);
            }
            else
            {
                MessageBox.Show("Нету устройстви для синхронизации");
            }
            
        }
        #endregion

        #region обновленые талицы
        /// <summary>
        /// метод для обновленые список устройств в главном таблице
        /// </summary>
        private void update_Main_table(List<string> ser, List<string> name, List<string> path)
        {
            for (int i = 0; i <= name.Count - 1; i++)
            {
                if (drivers_search(ser[i]))
                {
                    ListViewItem itm = new ListViewItem();
                    itm.SubItems.Add(ser[i]);
                    itm.SubItems.Add(name[i]);
                    itm.SubItems.Add(path[i]);
                    lv_main.Items.Add(itm);
                    lv_main.Items[i].ImageIndex = 0;
                }
                else
                {
                    ListViewItem itm = new ListViewItem();
                    itm.SubItems.Add(ser[i]);
                    itm.SubItems.Add(name[i]);
                    itm.SubItems.Add(path[i]);
                    lv_main.Items.Add(itm);
                    lv_main.Items[i].ImageIndex = 1;
                }

            }

        }
        #endregion

        #region поиск файла  Serial

        /// <summary>
        /// метод для поиска файлов в устройствах
        /// </summary>        
        private bool drivers_search(string driver_ser)
        {
            string SerFile = "Serial.txt"; 
            bool tf = false; 
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (File.Exists(d.Name + SerFile))
                {
                    if (ser_tf(d.Name + SerFile) == driver_ser)
                    {
                        tf = true;
                    }                    
                }
            }
            return tf;  
        }
        #endregion

        #region
        /// <summary>
        /// метод для полученые индентификатор устройств
        /// </summary>      
        private string ser_tf(string Apath)
        {
            
            FileStream myhelp = new FileStream(Apath, FileMode.Open);
            StreamReader ln = new StreamReader(myhelp, Encoding.GetEncoding(1251));
            string s = ln.ReadLine(); 
            ln.Close();
            return s; 
        }
        #endregion

        #region 
        /// <summary>
        /// получает буква диска при подключеные новых устройств
        /// </summary>
        /// <param name="disck_name"></param>
        private void file_search(string disck_name)
        {
            string path = disck_name + ":\\";
            if (File.Exists(path + "Serial.txt"))
            {
                string nser = ser_tf(path + "Serial.txt");
                string npt = My_xml.load_path(nser);
                if (npt != "")
                {
                    DirectoryInfo cDirs = new DirectoryInfo(npt);
                    FileInfo[] file = cDirs.GetFiles("*.*", SearchOption.AllDirectories);

                    foreach (FileInfo dir in file)
                    {
                        Task ts = new Task(() => { MyCopy.CopyFile(dir.FullName, path); Add(); });
                        int id = ts.Id;
                        idtheard.Add(id);
                        ts.Start();
                    }

                }
            }
        }
        #endregion

        #region Справка
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Help.mht");
        }
        #endregion


    }
}
