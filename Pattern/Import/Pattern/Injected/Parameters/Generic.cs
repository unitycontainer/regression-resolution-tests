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
    public abstract partial class InjectedPattern
    {
        #region Passing

        /// <summary>
        /// Tests injecting dependencies by generic parameter
        /// </summary>
        /// <example>
        /// Container.RegisterType(target, new InjectionConstructor(new GenericParameter(T)), 
        ///                                new InjectionMethod("Method", new GenericParameter(T)) , 
        ///                                new InjectionField("Field",   new GenericParameter(T)), 
        ///                                new InjectionProperty("Property", new GenericParameter(T)));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Data))]
        public virtual void Injected_ByGeneric(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType) return;

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            RegisterTypes();

            Container.RegisterType(definition, GetGenericMember(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion


        #region Required

        /// <summary>
        /// Tests injecting by generic parameter required dependencies from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Injected_ByGeneric_Required(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType)
#if UNITY_V4
                throw new ResolutionFailedException(type, name, null, null);
#else
                throw new ResolutionFailedException(type, name, "Not Generic");
#endif

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            Container.RegisterType(definition, GetGenericMember(dependency, name));

            // Act
            _ = Container.Resolve(target) as PatternBaseType;
        }

        #endregion


        #region Optional

        /// <summary>
        /// Tests injecting optional dependencies by generic parameter from empty container
        /// </summary>
        /// <remarks>
        /// The injection member overrides Optional attribute with Optional dependency.
        /// </remarks>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Data))]
        public virtual void Injected_ByGeneric_Optional(string test, Type type, string name, Type dependency, object expected)
        {
#if UNITY_V4
            if (!type.IsGenericType)
                throw new ResolutionFailedException(type, name, null, null);
#else
            if (!type.IsGenericType)
                throw new ResolutionFailedException(type, name, "Not Generic");
#endif
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            Container.RegisterType(definition, GetGenericOptional(dependency, name));

            // Act
            var value = Container.Resolve(target) as PatternBaseType;

            Assert.IsNotNull(value);
            Assert.AreEqual(value.Value, expected);
        }

        #endregion


        #region With Default

        /// <summary>
        /// Tests injecting by generic parameter dependencies with default from empty container
        /// </summary>
        /// <remarks>
        /// Injected member overrides existing default and either is resolved or throws
        /// </remarks>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Default_Data))]
        public virtual void Injected_ByGeneric_Default(string test, Type type, string name, Type dependency, object expected)
        {
            if (!type.IsGenericType) return;

            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;

            Type definition = type.IsGenericTypeDefinition
                            ? type
                            : type.GetGenericTypeDefinition();
            // Arrange
            RegisterTypes();

            Container.RegisterType(definition, GetGenericMember(dependency, name));

            // Act
            var instance = Container.Resolve(target) as PatternBaseType;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        #endregion
    }
}