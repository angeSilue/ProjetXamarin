using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace ListesChaussures.UWP
{
    class ChaussuresDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<itemChaussures> Chaussures { get; set; }

        public ChaussuresDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            database.CreateTable<itemChaussures>();

            this.Chaussures = new ObservableCollection<itemChaussures>
            (database.Table<itemChaussures>());

            // If the table is empty, initialize the collection
            if (!database.Table<itemChaussures>().Any())
            {
                AddNewChaussures();
            }
        }

        public void AddNewChaussures()
        {
            this.Chaussures.
                Add(new itemChaussures
                {
                    NomMarque = "Nike",
                    SerieChaussure = "AirMax",
                    Prix = 255.06m
                });

            this.Chaussures.
                Add(new itemChaussures
                {
                    NomMarque = "Adidas",
                    SerieChaussure = "SuperStar",
                    Prix = 175.89m
                });


            this.Chaussures.
                Add(new itemChaussures
                {
                    NomMarque = "Aldo",
                    SerieChaussure = "Loafer Henri",
                    Prix = 89.76m
                });

            this.Chaussures.
                Add(new itemChaussures
                {
                    NomMarque = "Vans",
                    SerieChaussure = "Chauffeur SF",
                    Prix = 75.80m
                });
        }

        // Use LINQ to query and filter data
        public IEnumerable<itemChaussures> GetFilteredChaussures(string nomMarque)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from cust in database.Table<itemChaussures>()
                            where cust.NomMarque == nomMarque
                            select cust;
                return query.AsEnumerable();
            }
        }

        // Use SQL queries against data
        public IEnumerable<itemChaussures> GetFilteredChaussures()
        {
            lock (collisionLock)
            {
                return database.Query<itemChaussures>
                ("SELECT * FROM itemChaussures WHERE NomMarque = 'Adidas'").AsEnumerable();
            }
        }

        public itemChaussures GetChaussures(int id)
        {
            lock (collisionLock)
            {
                return database.Table<itemChaussures>().
                    FirstOrDefault(Chaussures => Chaussures.Id == id);
            }
        }


        public int SaveChaussures(itemChaussures ChaussuresInstance)
        {
            lock (collisionLock)
            {
                if (ChaussuresInstance.Id != 0)
                {
                    database.Update(ChaussuresInstance);
                    return ChaussuresInstance.Id;
                }
                else
                {
                    database.Insert(ChaussuresInstance);
                    return ChaussuresInstance.Id;
                }
            }
        }

        public void SaveAllChaussures()
        {
            lock (collisionLock)
            {
                foreach (var ChaussuresInstance in this.Chaussures)
                {
                    if (ChaussuresInstance.Id != 0)
                    {
                        database.Update(ChaussuresInstance);
                    }
                    else
                    {
                        database.Insert(ChaussuresInstance);
                    }
                }
            }
        }

        public int DeleteChaussures(itemChaussures ChaussuresInstance)
        {
            var id = ChaussuresInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<itemChaussures>(id);
                }
            }
            this.Chaussures.Remove(ChaussuresInstance);
            return id;
        }


        public void DeleteAllChaussuress()
        {
            lock (collisionLock)
            {
                database.DropTable<itemChaussures>();
                database.CreateTable<itemChaussures>();
            }
            this.Chaussures = null;
            this.Chaussures = new ObservableCollection<itemChaussures>
            (database.Table<itemChaussures>());
        }
    }
}
