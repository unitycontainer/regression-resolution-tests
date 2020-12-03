﻿using System;
using System.ComponentModel;
using static Import.Pattern;
using Regression;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Import.Required.Fields
{
    #region Validation

    public class PrivateTestType<TDependency>
        : FixtureBaseType
    {
        [Dependency] private TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
        protected TDependency Dummy() => (Field = default);
    }

    public class ProtectedTestType<TDependency>
        : FixtureBaseType
    {
        [Dependency] protected TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class InternalTestType<TDependency>
        : FixtureBaseType
    {
        [Dependency] internal TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
        protected TDependency Dummy() => (Field = default);
    }

    #endregion
}


namespace Import.Optional.Fields.WithDefault
{
    // Unity does not support implicit default values on properties
    // When resolved it will throw if not registered
    //
    //public class Required_WithDefault : PatternBaseType
    //{
    //    [Dependency] public TDependency Field;
    //}
}


namespace Import.Required.Fields.WithDefaultAttribute
{
#if !BEHAVIOR_V5 // Unity v5 did not support DefaultValueAttribute on fields

    public class Required_Field_Int_WithDefaultAttribute : ImportBaseType
    {
        [Dependency] [DefaultValue(Pattern.DefaultValueInt)] public int Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_WithDefaultAttribute_Int : ImportBaseType
    {
        [DefaultValue(Pattern.DefaultValueInt)] [Dependency] public int Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_String_WithDefaultAttribute : ImportBaseType
    {
        [Dependency] [DefaultValue(Pattern.DefaultValueString)] public string Field;
        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Field_WithDefaultAttribute_String : ImportBaseType
    {
        [DefaultValue(Pattern.DefaultValueString)] [Dependency] public string Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Derived_WithDefaultAttribute : Required_Field_Int_WithDefaultAttribute
    {
    }

#endif
}


namespace Import.Required.Fields.WithDefaultAndAttribute
{
#if !BEHAVIOR_V5 // Unity v5 did not support DefaultValueAttribute on fields

    public class Required_Field_Int_WithDefaultAndAttribute : ImportBaseType
    {
        [Dependency] [DefaultValue(Pattern.DefaultValueInt)] public int Field = Pattern.DefaultInt;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_WithDefaultAndAttribute_Int : ImportBaseType
    {
        [DefaultValue(Pattern.DefaultValueInt)] [Dependency] public int Field = Pattern.DefaultInt;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_String_WithDefaultAndAttribute : ImportBaseType
    {
        [Dependency] [DefaultValue(Pattern.DefaultValueString)] public string Field = Pattern.DefaultString;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Field_WithDefaultAndAttribute_String : ImportBaseType
    {
        [DefaultValue(Pattern.DefaultValueString)] [Dependency] public string Field = Pattern.DefaultString;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => Pattern.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Derived_WithDefaultAndAttribute : Required_Field_Int_WithDefaultAndAttribute
    {
    }
#endif
}
