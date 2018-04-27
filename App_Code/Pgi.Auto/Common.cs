using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Pgi.Auto
{
   public class Common
    {
        string sCode;
        string sValue;
        string sKey;
        string sEmpty;
        string sSql;
        List<SqlParameter> SqlParts = new List<SqlParameter>();
        public string Code
        {

            get { return sCode; }

            set { sCode = value; }

        }
        public string Value
        {

            get { return sValue; }

            set { sValue = value; }

        }
        public string Key
        {

            get { return sKey; }

            set { sKey = value; }

        }

        public string Empty
        {

            get { return sEmpty; }

            set { sEmpty = value; }

        }

        public string Sql
        {

            get { return sSql; }

            set { sSql = value; }

        }

        public List<SqlParameter> Paras
        {

            get { return SqlParts; }

            set { SqlParts = value; }

        }
    }
}
