﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manager;
using System;
using System.Threading;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Registration
{
    [TestClass]
    public partial class IsRegistered : FixtureBase
    {
        private string other = "other";

        #region Scaffolding

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Container.RegisterType<ILogger, MockLogger>();
            Container.RegisterType<ILogger, MockLogger>(Name);

            var service = new Service();
            Container.RegisterInstance<IService>(service);
            Container.RegisterInstance<IService>(Name, service);

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context) => ClassInitialize(context);

        #endregion


        #region Test Data

        public interface ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }

        #endregion
    }
}
