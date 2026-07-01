using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DbContext;
namespace Repository
{
    public abstract class BaseRepository
    {
        protected readonly string ConnectionString;

        protected BaseRepository(
            DbConnectionSettings db)
        {
            ConnectionString = db.ConnectionString;
        }
    }
}
