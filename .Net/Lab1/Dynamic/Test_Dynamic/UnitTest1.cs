using NUnit.Framework;
using Dynamic;
using FluentAssertions;

namespace Test_Dynamic
{
    public class Tests
    {
        [Test]
        public void KnapSack_CheckEmptyBackpack_ShouldReturnNothing()
        {
            // Arrange
            var items = new Bag(0);
            var backpack = new Bag(30);

            // Act
            items.GenerateRandomBackpack(100);
            backpack.KnapSack(items);

            // Assert
            backpack.Items.Should().BeEmpty();
        }

        public void KnapSack_CheckObjectsWithSameSeed_ShouldBeEqual()
        {

        }


        public void KnapSack_CheckSortingOrder_ShouldntReturnSameObject()
        {
            var itemsDescending = new Bag(30);
            var itemsAscending = new Bag(30);
        }

        public void KnapSack_IfAtLeastOneItemMeetRequirments_ShouldReturnList()
        {

        }

    }
}