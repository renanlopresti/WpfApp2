using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Entidades.DataBases
{
    public interface IDataBase
    {
        List<Biker> LerBikers();

        List<Tentativas> LerTentativas(int id);

        void AdicionaBiker(Biker newBiker);

        void EditarBiker(Biker newBiker);

        void RemoverBiker(int id);

        void AdicionaTentativa(Tentativas newTentativas, int id);

        void RemoverTentativa(Biker biker, Tentativas newTentativa);

    }
}
