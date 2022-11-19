using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Entidades.DataBases
{
    public class DataBaseManager : IDataBase
    {
        readonly IDataBase DataBase;

        public DataBaseManager(IDataBase DataBase)
        {
            this.DataBase = DataBase;
        }

        public void AdicionaBiker(Biker newBiker)
        {
            if (newBiker.NomeCompleto == null) throw new Exception("Campo nome não pode ser nulo");
            if (newBiker.NomeCompleto.Length <= 0) throw new ArgumentNullException("Campo nome não pode ser nulo");
            if (newBiker.Cidade == null) throw new Exception("Campo cidade não pode ser nulo");
            if (newBiker.Cidade.Length <= 0) throw new ArgumentNullException("Campo cidade não pode ser nulo");
            if (newBiker.Idade < 13) throw new Exception("Atletas menores do que 13 anos não podem se inscrever");

            DataBase.AdicionaBiker(newBiker);
        }

        public void AdicionaTentativa(Tentativas newTentativas, int id)
        {
            if (newTentativas.Manobra == null) throw new Exception("Campo manobra não pode ser nulo");
            if (newTentativas.Valor <= 0) throw new Exception("Valor da manobra não pode ser menor ou igual a 0");

            DataBase.AdicionaTentativa(newTentativas, id);
        }

        public void EditarBiker(Biker newBiker)
        {
            if (newBiker.NomeCompleto.Length <= 0) throw new ArgumentNullException("Campo nome não pode ser nulo");
            if (newBiker.Cidade.Length <= 0) throw new Exception("Campo cidade não pode ser nulo");
            if (newBiker.Idade < 13) throw new Exception("Atletas menores do que 13 anos não podem se inscrever");

            DataBase.EditarBiker(newBiker);
        }

        public List<Biker> LerBikers()
        {
            return DataBase.LerBikers();
        }

        public List<Tentativas> LerTentativas(int id)
        {
            return DataBase.LerTentativas(id);
        }

        public void RemoverBiker(int id)
        {
            DataBase.RemoverBiker(id);
        }

        public void RemoverTentativa(Biker biker, Tentativas newTentativa)
        {
            Biker newBiker = (Biker)biker.Clone();
            newBiker.Total -= newTentativa.Valor;

            DataBase.RemoverTentativa(biker, newTentativa);
            DataBase.EditarBiker(newBiker);
        }

    }
}
