using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace WpfApp2.Entidades.DataBases
{
    public class MySQLDB : IDataBase
    {
        private string hostName = "127.0.0.1";
        private string port = "3306";
        private string user = "root";
        private string password = "@Renan8422";
        private string dataBase = "local";
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader dr;

        public MySQLDB()
        {
            this.connection = new MySqlConnection($"server={hostName};user={user};database={dataBase};port={port};password={password};");
            command = new MySqlCommand();
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

            command.CommandText = "Select * From Biker ORDER BY total DESC";
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

        private void ExecuteCommand(MySqlCommand newCommand)
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
