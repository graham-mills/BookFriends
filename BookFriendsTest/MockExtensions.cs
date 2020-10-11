using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BookFriendsTest
{
    public static class MockExtensions
    {
        public static void SetupGet<TEntity>(this Mock<IEntityRepository<TEntity>> mock,
            IEnumerable<TEntity> itemsInRepo,
            int expectedItemsToTake,
            int itemsToReturn) where TEntity : class
        {
            // Return correct amount of entities if correct amount are requested (taken)
            mock.Setup(x => x.Get(
                It.IsAny<Expression<Func<TEntity, bool>>>(),
                It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
                It.Is<int?>(t => t == expectedItemsToTake),
                It.IsAny<int?>())
            ).Returns(itemsInRepo.Take(itemsToReturn));
            // Returns no entities if incorrect amount are requested (taken)
            mock.Setup(x => x.Get(
                It.IsAny<Expression<Func<TEntity, bool>>>(),
                It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
                It.Is<int?>(t => t != expectedItemsToTake),
                It.IsAny<int?>())
            ).Returns(itemsInRepo.Take(0));
        }
    }
}
