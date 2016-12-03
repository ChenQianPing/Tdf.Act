namespace Tdf.Domain.Entities.Auditing
{
    /// <summary>
    /// 如果你想为一个实体实现所有的审计接口（创建，修改和删除），
    /// 那么可以直接实现IFullAudited,因为它继承了所有的这些接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFullAudited<T> : IAudited<T>
        , IDeletionAudited<T>
    {
    }
}
