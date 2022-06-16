using Battleships.Game.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace Battleships.Game.Tests.Models
{
    [TestFixture]
    internal class PointTests
    {
        [Test]
        public void CreateHorizontalRange_PointOnEdge_ReturnsValidArray()
        {
            // Arrange
            const int GridSize = 25;
            const int Length = 5;
            var range = Enumerable.Range(0, GridSize).ToArray();
            var pointOnLeft = new Point(Chars.A, range[0]);
            var pointOnRight = new Point(Chars.A, range[GridSize - 1]);
            // Act
            var rangeFromPointOnLeft = pointOnLeft.CreateHorizontalRange(Length, GridSize);
            var rangeFromPointOnRight = pointOnRight.CreateHorizontalRange(Length, GridSize);
            // Assert
            Assert.That(rangeFromPointOnLeft, Is.Not.Null);
            Assert.That(rangeFromPointOnLeft.MaxBy(x => x.ColumnIndex)!.ColumnIndex, Is.EqualTo(Length - 1));
            Assert.That(rangeFromPointOnRight, Is.Not.Null);
            Assert.That(rangeFromPointOnRight.MinBy(x => x.ColumnIndex)!.ColumnIndex, Is.EqualTo(GridSize - Length - 1));
        }

        [Test]
        public void CreateHorizontalRange_LengthExceedsGridWidth_ReturnsEmptyArray()
        {
            // Arrange
            const int GridSize = 10;
            const int Length = 11;
            var range = Enumerable.Range(0, GridSize).ToArray();
            var point = new Point(Chars.A, range[0]);
            // Act
            var rangeFromPoint = point.CreateHorizontalRange(Length, GridSize);
            // Assert
            Assert.That(rangeFromPoint, Is.Not.Null);
            Assert.That(rangeFromPoint.Length, Is.Zero);
        }

        [Test]
        public void CreateVerticalRange_PointOnEdge_ReturnsValidArray()
        {
            // Arrange
            const int GridSize = 25;
            const int Length = 5;
            var range = Enumerable.Range(Chars.A, GridSize).ToArray();
            var pointOnTop = new Point((char)range[0], 0);
            var pointOnBottom = new Point((char)range[GridSize - 1], 0);
            // Act
            var rangeFromPointOnTop = pointOnTop.CreateVerticalRange(Length, GridSize);
            var rangeFromPointOnBottom = pointOnBottom.CreateVerticalRange(Length, GridSize);
            // Assert
            Assert.That(rangeFromPointOnTop, Is.Not.Null);
            Assert.That(rangeFromPointOnTop.MaxBy(x => x.RowIdentifier)!.RowIdentifier, Is.EqualTo(Chars.A + (Length - 1)));
            Assert.That(rangeFromPointOnBottom, Is.Not.Null);
            Assert.That(rangeFromPointOnBottom.MinBy(x => x.RowIdentifier)!.RowIdentifier, Is.EqualTo(Chars.A + (GridSize - Length - 1)));
        }

        [Test]
        public void CreateVerticalRange_LengthExceedsGridLength_ReturnsEmptyArray()
        {
            // Arrange
            const int GridSize = 10;
            const int Length = 11;
            var range = Enumerable.Range(Chars.A, GridSize).ToArray();
            var point = new Point((char)range[0], 0);
            // Act
            var rangeFromPoint = point.CreateVerticalRange(Length, GridSize);
            // Assert
            Assert.That(rangeFromPoint, Is.Not.Null);
            Assert.That(rangeFromPoint.Length, Is.Zero);
        }

        [Test]
        public void TryParse_InvalidInput_ReturnsFalse()
        {
            // Arrange
            const int GridSize = 20;
            const string TooShortRowIdentifier = "+12";
            const string TooLongRowIdentifier = "Z12";
            const string TooShortColumnIndex = "K0";
            const string TooLongColumnIndex = "K25";
            const string ValidInputWithJunk = "K12a";
            var grid = new Grid(GridSize);
            var junkInput = Guid.NewGuid().ToString();
            // Act
            var tooShortRowIdentifierResult = Point.TryParse(TooShortRowIdentifier, grid, out _);
            var tooLongRowIdentifierResult = Point.TryParse(TooLongRowIdentifier, grid, out _);
            var tooShortColumnIndexResult = Point.TryParse(TooShortColumnIndex, grid, out _);
            var tooLongColumnIndexResult = Point.TryParse(TooLongColumnIndex, grid, out _);
            var validInputWithJunkResult = Point.TryParse(ValidInputWithJunk, grid, out _);
            var junkInputResult = Point.TryParse(junkInput, grid, out _);
            var nullResult = Point.TryParse(null, grid, out _);
            var emptyStringResult = Point.TryParse(string.Empty, grid, out _);
            // Assert
            Assert.That(tooShortRowIdentifierResult, Is.False);
            Assert.That(tooLongRowIdentifierResult, Is.False);
            Assert.That(tooShortColumnIndexResult, Is.False);
            Assert.That(tooLongColumnIndexResult, Is.False);
            Assert.That(validInputWithJunkResult, Is.False);
            Assert.That(junkInputResult, Is.False);
            Assert.That(nullResult, Is.False);
            Assert.That(emptyStringResult, Is.False);
        }

        [Test]
        public void TryParse_ValidInput_ReturnsTrue()
        {
            // Arrange
            const int GridSize = 20;
            const string ValidInput = "K12";
            var grid = new Grid(GridSize);
            // Act
            var validInputResult = Point.TryParse(ValidInput, grid, out _);
            // Assert
            Assert.That(validInputResult, Is.True);
        }
    }
}
