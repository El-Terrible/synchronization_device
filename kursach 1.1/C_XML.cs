using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml; // пространство имен для работы с XML
using System.IO;
using System.Windows.Forms; 

namespace kursach_1._1
{



    /// <summary>
    /// класс используется для сохраненые и загрузки XML файлов 
    /// в нашем случае База Данных
    /// </summary>
    class C_XML
    {

        #region Добавленые 
        /// <summary>
        ///Метод для добавленые новых устройств в БД
        /// </summary>
        /// <param name="FileName"></param>
        public  void Save_To_MainDB(string Aserial, string Aname, string Apath)
        {
            string FileName = "MainDB"; 
            DialogResult result = DialogResult.Yes; 
            XmlDocument xmld = new XmlDocument();
            if (!File.Exists(FileName))
            {

                result = MessageBox.Show("Файл База Данных не найден. Создат новый файл ?", "Файл не найден", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    XmlElement root = xmld.CreateElement("MyAttribut");
                    XmlElement id = xmld.CreateElement("id");
                    id.SetAttribute("serial", Aserial);
                    id.SetAttribute("disck_name", Aname);
                    id.SetAttribute("path", Apath);
                    root.AppendChild(id);
                    xmld.AppendChild(root);
                }
                

            }
            else
            {
                xmld.Load(FileName);
                XmlElement root = xmld.DocumentElement;
                XmlElement id = xmld.CreateElement("id");
                id.SetAttribute("serial", Aserial);
                id.SetAttribute("disck_name", Aname);
                id.SetAttribute("path", Apath);
                root.AppendChild(id);
                xmld.AppendChild(root);
            }
            if (result == DialogResult.Yes)
            {
                xmld.Save(FileName);
            }            

        }
        #endregion 

        #region загрузка данных
        public void XmlLoad_From_MainDB(List<string> Aserial, List<string> Aname, List<string> Apath)
        {
            XmlDocument xmld = new XmlDocument();
            xmld.Load("MainDB");
            XmlNode n0 = xmld.SelectSingleNode("/MyAttribut");
            XmlNodeList nl = n0.ChildNodes;

            for (int i = 0; i <= n0.ChildNodes.Count - 1; i++)
            {
                Aserial.Add(n0.ChildNodes[i].Attributes[0].Value); 
                Aname.Add(n0.ChildNodes[i].Attributes[1].Value); 
                Apath.Add(n0.ChildNodes[i].Attributes[2].Value);
             
            }
        }
        #endregion

        #region метод для полученые путь к файлам по индентификатора устройств
        public string load_path(string disck_ser)
        {
            string pt = ""; 
            XmlDocument xmld = new XmlDocument();
            xmld.Load("MainDB");
            XmlNode n0 = xmld.SelectSingleNode("/MyAttribut");
            XmlNodeList nl = n0.ChildNodes;

            for (int i = 0; i <= n0.ChildNodes.Count - 1; i++)
            {
                
                if (disck_ser == n0.ChildNodes[i].Attributes[0].Value)
                {
                    pt = n0.ChildNodes[i].Attributes[2].Value;
                }

            }
            return pt; 

        }
        #endregion 

        #region Удаление записей
        public void del_XLM(string ser)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("MainDB");           
        XmlNodeList cl = doc.DocumentElement.ChildNodes;
        foreach (XmlNode n in cl)
            if (n.Attributes[0].Value == ser)
                doc.DocumentElement.RemoveChild(n);
        
                    
            doc.Save("MainDB");
        }
        #endregion 
    }
}
