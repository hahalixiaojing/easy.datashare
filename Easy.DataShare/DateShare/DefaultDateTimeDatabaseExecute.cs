using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare.DateShare
{
    public class DefaultDateTimeDatabaseExecute : IDateTimeDatabaseExecute
    {
        private readonly DateTimeSplitDatabaseSelector selector;

        public DefaultDateTimeDatabaseExecute(DateTimeSplitDatabaseSelector selector)
        {
            this.selector = selector;
        }
        public void Add(DateTime datetime, Action<IDateTimeSplitDatabase> execute)
        {
            var database = selector.Select(datetime);
            execute.Invoke(database);
        }
        public Int64 Count(Func<IDateTimeSplitDatabase, Int64> execute)
        {
            var tasks = selector.All.Select(m =>
            {
                var t = new Task<Int64>(() =>
                {
                    return execute.Invoke(m);
                });
                t.Start();
                return t;
            });

            return Task.WhenAll(tasks).Result.Sum();
        }
        public Int64 Count(Query query, Func<IDateTimeSplitDatabase, Int64> execute)
        {
            var databases = selector.Select(query.Start, query.End, OrderBy.DESC);
            var tasks = databases.Select(m =>
            {
                var t = new Task<Int64>(() =>
                {
                    return execute.Invoke(m);
                });
                t.Start();
                return t;
            });
            var result = Task.WhenAll(tasks);
            return result.Result.Sum();
        }
        public ENTITY FindBy<ENTITY>(DateTime datetime, Func<IDateTimeSplitDatabase, ENTITY> execute)
        {
            return execute.Invoke(selector.Select(datetime));
        }

        public ENTITY FindBy<ENTITY>(Func<IDateTimeSplitDatabase, ENTITY> execute)
        {
            var tasks = selector.All.Select(m =>
             {
                 var t = new Task<ENTITY>(() =>
                 {
                     return execute.Invoke(m);
                 });
                 t.Start();
                 return t;
             });

            Task<ENTITY[]> results = Task.WhenAll(tasks);
            results.Wait();
            return results.Result.SingleOrDefault(m => m != null);
        }

        public IEnumerable<ENTITY> Select<ENTITY>(Func<IDateTimeSplitDatabase, IEnumerable<ENTITY>> execute)
        {
            return this.Select<ENTITY>(selector.All, execute);
        }

        public IEnumerable<ENTITY> Select<ENTITY>(DateTime start, DateTime end, Func<IDateTimeSplitDatabase, IEnumerable<ENTITY>> execute)
        {
            return this.Select<ENTITY>(selector.Select(start, end), execute);
        }

        public void Remove( Action<IDateTimeSplitDatabase> execute)
        {
            var tasks = selector.All.Select(m =>
            {
                var t = new Task(() =>
                {
                    execute.Invoke(m);
                });
                t.Start();
                return t;
            });

            Task.WhenAll(tasks).Wait();
        }

        public void Remove(DateTime datetime,Action<IDateTimeSplitDatabase> execute)
        {
            var database = selector.Select(datetime);
            execute.Invoke(database);
        }

        public DataTimeDataList<ENTITY> Select<ENTITY>(Query query,
            Func<IDateTimeSplitDatabase,long, IEnumerable<ENTITY>> dataExecute,
            Func<IDateTimeSplitDatabase, long> countExecute)
        {
            var databaseList = selector.Select(query.Start, query.End, query.OrderBy);

            var tasks = databaseList.Select(m =>
            {
                var task = new Task<long>(() =>
                {
                    return countExecute.Invoke(m);
                });
                task.Start();
                return task;
            });

            long[] databaseRows = Task.WhenAll(tasks).Result;
            int absoluteOffset = (query.PageIndex - 1) * query.PageSize;

            long endOffset = 0;
            int databaseIndex = -1;
            long relativeDatabaseOffset = 0;
            for (var i = 0; i < databaseRows.Length; i++)
            {
                long startOffset = endOffset;
                endOffset = endOffset + (databaseRows[i]);

                if (absoluteOffset >= startOffset && absoluteOffset < endOffset)
                {
                    databaseIndex = i;
                    relativeDatabaseOffset = absoluteOffset - startOffset;
                    break;
                }
            }

            if (databaseIndex == -1)
            {
                return new DataTimeDataList<ENTITY>(new ENTITY[0], databaseRows.Sum());
            }
            
            IDateTimeSplitDatabase database = databaseList.ToArray()[databaseIndex];

            List<ENTITY> rows = new List<ENTITY>();
            while (rows.Count < query.PageSize)
            {
                var thisDatabaseDataList = dataExecute.Invoke(database, relativeDatabaseOffset);
                rows.AddRange(thisDatabaseDataList);
                
                databaseIndex = databaseIndex + 1;
                if (databaseIndex >= databaseList.Count())
                {
                    break;
                }
                database = databaseList.ToArray()[databaseIndex];
                relativeDatabaseOffset = 0;
            }

            var actualReturnRows = rows.Take(query.PageSize);
            return new DataTimeDataList<ENTITY>(actualReturnRows, databaseRows.Sum());
        }

        public void Update(Action<IDateTimeSplitDatabase> execute)
        {
            var tasks = selector.All.Select(m =>
            {
                var t = new Task(() =>
                {
                    execute.Invoke(m);
                });
                return t;
            });
            Task.WhenAll(tasks).Wait();
        }
        public void Update( DateTime datetime, Action<IDateTimeSplitDatabase> execute)
        {
            var database = selector.Select(datetime);
            execute.Invoke(database);
        }

        private IEnumerable<ENTITY> Select<ENTITY>(IEnumerable<IDateTimeSplitDatabase> databases, Func<IDateTimeSplitDatabase, IEnumerable<ENTITY>> execute)
        {
            var tasks = databases.Select(m =>
            {
                var t = new Task<IEnumerable<ENTITY>>(() => {
                    return execute.Invoke(m);

                });
                t.Start();
                return t;
            });

            Task<IEnumerable<ENTITY>[]> results = Task.WhenAll(tasks);
            results.Wait();

            List<ENTITY> dataList = new List<ENTITY>();
            foreach (var item in results.Result)
            {
                if (item.Count() > 0)
                {
                    dataList.AddRange(item);
                }
            }
            return dataList;
        }
    }
}
