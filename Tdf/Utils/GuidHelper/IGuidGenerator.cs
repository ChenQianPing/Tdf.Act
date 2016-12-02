using System;

namespace Tdf.Utils.GuidHelper
{
    /// <summary>
    /// Used to generate Ids.
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// Creates a GUID.
        /// </summary>
        /// <returns></returns>
        Guid Create();
    }
}
