using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.DataModel.Models;
using AngkorMoon.DataModel.Repositories;
using AngkorMoon.Desktop.Services;
using AngkorMoon.Desktop.Utils;
using AngkorMoon.Desktop.Utils.Commands;
using AngkorMoon.Desktop.Utils.Constants;
using AngkorMoon.Desktop.ViewModules.Items;

namespace AngkorMoon.Desktop.ViewModules.ItemWizard
{
    class ItemEditorViewModel : BindableBase
    {
        private IItemRepository _itemRepository;
        
        public ItemEditorViewModel(ICommandHandler commandHandler, IItemRepository itemRepository)
            : base(commandHandler)
        {
            _itemRepository = itemRepository;
        }

        private bool _editMode;
        public bool EditMode
        { get{ return _editMode; }
          set{ SetProperty(ref _editMode, value); }
        }

        private Item _editingItem = null;
        public void SetItem(Item item)
        {
            _editingItem = item;
            Item = new SimpleEditableItem(this.CommandHandler);
            CopyItem(item, Item);
        }

        private SimpleEditableItem _item;
        public SimpleEditableItem Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        protected override void RegisterActions(ICommandHandler handler)
        {
            handler.RegisterAction(CommandNames.CancelCommand, OnCancel);
            handler.RegisterAction(CommandNames.SaveCommand, OnSave);
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

        private void OnSave()
        {
            UpdateItem(Item, _editingItem);
            _itemRepository.Save();
            CommandHandler.DelegateAction(CommandNames.NavCommand, ViewNames.ItemListView);
        }

        private void OnCancel()
        {
            _editingItem.IsDirty = true;
            CommandHandler.DelegateAction(CommandNames.NavCommand, ViewNames.ItemListView);
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
