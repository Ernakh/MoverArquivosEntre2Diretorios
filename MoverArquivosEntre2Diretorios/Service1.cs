using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MoverArquivosEntre2Diretorios
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

#if DEBUG
        public void StartDebug(string[] args)
        {
            OnStart(args);   // simplesmente chama a rotina principal da classe
        }
#endif
        Timer x = new Timer();

        protected override void OnStart(string[] args)
        {
            x.Interval = 10000;//para debugar
            //x.Interval = 3600000; // a cada 1 hora
            x.Enabled = true;

            x.Elapsed += new ElapsedEventHandler(x_Elapsed);

            x.Start();
        }

        protected override void OnStop()
        {
            x.Stop();
        }

        void x_Elapsed(object sender, ElapsedEventArgs e)
        {
            x.Stop();//comentar depois
            if (DateTime.Now.Hour == 1 || DateTime.Now.Hour == 3)
            {
                string linha;
                string origem = "";
                string destino = "";

                System.IO.StreamReader file = new System.IO.StreamReader(@"diretorios.txt");

                int i = 0;

                while ((linha = file.ReadLine()) != null)
                {
                    if (i == 0)
                        origem = linha;
                    else
                        destino = linha;

                    i++;
                }

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                //System.IO.File.Copy(origem, destino, true);

                // To move a file or folder to a new location:
                //File.Move(origem, destino);

                // To move an entire directory. To programmatically modify or combine
                // path strings, use the System.IO.Path class.
                //Directory.Move(origem, destino);

                DirectoryInfo dirInfo = new DirectoryInfo(origem);

                foreach (FileInfo item in dirInfo.GetFiles())
                {
                    File.Move(item.FullName, destino + "/" + item.Name);
                }

                file.Close();
            }
        }
    }
}
