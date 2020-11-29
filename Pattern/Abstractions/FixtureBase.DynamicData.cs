﻿using System;
using System.Collections.Generic;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Regression
{
    public abstract partial class FixtureBase
    {
        public static IEnumerable<Type> Unsupported_Types
        {
            get
            {
                yield return typeof(Unresolvable);
                yield return typeof(string);

#if !BEHAVIOR_V4 // Unity v4 did not support optional value types
                yield return typeof(int);
                yield return typeof(bool);
                yield return typeof(long);
                yield return typeof(short);
                yield return typeof(float);
                yield return typeof(double);
                // TODO: typeof(TestStruct)
#endif                                            
                yield return typeof(List<>);
                yield return typeof(Type);
                yield return typeof(ICloneable);
                yield return typeof(Delegate);
            }
        }

        public static IEnumerable<Type> Supported_Types
        {
            get
            {
                yield return typeof(object);
                yield return typeof(Lazy<IUnityContainer>);
                yield return typeof(Func<IUnityContainer>);
                yield return typeof(object[]);
#if !BEHAVIOR_V4
                yield return typeof(List<int>);
                yield return typeof(List<string>);
                yield return typeof(IEnumerable<IUnityContainer>);
#endif
            }
        }

        #region Type Based Data

        public static IEnumerable<object[]> BuiltInTypes_Data
        {
            get
            {
                yield return new object[] { typeof(IUnityContainer).Name, typeof(IUnityContainer) };
#if !UNITY_V4 && !UNITY_V5
                yield return new object[] { typeof(IUnityContainerAsync).Name, typeof(IUnityContainerAsync) };
                yield return new object[] { typeof(IServiceProvider).Name, typeof(IServiceProvider) };
#endif
            }
        }

        public static IEnumerable<object[]> InvalidTypes_Data
        {
            get
            {
                foreach (var type in Unsupported_Types)
                {
                    yield return new object[] { type.Name, type };
                }
            }
        }

        public static IEnumerable<object[]> Unsupported_Data
        {
            get
            {
                foreach (var type in Unsupported_Types)
                {
                    if (type.IsGenericTypeDefinition) continue;
                    yield return new object[] { type.Name, type };
                }
            }
        }

        public static IEnumerable<object[]> SupportedTypes_Data
        {
            get
            {
                foreach (var type in Supported_Types)
                {
                    yield return new object[] { type.Name, type };
                }
            }
        }

        #endregion
    }
}

