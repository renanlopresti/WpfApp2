using WpfApp2.Entidades.DataBases;
using WpfApp2.Entidades;
using Moq;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public List<Biker> MockBikers()
        {
            List<Biker> lista = new List<Biker>()
            {
                new Biker()
                {
                    Inscricao = 1,
                    NomeCompleto = "Renan Lopresti",
                    Idade = 25,
                    Cidade = "São Paulo",
                    Total = 0
                },
                new Biker()
                {
                    Inscricao = 2,
                    NomeCompleto = "Ian Spinoza",
                    Idade = 23,
                    Cidade = "São Carlos",
                    Total = 0
                },
                new Biker()
                {
                    Inscricao = 3,
                    NomeCompleto = "Bruno Lage",
                    Idade = 18,
                    Cidade = "Curitiba",
                    Total = 0
                }
            };
            return lista;
        }

        [Test]
        public void TestGetAllBiker()
        {
            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock
                .Setup(x => x.LerBikers())
                .Returns(MockBikers());

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            List<Biker> listaBiker = new List<Biker>(dbManager.LerBikers());

            var expected = MockBikers();
            var actual = listaBiker;

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(expected[i].Inscricao, Is.EqualTo(actual[i].Inscricao));
                Assert.That(expected[i].NomeCompleto, Is.EqualTo(actual[i].NomeCompleto));
                Assert.That(expected[i].Idade, Is.EqualTo(actual[i].Idade));
                Assert.That(expected[i].Cidade, Is.EqualTo(actual[i].Cidade));
                Assert.That(expected[i].Total, Is.EqualTo(actual[i].Total));
            }
        }

        [Test]
        public void CriarBikerCorreto()
        {
            Biker newBiker = new Biker()
            {
                NomeCompleto = "Renan",
                Idade = 25,
                Cidade = "São Paulo"
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaBiker(newBiker));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            dbManager.AdicionaBiker(newBiker);

            dbMock.Verify(x => x.AdicionaBiker(newBiker));
        }

        [Test]
        public void CriarBikerComNomeNulo()
        {
            Biker newBiker = new Biker()
            {
                NomeCompleto = null,
                Idade = 25,
                Cidade = "São Paulo"
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaBiker(newBiker));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            Assert.That(() => dbManager.AdicionaBiker(newBiker),
                Throws.Exception
                .TypeOf<Exception>()
                .With.Message.EqualTo("Campo nome não pode ser nulo"));
        }

        [Test]
        public void CriarBikerComIdadeMenorQue13Anos()
        {
            Biker newBiker = new Biker()
            {
                NomeCompleto = "Renan",
                Idade = 12,
                Cidade = "São Paulo"
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaBiker(newBiker));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            Assert.That(() => dbManager.AdicionaBiker(newBiker),
                Throws.Exception
                .TypeOf<Exception>()
                .With.Message.EqualTo("Atletas menores do que 13 anos não podem se inscrever"));
        }

        [Test]
        public void CriarBikerComCidadeNulo()
        {
            Biker newBiker = new Biker()
            {
                NomeCompleto = "Renan",
                Idade = 12,
                Cidade = null
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaBiker(newBiker));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            Assert.That(() => dbManager.AdicionaBiker(newBiker),
                Throws.Exception
                .TypeOf<Exception>()
                .With.Message.EqualTo("Campo cidade não pode ser nulo"));
        }

        [Test]
        public void CriarTentativasCorreto()
        {
            Tentativas newTentativa = new Tentativas()
            {
                Manobra = "BarSpin",
                Valor = 50
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaTentativa(newTentativa, 1));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            dbManager.AdicionaTentativa(newTentativa, 1);

            dbMock.Verify(x => x.AdicionaTentativa(newTentativa, 1));
        }

        [Test]
        public void CriarTentativasComManobraNulo()
        {
            Tentativas newTentativa = new Tentativas()
            {
                Manobra = null,
                Valor = 50
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaTentativa(newTentativa, 1));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            dbManager.AdicionaTentativa(newTentativa, 1);

            Assert.That(() => dbManager.AdicionaTentativa(newTentativa, 1),
                Throws.Exception
                .TypeOf<Exception>()
                .With.Message.EqualTo("Campo manobra não pode ser nulo"));
        }

        [Test]
        public void CriarTentativasComValor0()
        {
            Biker newBiker = new Biker()
            {
                Inscricao = 1,
                NomeCompleto = "Renan",
                Idade = 12,
                Cidade = null
            };

            Tentativas newTentativa = new Tentativas()
            {
                Manobra = "BarSpin",
                Valor = 0
            };

            Mock<IDataBase> dbMock = new Mock<IDataBase>();

            dbMock.Setup(x => x.AdicionaTentativa(newTentativa, 1));

            DataBaseManager dbManager = new DataBaseManager(dbMock.Object);

            dbManager.AdicionaTentativa(newTentativa, 1);

            Assert.That(() => dbManager.AdicionaTentativa(newTentativa, 1),
                Throws.Exception
                .TypeOf<Exception>()
                .With.Message.EqualTo("Valor da manobra não pode ser menor ou igual a 0"));
        }

    }
}