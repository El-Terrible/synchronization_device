using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;//для работы с двоичними массивамы 
using System.Runtime.InteropServices;//Управляет размещением объекта при его экспорте в неуправляемый код.
using System.Windows.Forms;

namespace kursach_1._1
{
    /// <summary>
    /// класс определяющий подключеные устройств
    /// работает с WinPoc
    /// </summary>
    class C_WinPoc
    {
        #region раздел перемен
        /// <summary>
        /// Сообщение выполняется при подключении или отчключении устройств к системе
        /// </summary>
        private const int WM_DEVICECHANGE = 0x0219;
        /// <summary>
        /// Процесс при отсоединение флешки
        /// </summary>
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        /// <summary>
        /// Процесс при подключение флешки
        /// </summary>
        private const int DBT_DEVICEARRIVAL = 0x8000;
        /// <summary>
        /// Логический раздель
        /// </summary>
        private const uint DBT_DEVTYP_VOLUME = 0x00000002;

        /// <summary>
        /// Структура $DEV_BROADCAST_HDR надаёт тип устройств для отслеживания
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct DEV_BROADCAST_HDR
        {
            public uint dbch_size;
            public uint dbch_devicetype;
            public uint dbch_reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DEV_BROADCAST_VOLUME
        {
            public uint dbcv_size;
            public uint dbcv_devicetype;
            public uint dbcv_reserved;
            public uint dbcv_unitmask;
            public ushort dbcv_flags;
        }

        
        #endregion

        #region Метод обработки сообщение Windows
        public void My_WndProc(string sender,  Message m, ref bool tf, ref string new_disck)
        {
            AddForm AddForm_object = new AddForm(); 

            if (m.Msg == WM_DEVICECHANGE)
            {
                switch (m.WParam.ToInt32())
                {
                    case DBT_DEVICEARRIVAL:
                        DEV_BROADCAST_HDR dbhARRIVAL = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_HDR));
                        if (dbhARRIVAL.dbch_devicetype == DBT_DEVTYP_VOLUME)
                        {
                            DEV_BROADCAST_VOLUME dbv = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME));
                            BitArray bArray = new BitArray(new byte[] 
                            {
                                (byte)(dbv.dbcv_unitmask & 0x00FF),
                                (byte)((dbv.dbcv_unitmask & 0xFF00) >> 8),
                                (byte)((dbv.dbcv_unitmask & 0xFF0000) >> 16),
                                (byte)((dbv.dbcv_unitmask & 0xFF000000) >> 24)
                            });

                            int driveLetter = Char.ConvertToUtf32("A", 0);
                            for (int i = 0; i < bArray.Length; i++)
                            {
                                if (bArray.Get(i))
                                {
                                    string kk = Char.ConvertFromUtf32(driveLetter);
                                    new_disck = kk; 
                                }
                                driveLetter += 1;
                            }
                            tf = true;                           
                            }
                        break;
                    case DBT_DEVICEREMOVECOMPLETE:
                        { 
                            tf = true;                          
                            
                        }

                        break;
                }
            }
        }
        #endregion 
    }
}
