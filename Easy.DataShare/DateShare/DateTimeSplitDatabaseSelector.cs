using System;
using System.Collections.Generic;
using System.Linq;

namespace Easy.DataShare.DateShare
{
    public  class DateTimeSplitDatabaseSelector
    {
        private readonly IList<IDateTimeSplitDatabase> _DATABASE = new List<IDateTimeSplitDatabase>();

        public DateTimeSplitDatabaseSelector()
        {

        }
        public void Register(IDateTimeSplitDatabase database)
        {
            _DATABASE.Add(database);
        }
        public IDateTimeSplitDatabase this[int index]
        {
            get
            {
                return _DATABASE[index];
            }
        }


        /// <summary>
        /// 根据时间范围选择合适的数据库
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<IDateTimeSplitDatabase> Select(DateTime start,DateTime end)
        {
            return _DATABASE.Where(m => m.IsSelected(start, end));
        }
        public IEnumerable<IDateTimeSplitDatabase> Select(DateTime? start, DateTime? end, OrderBy orderBy)
        {
            if(start == null && end == null)
            {
                if(orderBy == OrderBy.ASC)
                {
                    return _DATABASE.OrderBy(m => m.Index);
                }
                return _DATABASE.OrderByDescending(m => m.Index);
            }

            if(start.HasValue && end == null)
            {
                if(orderBy == OrderBy.ASC)
                {
                   return  _DATABASE.Where(m => m.Index >= Select(start.Value.Date).Index).OrderBy(m => m.Index);
                }
                return _DATABASE.Where(m => m.Index >= Select(start.Value.Date).Index).OrderByDescending(m => m.Index);
            }
            if(end.HasValue && start == null)
            {
                if(orderBy == OrderBy.ASC)
                {
                    return _DATABASE.Where(m => m.Index <= Select(end.Value.Date).Index).OrderBy(m => m.Index);
                }
                return _DATABASE.Where(m => m.Index <= Select(end.Value.Date).Index).OrderByDescending(m => m.Index);
            }

            if(orderBy == OrderBy.ASC)
            {
                return Select(start.Value, end.Value).OrderBy(m => m.Index);
            }
            return Select(start.Value, end.Value).OrderByDescending(m => m.Index);
        }
        /// <summary>
        /// 根据时间点选择合适的数据库
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public IDateTimeSplitDatabase Select(DateTime datetime)
        {
            return _DATABASE.Where(m => m.IsSelected(datetime.Date)).FirstOrDefault();
        }
        /// <summary>
        /// 获得当前数据库的下一个数据库（时间更远的库）
        /// </summary>
        /// <param name="currentDatabaseIndex"></param>
        /// <returns></returns>
        public IDateTimeSplitDatabase Next(int currentDatabaseIndex)
        {
            return _DATABASE.SingleOrDefault(m => m.Index == (currentDatabaseIndex - 1));
        }
        public IDateTimeSplitDatabase Previous(int currentDatabaseIndex)
        {
            return _DATABASE.SingleOrDefault(m => m.Index == (currentDatabaseIndex + 1));
        }
        /// <summary>
        /// 第一个库（时间最近的库）
        /// </summary>
        public IDateTimeSplitDatabase Latest
        {
            get
            {
                return _DATABASE.SingleOrDefault(m => m.Index == _DATABASE.Max(x => x.Index));
            }
        }
        /// <summary>
        /// 最后一个库（时间最远的库）
        /// </summary>
        public IDateTimeSplitDatabase Oldest

        {
            get
            {
                return _DATABASE.SingleOrDefault(m => m.Index == _DATABASE.Min(x => x.Index));
            }
        }

        /// <summary>
        /// 全部数据库
        /// </summary>
        public IEnumerable<IDateTimeSplitDatabase> All
        {
            get
            {
                return _DATABASE.OrderByDescending(m => m.Index);
            }
        }

    }
}
