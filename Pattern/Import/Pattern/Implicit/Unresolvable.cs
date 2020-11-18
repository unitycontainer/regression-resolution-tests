﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Regression
{
    public abstract partial class ImplicitPattern
    {
        /// <summary>
        /// Tests dependency resolution from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     public int Field;
        /// 
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public PocoType(int value) { }
        ///     
        ///     [InjectionMethod]
        ///     public void Method(int value) { }
        /// }
        /// 
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Implicit_Unregistered_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Implicit_Unregistered(string test, Type type, string name, Type dependency, object expected)
        {
            // Act
            _ = Container.Resolve(type, name) as PatternBaseType;
        }

        // Test Data
        public static IEnumerable<object[]> Implicit_Unregistered_Data
        {
            get
            {
                var poco_type = GetType("Implicit_Dependency_Generic`1");
                var Poco_Value      = poco_type.MakeGenericType(typeof(int));
                var Poco_Ref        = poco_type.MakeGenericType(typeof(Unresolvable));
                var Poco_Struct     = poco_type.MakeGenericType(typeof(TestStruct));

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name       Type            Name    Dependency              Expected         
                                                                                                                                 
                yield return new object[] { "Value",        Poco_Value,     null,   typeof(int),            RegisteredInt    };
                yield return new object[] { "Class",        Poco_Ref,       null,   typeof(Unresolvable),   RegisteredUnresolvable        };
                yield return new object[] { "Struct",       Poco_Struct,    null,   typeof(TestStruct),     RegisteredStruct };
                                                                                                                                 
                yield return new object[] { "Value_Named",  Poco_Value,     Name,   typeof(int),            RegisteredInt    };
                yield return new object[] { "Class_Named",  Poco_Ref,       Name,   typeof(Unresolvable),   RegisteredUnresolvable        };
                yield return new object[] { "Class_Null",   Poco_Ref,       Null,   typeof(Unresolvable),   RegisteredUnresolvable        };
            }
        }


        /// <summary>
        /// Tests invalid parameter types
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     [InjectionConstructor]
        ///     public PocoType(ref int value)
        ///     
        ///     [InjectionMethod]
        ///     public void Method(out int value)
        /// }
        /// 
        /// </example>
        /// <param name="name">Type name</param>
        [DataTestMethod]
        [DataRow("Implicit_Dependency_Ref")]
        [DataRow("Implicit_Dependency_Out")]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Implicit_Parameters(string name)
        {
            var type = GetType(name);

            // Make dependencies available
            RegisterTypes();

            // Act
            _ = Container.Resolve(type);
        }

    }
}
