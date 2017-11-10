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
using Microsoft.VisualStudio.Services.Common;

namespace timeKeeper
{
    class Credentials : Microsoft.VisualStudio.Services.Client.VssClientCredentials
    {
        Microsoft.VisualStudio.Services.Common.WindowsCredential vCRed = new Microsoft.VisualStudio.Services.Common.WindowsCredential();
    }
}
