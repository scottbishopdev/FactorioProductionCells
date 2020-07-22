using System;
using System.Collections.Generic;
using Xunit;
using FactorioProductionCells.Domain.Common;

namespace FactorioProductionCells.Domain.UnitTests.Common
{
    public class ValueObjectTests : ValueObject
    {
        private static FullName Sterling = new FullName { FirstName = "Sterling", LastName = "Archer" };
        private static FullName KriegerBotSterling = new FullName { FirstName = "Sterling", LastName = "Archer" };
        private static FullName Lana = new FullName { FirstName = "Lana", LastName = "Kane" };
        private static Coordinate OneTwo = new Coordinate { X = 1, Y = 2 };
        
        private static FullName JohnDoe = new FullName { FirstName = "John", LastName = "Doe" };
        private static FullName JohnNull = new FullName { FirstName = "John", LastName = null };
        private static FullName JohnNullClone = new FullName { FirstName = "John", LastName = null };

        #region EqualOperator
        [Fact]
        public void EqualOperator_WhenGivenDifferentValues_ShouldReturnFalse()
        {
            Assert.False(ValueObject.EqualOperator(Sterling, Lana));
        }

        [Fact]
        public void EqualOperator_WhenGivenMatchingValues_ShouldReturnTrue()
        {
            Assert.True(ValueObject.EqualOperator(Sterling, KriegerBotSterling));
        }

        [Theory]
        [MemberData(nameof(EqualOperator_Nulls))]
        public void EqualOperator_WhenSingleParameterIsNull_ReturnsFalse(FullName left, FullName right)
        {
            Assert.False(ValueObject.EqualOperator(left, right));
        }
        #endregion

        #region NotEqualOperator
        [Fact]
        public void NotEqualOperator_WhenGivenDifferentValues_ShouldReturnFalse()
        {
            Assert.True(ValueObject.NotEqualOperator(Sterling, Lana));
        }

        [Fact]
        public void NotEqualOperator_WhenGivenMatchingValues_ShouldReturnTrue()
        {
            Assert.False(ValueObject.NotEqualOperator(Sterling, KriegerBotSterling));
        }
        #endregion

        #region EqualsValueObject
        [Fact]
        public void EqualsValueObject_WhenGivenDifferentValue_ShouldReturnFalse()
        {
            Assert.False(Sterling.Equals(Lana));
        }

        [Fact]
        public void EqualsValueObject_WhenGivenMatchingValue_ShouldReturnTrue()
        {
            Assert.True(Sterling.Equals(KriegerBotSterling));
        }

        [Fact]
        public void EqualsValueObject_WhenGivenNull_ReturnsFalse()
        {
            Assert.False(Sterling.Equals(null));
        }

        [Fact]
        public void EqualsValueObject_WhenGivenDifferentType_ReturnsFalse()
        {
            Assert.False(Sterling.Equals(OneTwo));
        }

        [Fact]
        public void EqualsValueObject_WhenLeftContainsUnmatchedNull_ReturnsFalse()
        {
            Assert.False(JohnNull.Equals(JohnDoe));
        }

        [Fact]
        public void EqualsValueObject_WhenRightContainsUnmatchedNull_ReturnsFalse()
        {
            Assert.False(JohnDoe.Equals(JohnNull));
        }

        [Fact]
        public void EqualsValueObject_WhenBothContainMatchedNull_ReturnsTrue()
        {
            Assert.True(JohnNull.Equals(JohnNullClone));
        }
        #endregion

        #region EqualsObject
        [Fact]
        public void EqualsObject_WhenGivenDifferentValue_ShouldReturnFalse()
        {
            Assert.False(Sterling.Equals((object) Lana));
        }

        [Fact]
        public void EqualsObject_WhenGivenDifferentType_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => Sterling.Equals(new String("This should fail.")));
            Assert.Equal("Unable to compare the specified object to a ValueObject. (Parameter 'obj')", exception.Message);
        }

        [Fact]
        public void EqualsObject_WhenGivenMatchingValue_ShouldReturnTrue()
        {
            Assert.True(Sterling.Equals((object) KriegerBotSterling));
        }
        #endregion

        #region GetHashCode
        [Fact]
        public void GetHashCode_MatchingObjects_ReturnSameHashCode()
        {
            Assert.Equal(Sterling.GetHashCode(), KriegerBotSterling.GetHashCode());
        }

        [Fact]
        public void GetHashCode_MatchingObjectsWithNull_ReturnSameHashCode()
        {
            Assert.Equal(JohnNull.GetHashCode(), JohnNullClone.GetHashCode());
        }

        [Fact]
        public void GetHashCode_NonMatchingObjects_ReturnDifferentHashCodes()
        {
            Assert.NotEqual(Sterling.GetHashCode(), Lana.GetHashCode());
        }
        #endregion

        public static IEnumerable<object[]> EqualOperator_Nulls =>
            new List<object[]>
            {
                new object[] {Sterling, null},
                new object[] {null, Lana}
            };

        // Note: Since ValueObject is an abstract class, we need to create a mock concrete class for it in order to test the methods that are defined in it.
        public class FullName : ValueObject
        {
            public String FirstName { get; set; }
            public String LastName { get; set; }

            public override IEnumerable<object> GetAtomicValues()
            {
                yield return FirstName;
                yield return LastName;
            }
        }

        public class Coordinate : ValueObject
        {
            public Int32 X { get; set; }
            public Int32 Y { get; set; }

            public override IEnumerable<object> GetAtomicValues()
            {
                yield return X;
                yield return Y;
            }
        }

        // Note: This method implementation only exists because the test class needs to inherit from ValueObject in order to test its protected methods.
        public override IEnumerable<object> GetAtomicValues() { throw new InvalidOperationException("This method should never be executed. It only exists because the test class needs access to protected methods."); }
    }
}
