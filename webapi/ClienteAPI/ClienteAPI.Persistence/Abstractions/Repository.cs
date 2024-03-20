using ClienteAPI.Domain.Core.Interfaces;
using ClienteAPI.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;

namespace ClienteAPI.Persistence.Abstractions
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected DbContext Db;
        protected DbSet<TEntity> DbSet;
        private readonly ILogger<Repository<TEntity>>? _logger;

        public Repository(DbContext context, ILogger<Repository<TEntity>> logger)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
            _logger = logger;
        }

        public void Add(TEntity obj)
        {
            try
            {
                obj.CreateAt = DateTime.UtcNow;
                obj.UpdateAt = DateTime.UtcNow;
                DbSet.Add(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public ICollection<TEntity> FindAll(params string[] includes)
        {
            _logger!.LogInformation($"Obtendo lista de {GetType().Name}");
            var query = DbSet.AsNoTracking();
            query = Includes(query, includes);

            var retorno = query.ToList();
            _logger!.LogInformation($"Lista de {GetType().Name} obtida");
            return retorno;
        }

        public ICollection<TEntity> FindAllWhere(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            _logger!.LogInformation($"Obtendo lista de {GetType().Name}");
            var query = DbSet.AsNoTracking().Where(predicate);
            query = Includes(query, includes);
            _logger!.LogInformation($"Lista de {GetType().Name} obtida");
            return query.ToList();
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            _logger!.LogInformation($"Obtendo {GetType().Name}");
            var query = DbSet.AsNoTracking().Where(predicate);
            query = Includes(query, includes);
            _logger!.LogInformation($"{GetType().Name} obtido");
            return query.FirstOrDefault();
        }

        public TEntity? GetById(Guid uuid, params string[] includes)
        {
            _logger!.LogInformation($"Obtendo {GetType().Name}");
            var query = DbSet.AsNoTracking().Where(e => e.Uuid == uuid);
            query = Includes(query, includes);
            _logger!.LogInformation($"{GetType().Name}, id {uuid} obtido");
            return query.FirstOrDefault();
        }

        public void Remove(Guid uuid)
        {
            var obj = GetById(uuid);
            if (obj! != null!)
            {
                obj!.Removed = true;
                Update(obj);
            }
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();

        }

        public void Update(TEntity obj)
        {
            _logger!.LogInformation($"Atualizando objeto {GetType().Name}, id {obj.Uuid}");
            obj.UpdateAt = DateTime.UtcNow;
            DbSet.Update(obj);
        }

        IQueryable<TEntity> Includes(IQueryable<TEntity> query, params string[] includes)
        {
            if (includes != null)
            {
                foreach (var item in includes)
                    query = query.Include(item);
            }

            return query;
        }

        protected object ConvertTableToObject(KeyValuePair<DataTable, string> retorno)
        {
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;

            foreach (DataRow row in retorno.Key.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in retorno.Key.Columns)
                    childRow.Add(col.ColumnName, row[col]);

                parentRow.Add(childRow);
            }

            return parentRow;
        }

        public async Task<ICollection<TEntity>> FindAllWhereAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var list = new List<TEntity>();
            _logger!.LogInformation($"Obtendo lista de {GetType().Name}");
            var query = DbSet.AsNoTracking().Where(predicate);
            query = Includes(query, includes);
            _logger!.LogInformation($"Lista de {GetType().Name} obtida");

            await Task.Run(() => list = query.ToList());

            return list;
        }

        public async Task<TEntity?> GetByIdAsync(Guid uuid, params string[] includes)
        {
            _logger!.LogInformation($"Obtendo {GetType().Name}, id {uuid}");
            IQueryable<TEntity> query = null!;
            await Task.Run(() => query = DbSet.AsNoTracking().Where(e => e.Uuid == uuid && e.Removed == false));
            query = Includes(query, includes);
            _logger!.LogInformation($"{GetType().BaseType}, id {uuid} obtido");
            return query.FirstOrDefault();
        }

        public void Remove(TEntity obj)
        {
            if (obj! != null!)
            {
                obj!.Removed = true;
                Update(obj);
            }
        }


    }
}
