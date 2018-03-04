namespace EdbotClientAPI.Unittest
{
    using Autofac.Extras.Moq;
    using EdbotClientAPI.Communication.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class EdbotClientTest
    {
        [TestMethod]
        public void Constructor()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var sut = mock.Create<EdbotClient>();
                mock.Mock<IWebClient>().Verify(x => x.Connect(), Times.Once);
            }
        }
    }
}
