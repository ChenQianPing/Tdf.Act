namespace Tdf.Domain.Entities.Auditing
{
    public interface IDeletionAudited<T> : IHasDeletionTime
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        T DeleterUserId { get; set; }
    }
}
