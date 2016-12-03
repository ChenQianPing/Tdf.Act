using System;

namespace Tdf.Domain.Entities
{
    public interface IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        Guid? TenantId { get; set; }
    }
}
