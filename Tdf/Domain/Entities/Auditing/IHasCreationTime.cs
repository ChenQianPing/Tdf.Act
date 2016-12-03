using System;

namespace Tdf.Domain.Entities.Auditing
{
    /// <summary>
    /// IHasCreationTime使得使用一个通用的属性来描述一个实体的“创建时间”信息成为可能。
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
