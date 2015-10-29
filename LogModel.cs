using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loglite
{
    class LogModel
    {
        public string User
        {
            get;
            set;
        }


       
        public DateTime LogTime
        {
            get;
            set;

        }

        public string Message
        {
            get;
            set;
        }


        public LogModel(string user, string message)
        {
            this.User = user;
            this.Message = message;
            this.LogTime = DateTime.Now;
        }
    }
}
