namespace System.Data.SQLite
{
    internal class SQLiteTable
    {
        public string TableName = "";
        public SQLiteColumnList Columns = new SQLiteColumnList();

        public SQLiteTable() {
        }

        public SQLiteTable(string name) {
            TableName = name;
        }
    }
}