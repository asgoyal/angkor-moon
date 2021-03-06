﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.Models;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Services;
using AngkorMoon.Desktop.Utils;
using AngkorMoon.Desktop.Utils.Commands;
using AngkorMoon.Desktop.Utils.Constants;

namespace AngkorMoon.Desktop.ViewModules.Items
{
    class ItemListViewModel : BindableBase
    {
        private IItemRepository _itemRepository;
        private ObservableCollection<Item> _items;

        public ItemListViewModel(ICommandHandler commandHandler, IItemRepository itemRepository)
            : base(commandHandler)
        {
            _itemRepository = itemRepository;
            NavCommand = new RelayCommand<string>(OnNav);
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

        public event Action<string> NavRequested = delegate { };

        private void OnNav(string destination)
        {
            NavRequested(ViewNames.PartListView);
        }
    }
}
