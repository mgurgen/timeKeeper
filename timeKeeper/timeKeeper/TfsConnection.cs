using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation;

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

            try
            {
                TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(Settings.TfsConnectionUri), new Credentials());
                tpc.EnsureAuthenticated();

                ICommonStructureService4 css = tpc.GetService<ICommonStructureService4>();

                List<ProjectInfo> projectList = new List<ProjectInfo>();
                if (Settings.ProjectNames.IsNullOrEmpty())
                {
                    projectList = css.ListAllProjects().ToList();
                }
                else
                {
                    string[] projectNames = Settings.ProjectNames.Split(',');
                    foreach (string projectName in projectNames)
                    {
                        projectList.Add(css.GetProjectFromName(projectName));
                    }
                }

                foreach (ProjectInfo projectInfo in projectList)
                {
                    TfsTeamService tts = tpc.GetService<TfsTeamService>();
                    IEnumerable<TeamFoundationTeam> list = tts.QueryTeams(projectInfo.Uri);

                    foreach (TeamFoundationTeam item in list)
                    {
                        Console.WriteLine(" Team: " + item.Name);

                        List<TeamFoundationIdentity> members = item.GetMembers(tpc, MembershipQuery.Expanded).ToList();
                        if (members.Exists(x => x.TeamFoundationId.Equals(tpc.AuthorizedIdentity.TeamFoundationId)))
                        {
                            _projectTeamDictionary.Add(projectInfo.Name, new List<string> { item.Name });
                        }
                    }
                }
            }
            catch (TeamFoundationServiceUnavailableException tfsUnavailableException)
            {
                throw new TeamFoundationServiceUnavailableException(tfsUnavailableException.Message);
            }
        }
    }
}
