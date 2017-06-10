using System;
using System.Data.Entity;
using AngkorMoon.DataModel.Models;

namespace AngkorMoon.DataModel.Repositories
{
    public class ItemRepository : SQLServerEntityRepository<Item>, IItemRepository
    {
        private static ItemRepository _instance = new ItemRepository();
        public static ItemRepository Instance => _instance;

        public override DbSet<Item> DbSet => DbContext.Items;
    }
}
