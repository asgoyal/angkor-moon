using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.Desktop.Utils;

namespace AngkorMoon.Desktop.ViewModules.Items
{
    public class SimpleEditableItem : BindableBase
    {
        private long _itemId;
        public long ItemId
        {
            get { return _itemId; }
            set { SetProperty(ref _itemId, value); }
        }

        private string _companyName;
        //[Required]
        public string CompanyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }
        
        private string _itemName;
        //[Required]
        public string ItemName
        {
            get { return _itemName; }
            set { SetProperty(ref _itemName, value); }
        }

        private string _itemType;
        //[Required]
        public string ItemType
        {
            get { return _itemType; }
            set { SetProperty(ref _itemType, value); }
        }

        private string _materialType;
        //[Required]
        public string MaterialType
        {
            get { return _materialType; }
            set { SetProperty(ref _materialType, value); }
        }

        private string _thirdPartyItemCode;
        public string ThirdPartyItemCode
        {
            get { return _thirdPartyItemCode; }
            set { SetProperty(ref _thirdPartyItemCode, value); }
        }

        private decimal _itemPrice;
        public decimal ItemPrice
        {
            get { return _itemPrice; }
            set { SetProperty(ref _itemPrice, value); }
        }
    }
}
