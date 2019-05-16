using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace ListesChaussures.UWP
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
