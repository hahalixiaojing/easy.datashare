using System;
using System.Collections.Generic;

namespace Easy.DataShare.DateShare
{
    /// <summary>
    /// 分页数据返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTimeDataList<T>
    {
        public DataTimeDataList(IEnumerable<T> rows, long totalRows)
        {
            this.Rows = rows;
            this.TotalRows = totalRows;
        }
        public IEnumerable<T> Rows { get; private set; }
        public Int64 TotalRows { get; private set; }
    }
}
