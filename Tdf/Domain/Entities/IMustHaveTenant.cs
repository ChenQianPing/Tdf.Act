using System;

namespace Tdf.Domain.Entities
{
    public interface IMustHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        Guid TenantId { get; set; }
    }
}
