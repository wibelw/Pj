using System;

namespace Inversion.Api.IG.Client.Common
{
    public interface PropertyEventDispatcher
    {
        void BeginInvoke(Action a);

        void addEventMessage(string message);
    }
}

