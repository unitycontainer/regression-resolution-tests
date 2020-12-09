﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regression;
using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif


namespace Parameters
{
    public partial class Pattern
    {
        #region Success

#if !UNITY_V4
        [PatternTestMethod("Ctor() preserves annotated contract")]
        [DynamicData(nameof(Test_Variants_Data)), TestProperty(PARAMETER, nameof(OptionalParameter))]
        public void OptionalParameter(Type type, Type definition, string member, string import,
                                      Func<object, InjectionMember> func, object registered, object named,
                                      object injected, object @default, bool isNamed) 
            => Assert_Optionally_Injected(definition.MakeGenericType(type), 
                func(new OptionalParameter()), @default, import, isNamed, registered, named);
#endif

        [PatternTestMethod("Ctor(type) forces contract: type, null")]
        [DynamicData(nameof(Test_Variants_Data)), TestProperty(PARAMETER, nameof(OptionalParameter))]
        public void OptionalParameter_Type(Type type, Type definition, string member, string import,
                                           Func<object, InjectionMember> func, object registered, object named,
                                           object injected, object @default, bool isNamed)
            => Assert_Optionally_Injected(definition.MakeGenericType(type),
                func(new OptionalParameter(type)), @default, import, isNamed, registered, registered);


        [PatternTestMethod("Ctor(null) forces contract: AnnotatedType, null")]
        [DynamicData(nameof(Test_Variants_Data)), TestProperty(PARAMETER, nameof(OptionalParameter))]
        public void OptionalParameter_Null(Type type, Type definition, string member, string import,
                                           Func<object, InjectionMember> func, object registered, object named,
                                           object injected, object @default, bool isNamed)
            => Assert_Optionally_Injected(definition.MakeGenericType(type),
                func(new OptionalParameter((string)null)), @default, import, isNamed, registered, registered);


        [PatternTestMethod("Ctor(Name) forces contract: AnnotatedType, Name")]
        [DynamicData(nameof(Test_Variants_Data)), TestProperty(PARAMETER, nameof(OptionalParameter))]
        public void OptionalParameter_Name(Type type, Type definition, string member, string import,
                                           Func<object, InjectionMember> func, object registered, object named,
                                           object injected, object @default, bool isNamed)
            => Assert_Optionally_Injected(definition.MakeGenericType(type),
                func(new OptionalParameter(Name)), @default, import, isNamed, named, named);


        [PatternTestMethod("Ctor(type, null) forces contract: type, null")]
        [DynamicData(nameof(Test_Variants_Data)), TestProperty(PARAMETER, nameof(OptionalParameter))]
        public void OptionalParameter_Type_Null(Type type, Type definition, string member, string import,
                                           Func<object, InjectionMember> func, object registered, object named,
                                           object injected, object @default, bool isNamed)
            => Assert_Optionally_Injected(definition.MakeGenericType(type),
                func(new OptionalParameter(type, null)), @default, import, isNamed, registered, registered);


        [PatternTestMethod("Ctor(type, Name) forces contract: type, Name")]
        [DynamicData(nameof(Test_Variants_Data)), TestProperty(PARAMETER, nameof(OptionalParameter))]
        public void OptionalParameter_Type_Name(Type type, Type definition, string member, string import,
                                           Func<object, InjectionMember> func, object registered, object named,
                                           object injected, object @default, bool isNamed)
            => Assert_Optionally_Injected(definition.MakeGenericType(type),
                func(new OptionalParameter(type, Name)), @default, import, isNamed, named, named);

        #endregion


        #region Implementation

        private void Assert_Optionally_Injected(Type type, InjectionMember member, object @default, string import, bool isNamed, object registered, object named)
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
            var expected = !IMPLICIT.Equals(import) && isNamed ? named : registered;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion
    }
}
