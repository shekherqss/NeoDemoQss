using NeoDemoQss.Data.Local.Repository.Contract;
using NeoDemoQss.Data.Local.Setup;
using SQLite;

namespace NeoDemoQss.Data.Local.Repository.Implementation
{
    public class SQLliteRepository : ISQLliteRepository
    {
        SQLiteAsyncConnection database;
        public SQLliteRepository()
        {
            database = NeoDemoQssDBSetup.GetDBInstance();
        }

        #region Area Methods
        //public async Task<bool> DeleteArea()
        //{
        //    try
        //    {
        //        await database.DropTableAsync<Area>();
        //        await database.CreateTableAsync<Area>();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> SaveAreas(List<Area> list)
        //{
        //    bool isSuccess = false;

        //    try
        //    {
        //        await database.InsertAllAsync(list);
        //        isSuccess = true;
        //    }
        //    catch
        //    {
        //        isSuccess = false;
        //    }

        //    return isSuccess;
        //}

        //public async Task<List<Area>> GetAreas()
        //{
        //    return await database.Table<Area>().ToListAsync();
        //}
        #endregion
    }
}
