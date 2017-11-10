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
using System.Net;

namespace timeKeeper
{
    class CredentialProvider : ICredentialsProvider
    {
        public ICredentials GetCredentials(Uri uri, ICredentials failedCredentials)
        {
            throw new NotImplementedException();
        }

        public void NotifyCredentialsAuthenticated(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
