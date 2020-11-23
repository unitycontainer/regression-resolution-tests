﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Regression
{
    /// <summary>
    /// Tests injecting dependencies by type
    /// </summary>
    /// <example>
    /// Container.RegisterType(target, new InjectionConstructor(type), 
    ///                                new InjectionMethod("Method", type) , 
    ///                                new InjectionField("Field", type), 
    ///                                new InjectionProperty("Property", type));
    /// </example>
    public abstract partial class InjectedPattern
    {
        #region Implicit

        [DataTestMethod]
        [DynamicData(nameof(RequiredImport_Data), typeof(PatternBase))]
        public virtual void ByType_Implicit_WithRequired(string test, Type type,
                                                         object @default, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
        {
            // Arrange
            var target = (TypeDefinition ??= GetType("Implicit", "BaselineTestType`1"))
                .MakeGenericType(type);

            Container.RegisterType(target, InjectionMember_Required_ByType(type));

            // Validate
            Assert.ThrowsException<ResolutionFailedException>(() => Container.Resolve(target, null));

            // Register missing types
            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }


        [DataTestMethod]
        [DynamicData(nameof(OptionalImport_Data), typeof(PatternBase))]
        public virtual void ByType_Implicit_WithOptional(string test, Type type,
                                                         object @default, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
        {
            PatternBaseType instance = null;
            // Arrange
            var target = (TypeDefinition ??= GetType("Implicit", "BaselineTestType`1"))
                .MakeGenericType(type);

            Container.RegisterType(target, InjectionMember_Optional_ByType(type));

            // Validate
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Expected, instance.Value);

            // Register missing types
            RegisterTypes();

            // Act
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }

        #endregion


        #region Required

        [DataTestMethod]
        [DynamicData(nameof(RequiredImport_Data), typeof(PatternBase))]
        public virtual void ByType_Required_WithRequired(string test, Type type,
                                                         object @default, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
        {
            // Arrange
            var target = (TypeDefinition ??= GetType("Annotated", "Required.BaselineTestType`1"))
                .MakeGenericType(type);

            Container.RegisterType(target, InjectionMember_Required_ByType(type));

            // Validate
            Assert.ThrowsException<ResolutionFailedException>(() => Container.Resolve(target, null));

            // Register missing types
            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }


        [DataTestMethod]
        [DynamicData(nameof(OptionalImport_Data), typeof(PatternBase))]
        public virtual void ByType_Required_WithOptional(string test, Type type,
                                                         object @default, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
        {
            PatternBaseType instance = null;

            // Arrange
            var target = (TypeDefinition ??= GetType("Annotated", "Required.BaselineTestType`1"))
                .MakeGenericType(type);

            Container.RegisterType(target, InjectionMember_Optional_ByType(type));

            // Validate
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Expected, instance.Value);

            // Register missing types
            RegisterTypes();

            // Act
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }

        #endregion


        #region Optional

        [DataTestMethod]
        [DynamicData(nameof(RequiredImport_Data), typeof(PatternBase))]
        public virtual void ByType_Optional_WithRequired(string test, Type type,
                                                         object defaultValue, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
        {
            // Arrange
            PatternBaseType instance = null;
            var target = (TypeDefinition ??= GetType("Annotated", "Optional.BaselineTestType`1"))
                .MakeGenericType(type);

            Container.RegisterType(target, InjectionMember_Required_ByType(type));

            // Unity v4 did not evaluate annotations on the dependency
            Assert.ThrowsException<ResolutionFailedException>(() => Container.Resolve(target, null));

            // Register missing types
            RegisterTypes();

            // Act
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(OptionalImport_Data), typeof(PatternBase))]
        public virtual void ByType_Optional_WithOptional(string test, Type type,
                                                         object @default, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
        {
            PatternBaseType instance = null;
            // Arrange
            var target = (TypeDefinition ??= GetType("Annotated", "Optional.BaselineTestType`1"))
                .MakeGenericType(type);

            Container.RegisterType(target, InjectionMember_Optional_ByType(type));

            // Validate
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Expected, instance.Value);

            // Register missing types
            RegisterTypes();

            // Act
            instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }

        #endregion
    }
}
