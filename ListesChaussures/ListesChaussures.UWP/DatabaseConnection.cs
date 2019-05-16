using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Xamarin.Forms;
using Windows.Storage;
using System.IO;
//using XamarinFormsDatabase.UWP;


[assembly: Dependency(typeof(ListesChaussures.UWP.DatabaseConnection))]
namespace ListesChaussures.UWP
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "CustomersDb.db3";

            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);

            var connection = new SQLiteConnection(path);

            return connection;
        }

    }
}
