﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registration.Lifetime
{
    [TestClass]
    public class PerContainer : Manager.Lifetime.Pattern
    {
        #region Scaffolding

        [TestInitialize]
        public override void TestInitialize() => base.TestInitialize();

        [ClassInitialize]
        public static void ClassInit(TestContext context) => ClassInitialize(context);

        protected override LifetimeManager GetManager() => new ContainerControlledLifetimeManager();

        #endregion

        [TestMethod]
        public void Baseline() { }
    }
}