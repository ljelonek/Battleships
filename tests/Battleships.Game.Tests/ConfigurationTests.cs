using NUnit.Framework;
using System;

namespace Battleships.Game.Tests
{
    [TestFixture]
    internal class ConfigurationTests
    {
        [Test]
        public void Build_NoArguments_ReturnsDefaultConfiguration()
        {
            // Arrange
            var arguments = Array.Empty<string>();
            // Act
            var result = Configuration.Build(arguments);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.GridSize, Is.Not.EqualTo(default(int)));
            Assert.That(result.PaddingWidth, Is.Not.EqualTo(default(int)));
        }

        [Test]
        public void Build_InvalidGridSize_ThrowsArgumentException()
        {
            // Arrange
            var nicePaddingWidth = 2.ToString();
            var stringValue = Guid.NewGuid().ToString();
            var floatValue = float.Epsilon.ToString();
            var negativeValue = decimal.MinusOne.ToString();
            var stringValueArguments = new[] { stringValue, nicePaddingWidth };
            var floatValueArguments = new[] { floatValue, nicePaddingWidth };
            var negativeValueArguments = new[] { negativeValue, nicePaddingWidth };
            // Act & Assert
            Assert.That(() => Configuration.Build(stringValueArguments), Throws.ArgumentException);
            Assert.That(() => Configuration.Build(floatValueArguments), Throws.ArgumentException);
            Assert.That(() => Configuration.Build(negativeValueArguments), Throws.ArgumentException);
        }

        [Test]
        public void Build_InvalidPaddingWidth_ThrowsArgumentException()
        {
            // Arrange
            var niceGridSize = 10.ToString();
            var stringValue = Guid.NewGuid().ToString();
            var floatValue = float.Epsilon.ToString();
            var negativeValue = decimal.MinusOne.ToString();
            var stringValueArguments = new[] { niceGridSize, stringValue };
            var floatValueArguments = new[] { niceGridSize, floatValue };
            var negativeValueArguments = new[] { niceGridSize, negativeValue };
            // Act & Assert
            Assert.That(() => Configuration.Build(stringValueArguments), Throws.ArgumentException);
            Assert.That(() => Configuration.Build(floatValueArguments), Throws.ArgumentException);
            Assert.That(() => Configuration.Build(negativeValueArguments), Throws.ArgumentException);
        }

        [Test]
        public void Build_GridSizeOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var nicePaddingWidth = 2.ToString();
            var notNiceGridSize = int.MaxValue.ToString();
            var arguments = new[] { notNiceGridSize, nicePaddingWidth };
            // Act & Assert
            Assert.That(() => Configuration.Build(arguments), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        }

        [Test]
        public void Build_PaddingWidthOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var niceGridSize = 10.ToString();
            var notNicePaddingWidth = int.MaxValue.ToString();
            var arguments = new[] { niceGridSize, notNicePaddingWidth };
            // Act & Assert
            Assert.That(() => Configuration.Build(arguments), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        }
    }
}
