using System;
using System.Web.UI;
using System.Web.UI.Adapters;

    public class ServerSideViewStateAdapter : PageAdapter
    {
        public override PageStatePersister GetStatePersister()
        {
            return new SessionPageStatePersister(this.Page);
        }
    }