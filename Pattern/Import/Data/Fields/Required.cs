﻿using Regression;
using System;
using System.ComponentModel;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Import.Annotated.Fields.Required
{
    public class BaselineTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] public TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class BaselineTestTypeNamed<TDependency>
        : PatternBaseType
    {
        [Dependency(ImportBase.Name)] public TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class BaselineInheritedType<TDependency>
        : BaselineTestType<TDependency>
    {
    }

    public class BaselineInheritedTwice<TDependency>
        : BaselineInheritedType<TDependency>
    {
    }

    public class DownTheLineType<TDependency>
        : PatternBaseType
    {
        public DownTheLineType(BaselineTestType<TDependency> import)
            => Value = import;
    }

    public class ArrayTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] public TDependency[] Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class PrivateTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] private TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class ProtectedTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] protected TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }

    public class InternalTestType<TDependency>
        : PatternBaseType
    {
        [Dependency] internal TDependency Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => default(TDependency);
    }
}


namespace Import.Annotated.Fields.Required.WithDefaults
{
    #region WithDefault

    // Unity does not support implicit default values on fields
    // When resolved it will throw if not registered
    //
    //public class Required_WithDefault : PatternBaseType
    //{
    //    [Dependency] public int Field = PatternBase.DefaultInt;
    //}

    #endregion

#if !BEHAVIOR_V5 // Unity v5 did not support DefaultValueAttribute on fields

    #region WithDefaultAttribute

    public class Required_Field_Int_WithDefaultAttribute : PatternBaseType
    {
        [Dependency] [DefaultValue(ImportBase.DefaultValueInt)] public int Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_WithDefaultAttribute_Int : PatternBaseType
    {
        [DefaultValue(ImportBase.DefaultValueInt)] [Dependency] public int Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_String_WithDefaultAttribute : PatternBaseType
    {
        [Dependency] [DefaultValue(ImportBase.DefaultValueString)] public string Field;
        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Field_WithDefaultAttribute_String : PatternBaseType
    {
        [DefaultValue(ImportBase.DefaultValueString)] [Dependency] public string Field;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Derived_WithDefaultAttribute : Required_Field_Int_WithDefaultAttribute
    {
    }

    #endregion


    #region WithDefaultAndAttribute

    public class Required_Field_Int_WithDefaultAndAttribute : PatternBaseType
    {
        [Dependency] [DefaultValue(ImportBase.DefaultValueInt)] public int Field = ImportBase.DefaultInt;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_WithDefaultAndAttribute_Int : PatternBaseType
    {
        [DefaultValue(ImportBase.DefaultValueInt)] [Dependency] public int Field = ImportBase.DefaultInt;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueInt;
        public override Type ImportType => typeof(int);
    }

    public class Required_Field_String_WithDefaultAndAttribute : PatternBaseType
    {
        [Dependency] [DefaultValue(ImportBase.DefaultValueString)] public string Field = ImportBase.DefaultString;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Field_WithDefaultAndAttribute_String : PatternBaseType
    {
        [DefaultValue(ImportBase.DefaultValueString)] [Dependency] public string Field = ImportBase.DefaultString;

        public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        public override object Default => ImportBase.DefaultValueString;
        public override Type ImportType => typeof(string);
    }

    public class Required_Derived_WithDefaultAndAttribute : Required_Field_Int_WithDefaultAndAttribute
    {
    }

    #endregion

#else

    public class Dummy_Success_Type : PatternBaseType
    {
        public override object Value { get => null; }
        public override object Default => null;
    }

#endif

}
