namespace Tdf.Domain.Entities.Auditing
{
    public interface IAudited<T> : ICreationAudited<T>
        , IModificationAudited<T>
    {
    }
}
