using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.Models;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Utils;
using AngkorMoon.Desktop.Utils.Commands;
using AngkorMoon.Desktop.Utils.Constants;

namespace AngkorMoon.Desktop.ViewModules.Items
{
    class ItemListViewModel : BindableBase
    {
        private ItemRepository _itemRepository;
        private ObservableCollection<Item> _items;

        public ItemListViewModel(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            NavCommand = new RelayCommand<string>(OnNav);
            EditItemCommand = new RelayCommand<Item>(OnEditItem);
        }

        public ObservableCollection<Item> Items
        {
            get
            {
                return _items;
            }

            set
            {
                SetProperty(ref _items, value);
            }
        }

        public async void LoadAllItems()
        {
            Items = new ObservableCollection<Item>(await _itemRepository.GetAllAsync());
        }

        public RelayCommand<string> NavCommand { get; private set; }
        public RelayCommand<Item> EditItemCommand { get; private set; }

        public event Action<string> NavRequested = delegate { };
        public event Action<string> AddItemRequested = delegate { };
        public event Action<Item> EditItemRequested = delegate { };

        private void OnNav(string destination)
        {
            NavRequested(ViewNames.PartListView);
        }

        private void OnEditItem(Item item)
        {
            EditItemRequested(item);
        }
    }
}
