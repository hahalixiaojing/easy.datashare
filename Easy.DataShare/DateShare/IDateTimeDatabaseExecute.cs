using System;
using System.Collections.Generic;

namespace Easy.DataShare.DateShare
{
    public interface IDateTimeDatabaseExecute
    {
        /// <summary>
        /// 根据ID查询，在所有库执行操作
        /// </summary>
        /// <typeparam name="ENTITY"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        ENTITY FindBy<ENTITY>(Func<IDateTimeSplitDatabase,ENTITY> execute);
        /// <summary>
        /// 根据ID查询，指时间所在库的查询
        /// </summary>
        /// <typeparam name="ENTITY"></typeparam>
        /// <param name="datetime"></param>
        /// <returns></returns>
        ENTITY FindBy<ENTITY>(DateTime datetime, Func<IDateTimeSplitDatabase, ENTITY> execute);
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="excute"></param>
        void Add(DateTime datetime, Action<IDateTimeSplitDatabase> excute);

        /// <summary>
        /// 根据ID查询，在所有数据库查询
        /// </summary>
        /// <typeparam name="ENTITY"></typeparam>
        /// <typeparam name="KEY"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        IEnumerable<ENTITY> Select<ENTITY>(Func<IDateTimeSplitDatabase, IEnumerable<ENTITY>> execute);
        /// <summary>
        /// 根据ID查询 指定时间范围内数据库中查询
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<ENTITY> Select<ENTITY>(DateTime start, DateTime end, Func<IDateTimeSplitDatabase, IEnumerable<ENTITY>> execute);
        /// <summary>
        /// 更新操作所有数据库都执行操作
        /// </summary>
        void Update(Action<IDateTimeSplitDatabase> execute);
        /// <summary>
        /// 更新操作，根据指定的时间定位数据库
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="execute"></param>
        void Update( DateTime datetime,Action<IDateTimeSplitDatabase> execute);
        /// <summary>
        /// 移除指定ID的数所在，在所有数据都要执行
        /// </summary>
        void Remove(Action<IDateTimeSplitDatabase> execute);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="ENTITY"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        ListResult<ENTITY> Select<ENTITY>(Query query, Func<IDateTimeSplitDatabase, long, IEnumerable<ENTITY>> dataExecute,
            Func<IDateTimeSplitDatabase, long> countExecute);
        /// <summary>
        /// 聚合计算，count
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long Count(Query query, Func<IDateTimeSplitDatabase,long> execute);
        /// <summary>
        /// 聚合计算 count
        /// </summary>
        /// <returns></returns>
        long Count(Func<IDateTimeSplitDatabase, long> execute);
    }
}
