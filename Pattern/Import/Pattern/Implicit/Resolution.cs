﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Import.Implicit
{
    public abstract partial class Pattern
    {
        [DataTestMethod, DynamicData(nameof(Import_Test_Data), typeof(ImportBase))]
        public virtual void Resolving_Import(string test, Type type,
                                                         object @default, object defaultAttr,
                                                         object registered, object named,
                                                         object injected, object overridden,
                                                         bool isResolveble)
            => TestRequiredImport(ImplicitImportType, type, registered);
    }
}
