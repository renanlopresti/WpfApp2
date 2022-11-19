using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WpfApp2.Entidades.DataBases
{
    public class Postgres : IDataBase
    {
        private string user = "postgres";
        private string password = "postgres";
        private string host = "localhost";
        private string port = "5432";
        private string dataBase = "postgres";
        private NpgsqlConnection connection;
        private NpgsqlCommand command;
        private NpgsqlDataReader dr;

        public Postgres()
        {
            connection = new NpgsqlConnection($"User ID = {user}; Password={password};Host={host};Port={port};Database={dataBase};");
            command = new NpgsqlCommand();
            command.Connection = connection;
        }

        private void Conectar()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Desconectar()
        {
            connection.Close();
        }


        public List<Biker> LerBikers()
        {
            Conectar();
            List<Biker> listBikers = new List<Biker>();


            command.CommandText = "Select * From Bike Order By Total";
            using (dr = command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listBikers.Add(new Biker(
                            dr.GetString(1),
                            dr.GetInt32(2),
                            dr.GetString(3),
                            dr.GetInt32(0),
                            dr.GetInt32(4)
                            ));
                    }
                }
            }
            Desconectar();
            return listBikers;
        }

        public List<Tentativas> LerTentativas(int id)
        {
            Conectar();
            List<Tentativas> listTentativas = new List<Tentativas>();

            command.CommandText = "Select * From tentativa Where idbiker = " + id + " Order By manobra";
            using (dr = command.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listTentativas.Add(new Tentativas(
                            dr.GetInt32(0),
                            dr.GetString(1),
                            dr.GetInt32(2)
                            ));
                    }
                }

            }
            Desconectar();
            return listTentativas;
        }

        public void AdicionaBiker(Biker newBiker)
        {

            command.CommandText = $@"INSERT INTO Biker(nomeCompleto, idade, cidade, total) 
                                      VALUES ('{newBiker.NomeCompleto}', {newBiker.Idade}, '{newBiker.Cidade}', {newBiker.Total});";
            ExecuteCommand(command);
        }

        public void EditarBiker(Biker newBiker)
        {
            command.CommandText = $@"UPDATE  biker 
                                        SET nomeCompleto= '{newBiker.NomeCompleto}',idade={newBiker.Idade},cidade='{newBiker.Cidade}',total={newBiker.Total}
                                        WHERE id={newBiker.Inscricao};";
            ExecuteCommand(command);
        }

        public void RemoverBiker(int id)
        {
            command.CommandText = $@"DELETE FROM biker WHERE id={id};";
            ExecuteCommand(command);
        }

        public void AdicionaTentativa(Tentativas newTentativas, int id)
        {

            command.CommandText = $@"INSERT INTO Tentativa(manobra, valor, idBiker) 
                                      VALUES ('{newTentativas.Manobra}', {newTentativas.Valor}, '{id}');";
            ExecuteCommand(command);

        }

        public void RemoverTentativa(Biker newBiker, Tentativas newTentativas)
        {

            command.CommandText = $@"DELETE FROM Tentativa WHERE id={newTentativas.Id};";
            ExecuteCommand(command);
        }

        private void ExecuteCommand(NpgsqlCommand newCommand)
        {
            Conectar();
            try
            {
                newCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Desconectar();
            }
        }

    }

}
