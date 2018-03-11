namespace EdbotClientAPI.Unittest
{
    using EdbotClientAPI.Communication.Requests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class EdbotWebRequestFactoryTest
    {
        private EdbotWebRequestFactory factory;

        [TestInitialize]
        public void Setup()
        {
            factory = new EdbotWebRequestFactory();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateRunMotion_NameIsNull_ThrowsException()
        {
            //act
            factory.CreateRunMotion(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateRunMotion_NameIsEmpty_ThrowsException()
        {
            //act
            factory.CreateRunMotion(string.Empty, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateRunMotion_NegativeMotionNumber_ThrowsException()
        {
            //act
            factory.CreateRunMotion(null, -1);
        }
    }
}
