using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace ListesChaussures.UWP
{
    [Table("Chaussures")]
    public class itemChaussures : INotifyPropertyChanged
    {       
        private int _id;
        [Column("Id"), PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }        

        private string _nomMarque;
        [Column("NomMarque"), NotNull]
        public string NomMarque
        {
            get
            {
                return _nomMarque;
            }
            set
            {
                this._nomMarque = value;
                OnPropertyChanged(nameof(NomMarque));
            }
        }


        private string _serieChaussure;
        [Column("SerieChaussure"), MaxLength(50)]
        public string SerieChaussure
        {
            get
            {
                return _serieChaussure;
            }
            set
            {
                this._serieChaussure = value;
                OnPropertyChanged(nameof(SerieChaussure));
            }
        }

        private decimal _prix;  
        [Column("Prix")]
        public decimal Prix
        {
            get
            {
                return _prix;
            }
            set
            {
                _prix = value;
                OnPropertyChanged(nameof(Prix));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
