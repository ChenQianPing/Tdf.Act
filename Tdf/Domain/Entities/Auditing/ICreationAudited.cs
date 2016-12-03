namespace Tdf.Domain.Entities.Auditing
{
    public interface ICreationAudited<T> : IHasCreationTime
    {
        /// <summary>
        /// Id of the creator user of this entity.
        /// </summary>
        T CreatorUserId { get; set; }
    }
}
