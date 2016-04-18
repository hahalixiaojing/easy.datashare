using System;
using System.Data;

namespace Easy.DataShare.DateShare
{
    public class DefaultDateTimeSplitDatabase : IDateTimeSplitDatabase
    {
        public DefaultDateTimeSplitDatabase(DateTime start, DateTime end, int databaseIndex,string database)
        {
            this.Start = start.Date;
            this.End = end.Date;
            this.Index = databaseIndex;
            this.Database = database;
        }
        public virtual DateTime End
        {
            get;protected set;
        }
        public virtual DateTime Start
        {
            get; protected set;
        }
        public virtual int Index
        {
            get; protected set;
        }

        public virtual string Database
        {
            get;
            private set;
        }


        public bool IsSelected(DateTime datetime)
        {
            if (datetime.Date >= this.Start.Date && datetime.Date <= this.End.Date)
            {
                return true;
            }
            return false;
        }

        public bool IsSelected(DateTime start, DateTime end)
        {
            if ((this.Start.Date >= start.Date && this.Start.Date <=end.Date) || (this.End.Date >= start.Date && this.End.Date <= end.Date))
            {
                return true;
            }
            if(this.Start.Date >= start.Date && this.End.Date <= end.Date)
            {
                return true;
            }
            return false;
        }
    }
}
