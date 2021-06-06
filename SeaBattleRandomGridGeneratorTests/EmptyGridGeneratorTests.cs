using NUnit.Framework;
using SeaBattleRandomGridGenerator;

namespace SeaBattleRandomGridGeneratorTests
{
    [TestFixture]
    public class EmptyGridGeneratorTests
    {
        [Test]
        public void GenerateEmptyGridOfSize_Input10_ReturnsEmptyGridOfSize10()
        {
            const int gridSize = 10;
            var rowOfFalse = new[] { false, false, false, false, false, false, false, false, false, false };

            var result = EmptyGridGenerator.GenerateEmptyGridOfSize(gridSize);

            Assert.That(result.Length, Is.EqualTo(gridSize));

            foreach (var row in result)
            {
                Assert.That(row, Is.EqualTo(rowOfFalse));
            }
        }
    }
}