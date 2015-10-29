
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Loglite
{
    public class SqliteHelper
    {
        const string DB_NAME = "log.sqlite";
        SQLiteConnection connection;

        public SqliteHelper(string connStr)
        {
            this.Open(connStr);
        }

        public SqliteHelper()
        {
            if (!File.Exists(DB_NAME))
            {
                InitDatabase();
            }
            else
            {
                this.Open("Data Source=" + DB_NAME + ";Version=3;");
            }
        }

        public void Open(string connStr)
        {
            try
            {
                if (this.connection == null)
                    this.connection = new SQLiteConnection(connStr);

                if (connection != null && connection.State == ConnectionState.Closed)
                    connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Close()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
                connection.Close();
        }


        /// <summary>
        /// 初始化数据库
        /// </summary>
        public void InitDatabase()
        {
            try
            {
                SQLiteConnection.CreateFile(DB_NAME);
                this.Open("Data Source=" + DB_NAME + ";Version=3;");
                InitTables();
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 初始化表结构
        /// </summary>
        public void InitTables()
        {
            string table_log = "CREATE TABLE LOG (logtime timestamp,message varchar,user varchar)";
            CreateTable(table_log);
        }

        /// <summary>
        /// 创建表结构
        /// </summary>
        /// <param name="sql"></param>
        public void CreateTable(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

        

        /// <summary>
        /// 执行，有事务
        /// </summary>
        /// <param name="sql"></param>
        public bool ExecuteNonQuery(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            SQLiteTransaction tran = connection.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception e)
            {
                tran.Rollback();
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                this.Close();
            }
        }

        public SQLiteDataReader Query(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
