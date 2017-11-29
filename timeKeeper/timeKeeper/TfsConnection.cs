using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Server;

namespace timeKeeper
{
    class TfsConnection
    {
        private Dictionary<ProjectInfo, List<TeamFoundationTeam>> _projectTeamDictionary;
        public Dictionary<ProjectInfo, List<TeamFoundationTeam>> ProjectTeamDictionary
        {
            get
            {
                return _projectTeamDictionary;
            }
        }

        public TfsConnection()
        {
            _projectTeamDictionary = new Dictionary<ProjectInfo, List<TeamFoundationTeam>>();

            try
            {
                TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(Settings.TfsConnectionUri), new Credentials());
                tpc.EnsureAuthenticated();

                ICommonStructureService4 css = tpc.GetService<ICommonStructureService4>();

                if (Settings.ProjectNames.IsNullOrEmpty())
                {
                    _projectTeamDictionary 
                        = css.ListAllProjects()
                        .GroupBy(projectInfo => projectInfo)
                        .ToDictionary(projectInfo => projectInfo.Key, projectInfo => new List<TeamFoundationTeam>());
                }
                else
                {
                    string[] projectNames = Settings.ProjectNames.Split(',');
                    foreach (string projectName in projectNames)
                    {
                        if (!_projectTeamDictionary.ContainsKey(css.GetProjectFromName(projectName)))
                            _projectTeamDictionary.Add(css.GetProjectFromName(projectName), new List<TeamFoundationTeam>());
                    }
                }

                foreach (ProjectInfo projectInfo in _projectTeamDictionary.Keys)
                {
                    TfsTeamService tts = tpc.GetService<TfsTeamService>();
                    List<TeamFoundationTeam> teamList = new List<TeamFoundationTeam>();

                    teamList = tts.QueryTeams(projectInfo.Uri).ToList();

                    if (Settings.TeamNames.IsNullOrEmpty())
                    {
                        foreach (TeamFoundationTeam team in teamList)
                        {
                            Console.WriteLine(" Team: " + team.Name);

                            List<TeamFoundationIdentity> members = team.GetMembers(tpc, MembershipQuery.Expanded).ToList();
                            if (members.Exists(member => member.TeamFoundationId.Equals(tpc.AuthorizedIdentity.TeamFoundationId)))
                            {
                                _projectTeamDictionary[projectInfo].Add(team);
                            }
                        }
                    }
                    else
                    {
                        string[] teamNames = Settings.TeamNames.Split(',');
                        foreach (string teamName in teamNames)
                        {
                            _projectTeamDictionary[projectInfo].AddRange(teamList.FindAll(x => x.Name.Equals(teamName)));
                        }
                    }
                }
            }
            catch (Microsoft.TeamFoundation.TeamFoundationServiceUnavailableException tfsUnavailableException)
            {
                throw new Microsoft.TeamFoundation.TeamFoundationServiceUnavailableException(tfsUnavailableException.Message);
            }
        }
    }
}
