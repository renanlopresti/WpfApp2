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
                    NomeCompleto = "Renan Lopresti",
                    Idade = 25,
                    Cidade = "São Paulo",
                    Tentativas = new List<Tentativas>(),
                    Total = 0
                },
                new Biker()
                {
                    NomeCompleto = "Ian Spinoza",
                    Idade = 23,
                    Cidade = "São Carlos",
                    Tentativas = new List<Tentativas>(),
                    Total = 0
                },
                new Biker()
                {
                    NomeCompleto = "Bruno Lage",
                    Idade = 18,
                    Cidade = "Curitiba",
                    Tentativas = new List<Tentativas>(),
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
            var actual = listaBiker.ToList();

            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}