﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Methods
{
    [TestClass]
    public class Selecting_Annotated : Selection.Annotated.Pattern
    {
        #region Properties
        protected override string DependencyName => "value";

        #endregion


        #region Scaffolding

        [TestInitialize]
        public override void TestInitialize() => base.TestInitialize();

        [ClassInitialize]
        public static void ClassInit(TestContext context) => ClassInitialize(context);

        #endregion
    }
}