using System;

namespace Easy.DataShare.DateShare
{
    public class Query
    {

        public Query()
        {
            this.OrderBy = OrderBy.DESC;
            this.PageSize = 20;
        }
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码，第一页从1开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 订单排序类型
        /// </summary>
        public OrderBy OrderBy { get; set; }
    }

    public enum OrderBy
    {
        DESC,
        ASC
    }
}
