using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.DatabaseContexts;
using AngkorMoon.DataModel.Models;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Utils;
using AngkorMoon.Desktop.Utils.Commands;
using AngkorMoon.Desktop.Utils.Constants;
using AngkorMoon.Desktop.ViewModules.Items;
using AngkorMoon.Desktop.ViewModules.ItemWizard;
using AngkorMoon.Desktop.ViewModules.Parts;

namespace AngkorMoon.Desktop
{
    class InventoryViewModel : BindableBase
    {
        private ItemListViewModel _itemListViewModel;
        private PartListViewModel _partListViewModel;
        private ItemEditorViewModel _itemEditorViewModel;
        // TODO ItemRepository should be passed through dependency injection
        ItemRepository _ItemRepo = ItemRepository.Instance;

        private BindableBase _currentViewModel;

        public RelayCommand<string> NavCommand { get; private set; }

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public InventoryViewModel()
        {
            // NOTE this is very important for database to work
            Database.SetInitializer<SQLServerDbContext>(new DropCreateDatabaseIfModelChanges<SQLServerDbContext>());

            _itemListViewModel = new ItemListViewModel(_ItemRepo);
            _partListViewModel = new PartListViewModel();
            _itemEditorViewModel = new ItemEditorViewModel(_ItemRepo);

            NavCommand = new RelayCommand<string>(OnNav);

            _itemListViewModel.AddItemRequested += OnNav;
            _itemListViewModel.EditItemRequested += NavToEditItemViewModel;
            _itemListViewModel.NavRequested += OnNav;
            _itemEditorViewModel.Done += OnNav;

            Command.RegisterAction<string>("AddItemCommand", OnNav);
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case ViewNames.ItemAddView:
                    _itemEditorViewModel.EditMode = false;
                    _itemEditorViewModel.SetItem(_ItemRepo.New<Jewelry>());
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
            _itemEditorViewModel.SetItem(_ItemRepo.Get(item.ItemId));
            CurrentViewModel = _itemEditorViewModel;
        }
    }
}
