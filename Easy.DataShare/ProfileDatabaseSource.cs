using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.DataShare.DateShare;
using Easy.Profile;

namespace Easy.DataShare
{
    public class ProfileDatabaseSource : IDatabaseSource
    {
        private string application;
        private string profileName;
        public ProfileDatabaseSource(string application,string profileName)
        {
            this.application = application;
            this.profileName = profileName;
        }

        public IList<DefaultDateTimeSplitDatabase> GetDataSource()
        {
            return ProfileManager.Instance.GetConfigData<IList<DefaultDateTimeSplitDatabase>>(this.application, this.profileName).GetProfileData();
        }
    }
}
