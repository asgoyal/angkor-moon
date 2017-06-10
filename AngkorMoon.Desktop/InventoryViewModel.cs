using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.DatabaseContexts;
using AngkorMoon.DataModel.Models;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Services;
using AngkorMoon.Desktop.Utils;
using AngkorMoon.Desktop.Utils.Commands;
using AngkorMoon.Desktop.Utils.Constants;
using AngkorMoon.Desktop.Utils.Helpers;
using AngkorMoon.Desktop.ViewModules.Items;
using AngkorMoon.Desktop.ViewModules.ItemWizard;
using AngkorMoon.Desktop.ViewModules.Parts;
using Microsoft.Practices.Unity;

namespace AngkorMoon.Desktop
{
    class InventoryViewModel : BindableBase
    {
        private ItemListViewModel _itemListViewModel;
        private PartListViewModel _partListViewModel;
        private ItemEditorViewModel _itemEditorViewModel;
        private IItemRepository _itemRepo;

        private BindableBase _currentViewModel;

        public RelayCommand<string> NavCommand { get; private set; }

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public InventoryViewModel(ICommandHandler commandHandler, IItemRepository itemRepo)
            : base(commandHandler)
        {
            // NOTE this is very important for database to work
            Database.SetInitializer<SQLServerDbContext>(new DropCreateDatabaseIfModelChanges<SQLServerDbContext>());

            _itemRepo = itemRepo;
            _itemListViewModel = ContainerHelper.Container.Resolve<ItemListViewModel>();
            _partListViewModel = ContainerHelper.Container.Resolve<PartListViewModel>();
            _itemEditorViewModel = ContainerHelper.Container.Resolve<ItemEditorViewModel>();

            NavCommand = new RelayCommand<string>(OnNav);

            _itemListViewModel.AddItemRequested += OnNav;
            _itemListViewModel.EditItemRequested += NavToEditItemViewModel;
            _itemListViewModel.NavRequested += OnNav;
            _itemEditorViewModel.Done += OnNav;
        }

        protected override void RegisterActions(ICommandHandler handler)
        {
            base.RegisterActions(handler);
            handler.RegisterAction<string>("AddItemCommand", OnNav);
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case ViewNames.ItemAddView:
                    _itemEditorViewModel.EditMode = false;
                    _itemEditorViewModel.SetItem(_itemRepo.New<Jewelry>());
                    CurrentViewModel = _itemEditorViewModel;
                    break;
                case ViewNames.PartListView:
                    CurrentViewModel = _partListViewModel;
                    break;
                case ViewNames.ItemListView:
                default:
                    CurrentViewModel = _itemListViewModel;
                    break;
            }
        }

        private void NavToEditItemViewModel(Item item)
        {
            _itemEditorViewModel.EditMode = true;
            _itemEditorViewModel.SetItem(_itemRepo.Get(item.ItemId));
            CurrentViewModel = _itemEditorViewModel;
        }
    }
}
