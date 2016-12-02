using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tdf.Domain.Repositories;

namespace Tdf.Act.EntityFramework.Repositories
{
    public class EfRepositoryBase<T> : IRepository<T> where T : class
    {
        // 定义数据访问上下文对象
        private ActDbContext _dbContext;

        public virtual DbSet<T> Table { get { return _dbContext.Set<T>(); } }

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public EfRepositoryBase(ActDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 获得实体的列表
        public IQueryable<T> GetAll()
        {
            return Table;
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllList()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// 获取实体集合（异步方式）
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合（异步方式）
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public async Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return Table.Where(predicate);
            }
            return Table.AsQueryable<T>();
        }
        #endregion

        #region 获取实体的列表，支持分页
        public IEnumerable<T> Get(string orderBy, int pageIndex, int pageSize)
        {
            return
                GetAll()
                .OrderBy(orderBy)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable<T>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">行数</param>
        /// <param name="pageSize">单页数据数</param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate,
            string orderBy, int pageIndex, int pageSize)
        {
            return
                GetAll().Where(predicate)
                .OrderBy(orderBy)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable<T>();
        }
        #endregion

        #region 获得单个实体
        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体（异步方式）
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }
        #endregion

        #region 插入
        public T Insert(T entity)
        {
            Table.Add(entity);
            return entity;
        }

        public Task<T> InsertAsync(T entity)
        {
            Table.Add(entity);
            return Task.FromResult(entity);
        }
        #endregion

        #region 更新
        public T Update(T entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<T> UpdateAsync(T entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        public void Delete(T entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public Task DeleteAsync(T entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="predicate">lambda表达式</param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            Delete(predicate);
        }

        #endregion

        #region 验证是否存在

        public virtual bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var entry = Table.Where(predicate);
            return (entry.Any());
        }

        public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var entry = Table.Where(predicate);
            return await Task.Run(() => entry.Any());
        }
        #endregion

        #region 其他
        public int Count()
        {
            return GetAll().Count();
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public long LongCount()
        {
            return GetAll().LongCount();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public long LongCount(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        #endregion

        protected virtual void AttachIfNot(T entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }
    }
}

