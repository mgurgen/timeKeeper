using System;
using System.Collections.Generic;
using System.Linq;

using System.Collections.ObjectModel;

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;

namespace timeKeeper
{
    class TfsConnection
    {
        private Dictionary<string, List<string>> _projectTeamDictionary;
        public Dictionary<string, List<string>> ProjectTeamDictionary
        {
            get
            {
                return _projectTeamDictionary;
            }
        }

        public TfsConnection()
        {
            _projectTeamDictionary = new Dictionary<string, List<string>>();
            
            TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri("https://venus.tfs.siemens.net/tfs/tia"), new Credentials());
            tpc.EnsureAuthenticated();

            ICommonStructureService4 css = tpc.GetService<ICommonStructureService4>();

            var projectFromName = css.GetProjectFromName("TIA");

            TfsTeamService tts = tpc.GetService<TfsTeamService>();

            IEnumerable<TeamFoundationTeam> list = tts.QueryTeams(projectFromName.Uri);

            foreach (TeamFoundationTeam item in list)
            {
                Console.WriteLine(" Team: " + item.Name);

                List<TeamFoundationIdentity> members = item.GetMembers(tpc, MembershipQuery.Expanded).ToList();

                if (members.Exists(x => x.TeamFoundationId.Equals(tpc.AuthorizedIdentity.TeamFoundationId)))
                {
                    _projectTeamDictionary.Add(projectFromName.Name, new List<string> { item.Name });
                }
            }
        }
    }
}
