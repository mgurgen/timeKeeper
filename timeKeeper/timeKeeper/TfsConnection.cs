using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // Connect to Team Foundation Server
        //     Server is the name of the server that is running the application tier for Team Foundation.
        //     Port is the port that Team Foundation uses. The default port is 8080.
        //     VDir is the virtual path to the Team Foundation application. The default path is tfs.

        /*
        static string tfsServerUri = "https://venus.tfs.siemens.net/tfs/tia/TIA/";

        static Uri tfsUri = (tfsServerUri.Length < 1) ?
            new Uri("http://Server:Port/VDir") : new Uri(tfsServerUri);

        static TfsConfigurationServer configurationServer =
            TfsConfigurationServerFactory.GetConfigurationServer(tfsUri);

        ReadOnlyCollection<CatalogNode> collectionNodes = configurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);
        */
        public TfsConnection()
        {
            Credentials credentials = new Credentials();
            

            TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri("https://venus.tfs.siemens.net/tfs/tia"), credentials);
            tpc.EnsureAuthenticated();

            ICommonStructureService4 css = tpc.GetService<ICommonStructureService4>();
            var projectFromName = css.GetProjectFromName("TIA");

            TfsTeamService tts = tpc.GetService<TfsTeamService>();

            IEnumerable<TeamFoundationTeam> list = tts.QueryTeams(projectFromName.Uri);
            
            ReadOnlyCollection<CatalogNode> collectionNodes = tpc.ConfigurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);


            // List the team project collections
            foreach (CatalogNode collectionNode in collectionNodes)
            {
                // Use the InstanceId property to get the team project collection
                //Guid collectionId = new Guid(collectionNode.Resource.Properties["InstanceId"]);
                //TfsTeamProjectCollection teamProjectCollection = tpc.ConfigurationServer.GetTeamProjectCollection(collectionId);

                // Print the name of the team project collection
                //Console.WriteLine("Collection: " + teamProjectCollection.Name);

                // Get a catalog of team projects for the collection
                ReadOnlyCollection<CatalogNode> projectNodes = collectionNode.QueryChildren(
                    new[] { CatalogResourceTypes.TeamProject },
                    false, CatalogQueryOptions.None);

                // List the team projects in the collection
                foreach (CatalogNode projectNode in projectNodes)
                {
                    Console.WriteLine(" Team Project: " + projectNode.Resource.DisplayName);
                }
            }


        }
    }
}
