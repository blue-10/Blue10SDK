using Blue10SDK;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class MeActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public MeActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public string GetMe()
        {
            var fRet = MBlue10Client.GetMe();
            return fRet;
        }
    }
}
