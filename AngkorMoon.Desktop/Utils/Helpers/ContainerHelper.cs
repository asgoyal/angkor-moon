using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Services;
using Microsoft.Practices.Unity;

namespace AngkorMoon.Desktop.Utils.Helpers
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<ICommandHandler, DefaultCommandHandler>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IItemRepository, ItemRepository>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container => _container;
    }
}
