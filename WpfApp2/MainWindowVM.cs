using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp2.Entidades.DataBases;
using WpfApp2.Entidades;
using WpfApp2.Utilidades;

namespace WpfApp2
{
    internal class MainWindomVM : BaseNotify
    {

        private IDataBase connection;

        public List<Biker> ListaBikers { get; set; }

        public List<Tentativas> ListaTentativas { get; set; }


        private Biker _bikerSelected;

        public Biker BikerSelected
        {
            get { return _bikerSelected; }
            set { _bikerSelected = value; if (_bikerSelected != null) { ListaTentativas = _bikerSelected.Tentativas; Notifica(nameof(BikerSelected)); Notifica(nameof(ListaTentativas)); } }
        }

        public Tentativas TentativaSelected { get; set; }

        public ICommand AddNewBiker { get; private set; }

        public ICommand RemoverBiker { get; private set; }

        public ICommand EditarBiker { get; private set; }

        public ICommand AddTentativa { get; private set; }

        public ICommand RemoverTentativa { get; private set; }


        public MainWindomVM()
        {
            try
            {
                connection = new DataBaseManager(new Postgres());
                this.ListaBikers = AtualizaLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }

            Notifica(nameof(ListaBikers));

            StartComand();
        }

        public List<Biker> AtualizaLista()
        {

            List<Biker> newList = connection.LerBikers();

            foreach (Biker b in newList)
            {
                List<Tentativas> newListTentativa = connection.LerTentativas(b.Inscricao);
                b.Tentativas = newListTentativa;
            }

            return newList;
        }


        public void StartComand()
        {
            AddNewBiker = new RelayCommand((object _) =>
            {

                Biker newBiker = new Biker();

                AddBiker tela = new AddBiker();
                tela.DataContext = newBiker;
                bool? verifica = tela.ShowDialog();
                if (verifica.HasValue && verifica.Value)
                {
                    try
                    {
                        connection.AdicionaBiker(newBiker);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    this.ListaBikers = AtualizaLista();
                    Notifica(nameof(ListaBikers));
                }

            });

            EditarBiker = new RelayCommand((object _) =>
            {

                Biker newBiker = this.BikerSelected.Clone() as Biker;

                EditarBiker tela = new EditarBiker();
                tela.DataContext = newBiker;
                bool? verifica = tela.ShowDialog();
                if (verifica.HasValue && verifica.Value)
                {
                    try
                    {
                        connection.EditarBiker(newBiker);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    this.ListaBikers = AtualizaLista();
                    this.ListaTentativas = null;
                    Notifica(nameof(ListaBikers));
                    Notifica(nameof(ListaTentativas));
                }
            }, (object _) => this.BikerSelected != null);


            RemoverBiker = new RelayCommand((object _) =>
            {
                try
                {
                    connection.RemoverBiker(this.BikerSelected.Inscricao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.ListaBikers = AtualizaLista();
                this.ListaTentativas = null;
                Notifica(nameof(ListaBikers));
                Notifica(nameof(ListaTentativas));

            }, (object _) => this.BikerSelected != null);


            AddTentativa = new RelayCommand((object _) =>
            {
                Tentativas newTentativa = new Tentativas();
                Biker newBiker = this.BikerSelected.Clone() as Biker;

                AddTentativa tela = new AddTentativa();
                tela.DataContext = newTentativa;
                bool? verifica = tela.ShowDialog();
                if (verifica.HasValue && verifica.Value)
                {
                    try
                    {
                        newBiker.Total += newTentativa.Valor;
                        connection.AdicionaTentativa(newTentativa, this.BikerSelected.Inscricao);
                        connection.EditarBiker(newBiker);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    this.ListaBikers = AtualizaLista();
                    this.ListaTentativas = null;
                    Notifica(nameof(ListaBikers));
                    Notifica(nameof(ListaTentativas));
                    Notifica(nameof(BikerSelected));
                }

            }, (object _) => this.BikerSelected != null);

            RemoverTentativa = new RelayCommand((object _) =>
            {
                Biker newBiker = this.BikerSelected.Clone() as Biker;

                try
                {
                    newBiker.Total -= this.TentativaSelected.Valor;
                    connection.RemoverTentativa(newBiker, TentativaSelected);
                    connection.EditarBiker(newBiker);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.ListaBikers = AtualizaLista();
                this.ListaTentativas = null;

                Notifica(nameof(ListaBikers));
                Notifica(nameof(ListaTentativas));
                Notifica(nameof(BikerSelected));

            }, (object _) => this.BikerSelected != null && this.TentativaSelected != null);


        }
    }
}
