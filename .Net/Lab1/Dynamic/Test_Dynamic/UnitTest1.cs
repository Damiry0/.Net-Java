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

        [Test]
        public void KnapSack_CheckObjectsWithSameSeed_ShouldBeEqual()
        {
            // Arrange
            var backpack1 = new Bag(30);
            var backpack2 = new Bag(30);
            

            // Act
            backpack1.GenerateRandomBackpack(100);

            backpack2.GenerateRandomBackpack(100);
            
            // Assert

            backpack1.Items.Should().BeEquivalentTo(backpack2.Items);
        }

        [Test]
        public void KnapSack_CheckSortingOrder_ShouldntReturnSameObject()
        {
            var itemsDescending = new Bag(30);
            var itemsAscending = new Bag(30);

            // Arrange
            var items1 = new Bag(20);
            var backpack1 = new Bag(30);

            var items2 = new Bag(20);
            var backpack2 = new Bag(30);

            // Act
            items1.GenerateRandomBackpack(100);
            backpack1.KnapSack(items1);


            items2.GenerateRandomBackpack(100);
            items2.Items.Reverse();
            backpack2.KnapSack(items2);


            // Assert

            backpack1.Items.Should().NotBeEquivalentTo(backpack2.Items);
        }
    

        public void KnapSack_IfAtLeastOneItemMeetRequirments_ShouldReturnList()
        {

        }

    }
}