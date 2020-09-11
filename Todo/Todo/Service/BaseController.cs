using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Todo.Helper;
using Todo.Model;

namespace Todo.Service
{
    public abstract class BaseController<T> where T : BaseEntity, new()
    {

        public SQLiteAsyncConnection Connection
        {
            get;
            private set;
        }

        public BaseController()
        {
            Connection = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    Constants.LOCAL_DB_NAME),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            Connection.CreateTableAsync<T>().Wait();
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await Connection.Table<T>().ToListAsync();
        }

        public async Task<int> Save(T entity)
        {
            return await Connection.InsertAsync(entity);
        }

        public async Task<int> Update(T entity)
        {
            return await Connection.UpdateAsync(entity);
        }

        public async Task<T> FindByIdAsync(object Id) 
        { 
             return await Connection.Table<T>().Where(
                i => i.Id.Equals(Id)).FirstOrDefaultAsync();
        }

        public async Task<int> RemoveAllAsync()
        {
            return await Connection.DeleteAllAsync<T>();
        }

        public async Task<int> RemoveAsync(T entity)
        {
            return await Connection.DeleteAsync(entity);
        }

    }
}
