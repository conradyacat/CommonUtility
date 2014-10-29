using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Utility.Text.Tests
{
    /// <summary>
    /// Summary description for StringExpressionBuilderFixture
    /// </summary>
    [TestClass]
    public class StringExpressionBuilderFixture
    {
        [TestMethod]
        public void ShouldCreateEqualsExpression()
        {
            // arrange
            string expected = " Field1 = '1' ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.Equals("Field1", "1").Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 = 1 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.Equals("Field1", 1).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 = 1.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.Equals("Field1", 1.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateNotEqualsExpression()
        {
            // arrange
            string expected = " Field1 <> '1' ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.NotEquals("Field1", "1").Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 <> 1 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.NotEquals("Field1", 1).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 <> 1.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.NotEquals("Field1", 1.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateLessThanExpression()
        {
            // arrange
            string expected = " Field1 < '1' ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.LessThan("Field1", "1").Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 < 1 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.LessThan("Field1", 1).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 < 1.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.LessThan("Field1", 1.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateLessThanOrEqualsExpression()
        {
            // arrange
            string expected = " Field1 <= '1' ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.LessThanOrEquals("Field1", "1").Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 <= 1 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.LessThanOrEquals("Field1", 1).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 <= 1.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.LessThanOrEquals("Field1", 1.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateGreaterThanExpression()
        {
            // arrange
            string expected = " Field1 > '1' ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.GreaterThan("Field1", "1").Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 > 1 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.GreaterThan("Field1", 1).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 > 1.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.GreaterThan("Field1", 1.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateGreaterThanOrEqualsExpression()
        {
            // arrange
            string expected = " Field1 >= '1' ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.GreaterThanOrEquals("Field1", "1").Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 >= 1 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.GreaterThanOrEquals("Field1", 1).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 >= 1.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.GreaterThanOrEquals("Field1", 1.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateMixExpressions()
        {
            // arrange
            string expected = " Field1 = '1'  AND  Field2 = 2  AND  Field3 = 3.00 ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.Equals("Field1", "1").And()
                .Equals("Field2", 2).And()
                .Equals("Field3", 3.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 = '1'  OR  Field2 = 2  OR  Field3 = 3.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.Equals("Field1", "1").Or()
                .Equals("Field2", 2).Or()
                .Equals("Field3", 3.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            expected = " Field1 = '1'  OR  Field2 = 2  AND  Field3 = 3.00 ";

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.Equals("Field1", "1").Or()
                .Equals("Field2", 2).And()
                .Equals("Field3", 3.00m).Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldRemoveExcessConjunction()
        {
            // arrange
            string expected = " Field1 = '1'  AND  Field2 = 2  AND  Field3 = 3.00 ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.Equals("Field1", "1").And()
                .Equals("Field2", 2).And()
                .Equals("Field3", 3.00m).And().Build();

            // assert
            Assert.AreEqual(expected, actual);

            // act
            exprBuilder = new StringExpressionBuilder();
            actual = exprBuilder.Equals("Field1", "1").And()
                .Equals("Field2", 2).And()
                .Equals("Field3", 3.00m).Or().Build();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldCreateEqualsParamNameExpression()
        {
            // arrange
            string expected = " Field1 = @Field1 ";

            // act
            var exprBuilder = new StringExpressionBuilder();
            string actual = exprBuilder.EqualsParamName("Field1", "Field1").Build();

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
