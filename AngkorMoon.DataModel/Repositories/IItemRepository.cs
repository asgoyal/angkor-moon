using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.DatabaseContexts;
using AngkorMoon.DataModel.Models;

namespace AngkorMoon.DataModel.Repositories
{
    public interface IItemRepository : IRepository<SQLServerDbContext, Item>
    {
    }
}
