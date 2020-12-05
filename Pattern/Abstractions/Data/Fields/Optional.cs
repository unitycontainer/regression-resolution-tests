﻿using Regression;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Import.Optional.Fields
{
    #region Baseline

    public class ObjectTestType : FixtureBaseType
    {
        [OptionalDependency] public object Field;
        public override object Value { get => Field; }
        public override object Default => default(object);
    }

    public class BaselineTestType<TDependency> : FixtureBaseType
    {
        [OptionalDependency] public TDependency Field;
        public override object Value { get => Field; }
        public override object Default => default(TDependency);
    }

    public class BaselineTestTypeNamed<TDependency>
        : FixtureBaseType
    {
        [OptionalDependency(Pattern.Name)] public TDependency Field;

        public override object Value { get => Field; }
        public override object Default => default(TDependency);
    }

    #endregion


    #region No Public Members

    public class NoPublicMember<TDependency>
    {
#pragma warning disable IDE0052 // Remove unread private members
        [OptionalDependency] private TDependency Field;
#pragma warning restore IDE0052 // Remove unread private members
        protected TDependency Dummy()
        {
            Field = default;
            return Field;
        }
    }

    #endregion


    #region Array

    public class BaselineArrayType<TDependency> : FixtureBaseType
    {
        [OptionalDependency] public TDependency[] Field;
        public override object Value { get => Field; }
        public override object Default => default(TDependency);
    }

    #endregion


    #region Consumer

    public class BaselineConsumer<TDependency> : FixtureBaseType
    {
        public BaselineConsumer(BaselineTestType<TDependency> dependency, BaselineTestTypeNamed<TDependency> dummy)
        {
            Value = dependency.Value;
            Default = dummy.Value;
        }
    }

    #endregion
}
