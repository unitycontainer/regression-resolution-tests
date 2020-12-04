﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regression;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_V4
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
#else
using Unity.Resolution;
#endif


namespace Import
{
    public abstract partial class Pattern
    {
        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(Pattern))]
        public virtual void Inject_Value(string test, Type type, object defaultValue, object defaultAttr,
                                           object registered, object named, object injected, object overridden,
                                           object @default) 
            => Assert_Injected(BaselineTestType.MakeGenericType(type), 
                               InjectionMember_Value(injected), 
                               injected, injected);

        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(Pattern))]
        public virtual void Inject_Named(string test, Type type, object defaultValue, object defaultAttr,
                                         object registered, object named, object injected, object overridden,
                                         object @default)
            => Assert_Injected(BaselineTestNamed.MakeGenericType(type),
                               InjectionMember_Value(injected),
                               injected, injected);


        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(Pattern))]
        public virtual void Inject_WithType(string test, Type type, object defaultValue, object defaultAttr,
                                            object registered, object named, object injected, object overridden,
                                            object @default)
            => Assert_UnregisteredThrows_RegisteredSuccess(
                BaselineTestType.MakeGenericType(type),
                InjectionMember_Value(type), registered);


#if !BEHAVIOR_V4 && !BEHAVIOR_V5
        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(Pattern))]
        public virtual void Inject_Array(string test, Type type, object defaultValue, object defaultAttr,
                                         object registered, object named, object injected, object overridden,
                                         object @default)
        {
            var instance = Array.CreateInstance(type, 1);
            ((IList)instance)[0] = @default;

            Assert_Array_Import(BaselineArrayType.MakeGenericType(type), InjectionMember_Value(instance), instance);
        }


        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(Pattern))]
        public virtual void Inject_Enumerable(string test, Type type, object defaultValue, object defaultAttr, 
                                              object registered, object named, object injected, object overridden, 
                                              object @default)
        {
            var instance = Array.CreateInstance(type, 1);
            ((IList)instance)[0] = @default;

            var target = BaselineTestType.MakeGenericType(typeof(IEnumerable<>).MakeGenericType(type));

            Assert_Injected(target, InjectionMember_Value(instance), instance, instance);
        }

#endif
        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Compatibility_Data), typeof(Pattern))]
        public virtual void Inject_Resolver(string test, Type type, object defaultValue, object defaultAttr,
                                            object registered, object named, object injected, object overridden,
                                            object @default)
            => Assert_Injected(BaselineTestType.MakeGenericType(type),
                               InjectionMember_Value(new ValidatingResolver(injected)),
                               injected, injected);
#if !UNITY_V4
        [TestCategory(CATEGORY_INJECT)]
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(Pattern))]
        public virtual void Inject_TypeFactory(string test, Type type, object defaultValue, object defaultAttr,
                                               object registered, object named, object injected, object overridden,
                                               object @default)
            => Assert_Injected(BaselineTestType.MakeGenericType(type),
                               InjectionMember_Value(new ValidatingResolverFactory(injected)),
                               injected, injected);
#endif
    }
}
