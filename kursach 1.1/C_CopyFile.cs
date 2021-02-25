using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//**
using System.IO; 

namespace kursach_1._1
{
    class C_CopyFile
    {
        #region копирование файла
        public void CopyFile(string APath, string Adisk)
        {
            Regex regex = new Regex(@"(.*)\\");
            string copy_to = Adisk + APath.Substring(3);
            if (!(File.Exists(copy_to)))
            {
                Match match = regex.Match(copy_to);
                try
                {
                    File.Copy(APath, copy_to);
                }
                catch
                {
                    Directory.CreateDirectory(match.Groups[1].Value);
                    File.Copy(APath, copy_to);
                }
            }
            else
            {
                file_comparison(APath, copy_to);
            }

        }
        #endregion

        #region Сравненые файлов
         /// <summary>
        /// метод для сравнение файлов
        /// </summary>
        /// <param name="AFileforCopy">путь к файлу предназначенный для копирование</param>
        /// <param name="AfileInDisck">путь к файлу находяший в диске </param>
        private void file_comparison(string AFileforCopy, string AfileInDisck)
        {
            DateTime lwFileforCopy = File.GetLastWriteTime(AFileforCopy);
            DateTime lwfileInDisck = File.GetLastWriteTime(AfileInDisck);
            if (lwFileforCopy > lwfileInDisck)
            {
                File.Copy(AFileforCopy, AfileInDisck, true); 
            }
        }
        #endregion
    }
}
