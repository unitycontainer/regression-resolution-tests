﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Regression;
using System;
using System.Collections.Generic;
#if UNITY_V4
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Lifetime.Manager
{
    public abstract partial class Pattern
    {
        [PatternTestMethod("SetValue(...) ignores lifetime on object"), TestCategory(LIFETIME_MANAGER)]
        [DynamicData(nameof(Lifetime_Disposable_Data))]
        public void SetValueIgnoresLifetimeOnObject(string name, Func<LifetimeManager> factory, bool isDisposable)
        {
            // Arrange
            var scope = new LifetimeContainer();
            object instance = new object();

            // Act
            var manager = factory();
            manager.SetTestValue(instance, scope);

            // Validate object does not add IDIsposable
            var expected = manager is HierarchicalLifetimeManager ? 1 : 0;
            Assert.AreEqual(expected, scope.Count);
        }


        [PatternTestMethod("SetValue(...) adds IDisposable to scope"), TestCategory(LIFETIME_MANAGER)]
        [DynamicData(nameof(Lifetime_Disposable_Data))]
        public void SetValueAddsLifetime(string name, Func<LifetimeManager> factory, bool isDisposable)
        {
            // Arrange
            var scope = new LifetimeContainer();
            var disposable = new TestDisposable();
            var manager = factory();

            // Act
            manager.SetTestValue(disposable, scope);

            if (0 == scope.Count) return;

            foreach (IDisposable item in scope) item.Dispose();

            Assert.IsTrue(disposable.IsDisposed);
        }



        [PatternTestMethod("Supports multiple Dispose() calls"), TestCategory(LIFETIME_MANAGER)]
        [DynamicData(nameof(Lifetime_Disposable_Data))]
        public void ImmuneToMultiDisposes(string name, Func<LifetimeManager> factory, bool isDisposable)
        {
            // Arrange
            var scope = new LifetimeContainer();
            var disposable = new TestDisposable(2);
            var manager = factory();

            // Act
            manager.SetTestValue(disposable, scope);

            if (0 == scope.Count) return;

            foreach (IDisposable item in scope) item.Dispose();
            foreach (IDisposable item in scope) item.Dispose();

            Assert.IsTrue(disposable.IsDisposed);
        }
    }
}
