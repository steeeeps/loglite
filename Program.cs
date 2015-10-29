using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Loglite
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteHelper helper = new SqliteHelper();
            LogModel logModel = new LogModel("steeeeps", "app start");

            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + " \\log4net.xml";
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));

            log4net.ILog logdb = null;
            log4net.ILog logfile = null;

            logdb = (log4net.ILog)log4net.LogManager.GetLogger("LogToSqlite");
            logdb.Info(logModel);

            logfile = (log4net.ILog)log4net.LogManager.GetLogger("LogToFile");
            logfile.Error(logModel, new Exception("app start has error"));

        }
    }
}
