using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Utilidades;

namespace WpfApp2.Entidades
{
    public class Tentativas : BaseNotify, ICloneable
    {
        private int id;
        private String manobra;
        private int valor;

        public int Id { get { return id; } set { id = value; Notifica(nameof(Id)); } }

        public String Manobra { get { return manobra; } set { manobra = value; Notifica(nameof(Manobra)); } }

        public int Valor { get { return valor; } set { valor = value; Notifica(nameof(Valor)); } }

        public Tentativas()
        {

        }
        public Tentativas(int id, String trick, int valor)
        {
            this.id = id;
            this.manobra = trick;
            this.valor = valor;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Atualizar(Tentativas newTentativa)
        {
            Manobra = newTentativa.Manobra;
            Valor = newTentativa.Valor;
        }
    }
}
