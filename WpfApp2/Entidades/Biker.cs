using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Utilidades;

namespace WpfApp2.Entidades
{
    public class Biker : BaseNotify, ICloneable
    {

        private int inscricao;
        private string nomeCompleto;
        private string cidade;
        private int idade;
        private List<Tentativas> tentativas;
        private int total;

        public Biker()
        {
        }

        public Biker(string nomeCompleto, int age, string city, int inscricao)
        {
            this.nomeCompleto = nomeCompleto;
            this.cidade = city;
            this.idade = age;
            this.total = 0;
            this.inscricao = inscricao;
            this.tentativas = new List<Tentativas>();
        }

        public Biker(string nomeCompleto, int age, string city, int inscricao, int total)
        {
            this.nomeCompleto = nomeCompleto;
            this.cidade = city;
            this.idade = age;
            this.total = total;
            this.inscricao = inscricao;
            this.tentativas = new List<Tentativas>();
        }

        public int Inscricao { get { return inscricao; } set { inscricao = value; Notifica(nameof(Inscricao)); } }

        public string NomeCompleto { get { return nomeCompleto; } set { nomeCompleto = value; Notifica(nameof(NomeCompleto)); } }

        public string Cidade { get { return cidade; } set { cidade = value; Notifica(nameof(Cidade)); } }

        public int Idade { get { return idade; } set { idade = value; Notifica(nameof(Idade)); } }

        public List<Tentativas> Tentativas { get { return tentativas; } set { tentativas = value; Notifica(nameof(Tentativas)); } }

        public int Total { get { return total; } set { total = value; Notifica(nameof(Total)); } }


        public void AddTrick(Tentativas tentativa)
        {

            this.tentativas.Add(tentativa);
            this.total += tentativa.Valor;

        }

        public object Clone()
        {
            return (Biker)this.MemberwiseClone();
        }



    }
}
