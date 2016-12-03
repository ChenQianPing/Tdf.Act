namespace Tdf.Domain.Entities.Auditing
{
    public interface IModificationAudited<T> : IHasModificationTime
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        T LastModifierUserId { get; set; }
    }
}
