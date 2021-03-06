﻿#region Using

using C4rm4x.WebApi.Framework.Specification;
using System;

#endregion

namespace C4rm4x.WebApi.Framework.Test.Specification
{
    public partial class OrSpecificationTest
    {
        #region Helper classes

        public class TestEntity { }

        class TestSpecification : ExpressionSpecification<TestEntity>
        {
            public TestSpecification(bool isSatisfiedBy)
                : base(
                      (entity) => isSatisfiedBy,
                      (entity) => new Exception())
            {
            }
        }

        class TestOrSpecification : OrSpecification<TestEntity>
        {
            public TestOrSpecification(
                bool isLeftSatisfiedBy,
                bool isRightSatisfiedBy)
                : base(
                      new TestSpecification(isLeftSatisfiedBy),
                      new TestSpecification(isRightSatisfiedBy), 
                      (entity) => new Exception())
            {
            }
        }

        #endregion

        protected static ISpecification<TestEntity> GetSubjectUnderTest(
            bool isLeftSatisfiedBy = true,
            bool isRightSatisfiedBy = true)
        {
            return new TestOrSpecification(isLeftSatisfiedBy, isRightSatisfiedBy);
        }
    }
}
