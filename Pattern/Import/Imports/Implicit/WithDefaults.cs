﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regression;
using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Import.Implicit
{
    public abstract partial class Pattern
    {
#if BEHAVIOR_V4
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        [DataTestMethod, DynamicData(nameof(WithDefaultValue_Data))]
        /// <summary>
        /// Tests providing default values
        /// </summary>
        public virtual void WithDefaultValue(string test, Type type)
        {
            // Act
            var instance = Container.Resolve(type, null) as ImportBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, type);
            Assert.AreEqual(instance.Default, instance.Value);
        }


#if BEHAVIOR_V4 || BEHAVIOR_V5
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        [DataTestMethod, DynamicData(nameof(WithDefaultAttribute_Data))]
        /// <summary>
        /// Tests providing default values
        /// </summary>
        public virtual void WithDefaultAttribute(string test, Type type)
        {
            // Act
            var instance = Container.Resolve(type, null) as ImportBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, type);
            Assert.AreEqual(instance.Default, instance.Value);
        }


#if BEHAVIOR_V4
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        [DataTestMethod, DynamicData(nameof(WithDefaultAndAttribute_Data))]
        /// <summary>
        /// Tests providing default values
        /// </summary>
        public virtual void WithDefaultAndAttribute(string test, Type type)
        {
            // Act
            var instance = Container.Resolve(type, null) as ImportBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, type);
            Assert.AreEqual(instance.Default, instance.Value);
        }
    }
}