﻿using Regression;
using System;
using System.Collections.Generic;
#if UNITY_V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Selection.Injected
{
    public abstract partial class Pattern : SelectionBase
    {
        #region Test Data

        public static IEnumerable<object[]> DefaultMemberTest_Data
        {
            get
            {
                yield return new object[] { typeof(IUnityContainer), typeof(object) };
                yield return new object[] { typeof(int), typeof(string) };
                yield return new object[] { typeof(string), typeof(int) };
                yield return new object[] { typeof(Unresolvable), typeof(IUnityContainer) };
            }
        }

        #endregion
    }
}
