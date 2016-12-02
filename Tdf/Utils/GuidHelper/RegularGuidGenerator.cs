using System;

namespace Tdf.Utils.GuidHelper
{
    public class RegularGuidGenerator : IGuidGenerator
    {
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }

        public static string GenerateNewId()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}
