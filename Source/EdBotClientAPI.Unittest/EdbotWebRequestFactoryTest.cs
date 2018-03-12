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

        [TestMethod]
        public void CreateRunMotion_ValidParameters_ReturnsString()
        {
            //act
            string runMotion = factory.CreateRunMotion("a", 1);

            //assert
            Assert.AreEqual("/api/motion/a/1", runMotion);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSetServoMode_NameIsNull_ThrowsException()
        {
            //act
            factory.CreateSetServoMode(null, "b");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSetServoMode_NameIsEmpty_ThrowsException()
        {
            //act
            factory.CreateSetServoMode(string.Empty, "b");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSetServoMode_PathIsNull_ThrowsException()
        {
            //act
            factory.CreateSetServoMode("a", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSetServoMode_PathIsEmpty_ThrowsException()
        {
            //act
            factory.CreateSetServoMode("a", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSetServoMode_PathStartsWithSlash_ThrowsException()
        {
            //act
            factory.CreateSetServoMode("a", "/b");
        }

        [TestMethod]
        public void CreateSetServoMode_ValidParameters_ReturnsString()
        {
            //act
           string servoMode = factory.CreateSetServoMode("a", "b");

            //assert
            Assert.AreEqual("/api/servo_mode/a/b", servoMode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSay_NameIsNull_ThrowsException()
        {
            //act
            factory.CreateSay(null, "b");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSay_NameIsEmpty_ThrowsException()
        {
            //act
            factory.CreateSay(string.Empty, "b");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSay_TextIsNull_ThrowsException()
        {
            //act
            factory.CreateSay("a", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateSay_TextIsEmpty_ThrowsException()
        {
            //act
            factory.CreateSay("a", string.Empty);
        }

        [TestMethod]
        public void CreateSay_ValidParameters_ReturnsString()
        {
            //act
            string say = factory.CreateSay("a", "b");

            //assert
            Assert.AreEqual("/api/say/a/b", say);
        }
    }
}
