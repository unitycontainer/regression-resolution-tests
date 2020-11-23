﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Regression.Implicit
{
    public abstract partial class Pattern
    {
        [DataTestMethod]
        [DynamicData(nameof(ResolvableTypes_Data), typeof(PatternBase))]
        public virtual void Registered_Resolvable_Type(string test, Type type)
        {
            // Arrange
            RegisterTypes();

            // Act
            var instance = Container.Resolve(type, null);

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, type);
        }


        [DataTestMethod]
        [DynamicData(nameof(ResolvableTypes_Data), typeof(PatternBase))]
        public virtual void Registered_Resolvable_Import(string test, Type type)
        {
            // Arrange
            RegisterTypes();

            var target = (TypeDefinition ??= GetType("BaselineTestType`1"))
                .MakeGenericType(type);

            // Act
            var instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, target);
        }

        /// <summary>
        /// Tests exception throwing on unsatisfactory resolution.
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(UnResolvableTypes_Data), typeof(PatternBase))]
        public virtual void Registered_UnResolvable_Type(string test, Type type)
        {
            // Arrange
            RegisterUnResolvableTypes();

            // Act
            var instance = Container.Resolve(type, null);

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, type);
            Assert.AreEqual(Container.Resolve(type, null), instance);
        }


        /// <summary>
        /// Tests exception throwing on unsatisfactory resolution.
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(UnResolvableTypes_Data), typeof(PatternBase))]
        public virtual void Registered_UnResolvable_Import(string test, Type type)
        {
            // Arrange
            RegisterUnResolvableTypes();

            var target = (TypeDefinition ??= GetType("BaselineTestType`1"))
                .MakeGenericType(type);

            // Act
            var instance = Container.Resolve(target, null) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, target);
            Assert.AreEqual(Container.Resolve(type, null), instance.Value);
        }
    }
}