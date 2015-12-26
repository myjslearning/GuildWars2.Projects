using System.Collections.Generic;

namespace System.Data.SQLite
{
    internal class SQLiteColumnList : IList<SQLiteColumn>
    {
        private List<SQLiteColumn> _lst = new List<SQLiteColumn>();

        private void CheckColumnName(string colName) {
            for(int i = 0; i < _lst.Count; i++) {
                if(_lst[i].ColumnName == colName)
                    throw new Exception("Column name of \"" + colName + "\" is already existed.");
            }
        }

        public int IndexOf(SQLiteColumn item) => _lst.IndexOf(item);

        public void Insert(int index, SQLiteColumn item) {
            CheckColumnName(item.ColumnName);

            _lst.Insert(index, item);
        }

        public void RemoveAt(int index) {
            _lst.RemoveAt(index);
        }

        public SQLiteColumn this[int index] {
            get {
                return _lst[index];
            }
            set {
                if(_lst[index].ColumnName != value.ColumnName) {
                    CheckColumnName(value.ColumnName);
                }

                _lst[index] = value;
            }
        }

        public void Add(SQLiteColumn item) {
            CheckColumnName(item.ColumnName);

            _lst.Add(item);
        }

        public void Clear() {
            _lst.Clear();
        }

        public bool Contains(SQLiteColumn item) => _lst.Contains(item);

        public void CopyTo(SQLiteColumn[] array, int arrayIndex) {
            _lst.CopyTo(array, arrayIndex);
        }

        public int Count => _lst.Count;

        public bool IsReadOnly => false;

        public bool Remove(SQLiteColumn item) => _lst.Remove(item);

        public IEnumerator<SQLiteColumn> GetEnumerator() => _lst.GetEnumerator();

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator() => _lst.GetEnumerator();
    }
}