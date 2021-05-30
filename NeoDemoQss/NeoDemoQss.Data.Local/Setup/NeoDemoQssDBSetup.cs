using NeoDemoQss.Data.Local.Extensions;
using NeoDemoQss.Data.Local.SQLConstants;
using SQLite;
using System;
using System.Threading.Tasks;

namespace NeoDemoQss.Data.Local.Setup
{
    public class NeoDemoQssDBSetup
    {
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(SqliteConstants.DatabasePath, SqliteConstants.Flags);
        });

        SQLiteAsyncConnection Database => lazyInitializer.Value;

        private bool isDBInitialized { get; set; } = false;
        private static NeoDemoQssDBSetup instance = null;

        private NeoDemoQssDBSetup()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            if (!isDBInitialized)
            {
                //if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Area).Name))
                //{
                //    await Database.CreateTablesAsync(CreateFlags.None, typeof(Area)).ConfigureAwait(false);
                //}

                isDBInitialized = true;
            }
        }

        private static NeoDemoQssDBSetup GetInstance()
        {
            if (instance == null)
            {
                instance = new NeoDemoQssDBSetup();
            }

            return instance;
        }

        public static SQLiteAsyncConnection GetDBInstance()
        {
            return GetInstance().Database;
        }
    }
}
