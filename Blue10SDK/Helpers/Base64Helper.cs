using System;

namespace Blue10SDK
{
    public class Base64Helper
    {
        public static byte[] GetBytesFromJsonResult(string pJsonContent)
        {
            var fBase64ContentArray = pJsonContent.Split(',');
            if (fBase64ContentArray.Length != 2) return null;
            var fBase64String = fBase64ContentArray[1];
            var fRes = Convert.FromBase64String(fBase64String);
            return fRes;
        }
    }
}
