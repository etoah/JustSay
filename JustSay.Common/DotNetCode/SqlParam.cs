using System.Data;

namespace JustSay.Common.DotNetCode
{
    public class SqlParam
    {
        public string FieldName
        {
            get;
            set;
        }

        public DbType DataType
        {
            get;
            set;
        }

        public object FiledValue
        {
            get;
            set;
        }

        public SqlParam()
        {
        }

        public SqlParam(string _FieldName, object _FiledValue)
            : this(_FieldName, DbType.AnsiString, _FiledValue)
        {
        }

        public SqlParam(string _FieldName, DbType _DbType, object _FiledValue)
        {
            this.FieldName = _FieldName;
            this.DataType = _DbType;
            this.FiledValue = _FiledValue;
        }
    }
}