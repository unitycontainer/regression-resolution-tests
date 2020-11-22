﻿using System;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
#endif

namespace Constructors
{
    public static class Support
    {
        public static InjectionMember GetByNameMember(Type _)
            => throw new NotSupportedException();

        public static InjectionMember GetByNameOptional(Type _)
            => throw new NotSupportedException();

        public static InjectionMember GetResolvedMember(Type importType, string contractName)
            => new InjectionConstructor(new ResolvedParameter(importType, contractName));

        public static InjectionMember GetOptionalMember(Type importType, string contractName)
            => new InjectionConstructor(new OptionalParameter(importType, contractName));

        public static InjectionMember GetOptionalOptional(Type importType, string contractName)
            => new InjectionConstructor(new OptionalParameter(importType, contractName));

        public static InjectionMember GetGenericMember(Type _, string contractName)
            => new InjectionConstructor(new GenericParameter("T", contractName));

        public static InjectionMember GetGenericOptional(Type importType, string contractName)
            => new InjectionConstructor(new OptionalGenericParameter("T", contractName));

        public static InjectionMember GetInjectionValue(object argument)
            => new InjectionConstructor(argument);

        public static InjectionMember GetInjectionOptional(object argument)
            => new InjectionConstructor(argument);
    }
}
