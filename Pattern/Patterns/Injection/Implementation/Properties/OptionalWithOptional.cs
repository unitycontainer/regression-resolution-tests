﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regression;
using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
#endif

namespace Properties
{
    [TestClass]
    public partial class Injecting_Optional_With_Optional : Injection.Optional.Pattern
    {
        #region Properties

        protected override string DependencyName => "Property";

        #endregion


        #region Scaffolding

        [TestInitialize]
        public override void TestInitialize() => base.TestInitialize();

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            ClassInitialize(context);

            Type support = Type.GetType($"{typeof(FixtureBase).FullName}+{Member}");

            if (support is null) return;

            // Override injectors with optional
            InjectionMember_Value = (Func<object, InjectionMember>)support
                .GetMethod("GetInjectionValueOptional").CreateDelegate(typeof(Func<object, InjectionMember>));

            InjectionMember_Default = (Func<InjectionMember>)support
                .GetMethod("GetInjectionDefaultOptional").CreateDelegate(typeof(Func<InjectionMember>));

            InjectionMember_Contract = (Func<Type, string, InjectionMember>)support
                .GetMethod("GetInjectionContractOptional").CreateDelegate(typeof(Func<Type, string, InjectionMember>));
        }

        #endregion


        #region Overrides

        protected override void Assert_Injection(Type type, InjectionMember member, object @default, object expected)
        {
            // Inject
            Container.RegisterType(null, type, null, null, member);

            // Act
            var instance = Container.Resolve(type, null) as FixtureBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(@default, instance.Value);

            // Register missing types
            RegisterTypes();

            // Act
            instance = Container.Resolve(type, null) as FixtureBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region Special Cases
        #endregion
    }
}