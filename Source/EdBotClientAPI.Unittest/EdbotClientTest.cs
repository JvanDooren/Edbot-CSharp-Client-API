namespace EdbotClientAPI.Unittest
{
    using Autofac.Extras.Moq;
    using EdbotClientAPI.Communication.Requests;
    using EdbotClientAPI.Communication.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Net;

    [TestClass]
    public class EdbotClientTest
    {
        [TestMethod]
        public void Constructor_ConnectNotCalled()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //act
                var sut = mock.Create<EdbotClient>();

                //verify
                mock.Mock<IWebClient>().Verify(x => x.Connect(), Times.Never);
            }
        }

        [TestMethod]
        public void Connect_ConnectCalled()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //act
                var sut = mock.Create<EdbotClient>();
                sut.Connect();

                //verify
                mock.Mock<IWebClient>().Verify(x => x.Connect(), Times.Once);
            }
        }

        [TestMethod]
        public void RunMotion_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateRunMotion("name", 1)).Returns("runmotion");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.RunMotion("name", 1);

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateRunMotion("name", 1), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "runmotion"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoMode_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoMode("name", "path")).Returns("setservomode");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoMode("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoMode("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservomode"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoTorque_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoTorque("name", "path")).Returns("setservotorque");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoTorque("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoTorque("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservotorque"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoLED_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoLED("name", "path")).Returns("setservoled");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoLED("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoLED("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservoled"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoSpeed_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoSpeed("name", "path")).Returns("setservospeed");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoSpeed("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoSpeed("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservospeed"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoPosition_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoPosition("name", "path")).Returns("setservoposition");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoPosition("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoPosition("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservoposition"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoPID_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoPID("name", "path")).Returns("setservopid");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoPID("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoPID("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservopid"), Times.Once);
            }
        }

        [TestMethod]
        public void SetServoCombined_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetServoCombined("name", "path")).Returns("setservocombined");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetServoCombined("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetServoCombined("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setservocombined"), Times.Once);
            }
        }

        [TestMethod]
        public void SetBuzzer_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetBuzzer("name", "path")).Returns("setbuzzer");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetBuzzer("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetBuzzer("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setbuzzer"), Times.Once);
            }
        }

        [TestMethod]
        public void Say_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSay("name", "text")).Returns("say");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.Say("name", "text");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSay("name", "text"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "say"), Times.Once);
            }
        }

        [TestMethod]
        public void Reset_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateReset("name")).Returns("reset");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.Reset("name");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateReset("name"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "reset"), Times.Once);
            }
        }

        [TestMethod]
        public void SetOptions_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetOptions("name", "path")).Returns("setoptions");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetOptions("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetOptions("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setoptions"), Times.Once);
            }
        }

        [TestMethod]
        public void SetCustom_SendsRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //prepare
                mock.Mock<IRequestFactory>().Setup(x => x.CreateSetCustom("name", "path")).Returns("setcustom");

                //act
                var sut = mock.Create<EdbotClient>();
                sut.SetCustom("name", "path");

                //verify
                mock.Mock<IRequestFactory>().Verify(x => x.CreateSetCustom("name", "path"), Times.Once);
                mock.Mock<IWebClient>().Verify(x => x.Send(It.IsAny<WebHeaderCollection>(), "setcustom"), Times.Once);
            }
        }
    }
}
