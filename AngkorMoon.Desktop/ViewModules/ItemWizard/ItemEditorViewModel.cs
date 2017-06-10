using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.Models;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Utils;
using AngkorMoon.Desktop.Utils.Commands;
using AngkorMoon.Desktop.Utils.Constants;
using AngkorMoon.Desktop.ViewModules.Items;

namespace AngkorMoon.Desktop.ViewModules.ItemWizard
{
    class ItemEditorViewModel : BindableBase
    {
        private ItemRepository _itemRepository;

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public event Action<string> Done = delegate { };

        public ItemEditorViewModel(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        private bool _editMode;
        public bool EditMode
        {
            get
            {
                return _editMode;
            }

            set
            {
                SetProperty(ref _editMode, value);
            }
        }

        private Item _editingItem = null;
        public void SetItem(Item item)
        {
            _editingItem = item;
            Item = new SimpleEditableItem();
            CopyItem(item, Item);
        }

        private SimpleEditableItem _item;
        public SimpleEditableItem Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        private void CopyItem(Item source, SimpleEditableItem target)
        {
            target.ItemId = source.ItemId;
            if (EditMode)
            {
                target.CompanyName = source.CompanyName;
                target.ItemName = source.ItemName;
                target.ItemType = source.ItemType;
                target.MaterialType = source.MaterialType;
                target.ThirdPartyItemCode = source.ThirdPartyItemCode;
                target.ItemPrice = source.ItemPrice;
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private void OnSave()
        {
            UpdateItem(Item, _editingItem);
            _itemRepository.Save();
            Done(ViewNames.ItemListView);
        }

        private void OnCancel()
        {
            Done(ViewNames.ItemListView);
        }

        private void UpdateItem(SimpleEditableItem source, Item target)
        {
            target.CompanyName = source.CompanyName;
            target.ItemName = source.ItemName;
            target.ItemType = source.ItemType;
            target.MaterialType = source.MaterialType;
            target.ThirdPartyItemCode = source.ThirdPartyItemCode;
            target.ItemPrice = source.ItemPrice;
        }
    }
}
