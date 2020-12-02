﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regression;
using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
#endif

namespace Import.Optional
{
    public abstract partial class Pattern : Import.Pattern
    {
        protected override void Assert_Resolution(Type type, object expected)
        {
            // Validate
            var instance = Container.Resolve(type, null) as FixtureBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Default, instance.Value);

            // Register missing types
            RegisterTypes();

            // Act
            instance = Container.Resolve(type, null) as FixtureBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(ImportBase))]
        public override void Injection_ByType(string test, Type type,
                                                         object defaultValue, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         object @default)
            => Assert_Injected(type, InjectionMember_Value(type), registered);


        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(ImportBase))]
        public override void InjectionParameter_ByType(string test, Type type, object defaultValue, object defaultAttr,
                                                      object registered, object named, object injected, object overridden,
                                                      object @default)
            => Assert_Injected(type, InjectionMember_Value(new InjectionParameter(type)), registered, @default);


        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(ImportBase))]
        public override void InjectionParameter_ByValue_Incompatible(string test, Type type, object defaultValue, object defaultAttr,
                                                                    object registered, object named, object injected, object overridden,
                                                                    object @default)
        {
            Assert_Injected(type, InjectionMember_Value(new InjectionParameter(type, type)), registered, @default);
            Assert.Fail();
        }
    }
}