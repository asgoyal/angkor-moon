using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AngkorMoon.DataModel.Models
{
    public class Item : IModificationHistory
    {
        public long ItemId { get; set; }
        public string ThirdPartyItemCode { get; set; }
        public string CompanyName { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string MaterialType { get; set; }
        public decimal ItemPrice { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public bool IsDirty { get; set; }

        private IList<Item> _findings = new List<Item>();

        // virtual is used to indicate to Entity framework for lazy loading
        public virtual IList<Item> Findings
        {
            get
            {
                List<string> list = new List<string>();
                return _findings;
            }

            set
            {
                _findings = value;
            }
        }

        public string ProductCode
        {
            get
            {
                StringBuilder productIdStringBuilder = new StringBuilder();
                productIdStringBuilder.Append(CompanyInitials);
                productIdStringBuilder.Append(ItemType);
                productIdStringBuilder.Append(MaterialType);
                productIdStringBuilder.Append(
                    !string.IsNullOrEmpty(ThirdPartyItemCode) ? ThirdPartyItemCode
                    : ItemId.ToString());

                return productIdStringBuilder.ToString();
            }
        }

        protected string CompanyInitials  
        {
            get
            {
                if (string.IsNullOrEmpty(CompanyName))
                {
                    throw new NullReferenceException("Company Name is not specified");
                }

                StringBuilder companyInitialsStringBuilder = new StringBuilder();

                foreach (string companySubName in CompanyName.Split(' '))
                {
                    companyInitialsStringBuilder.Append(Char.ToUpper(companySubName[0]));
                }

                return companyInitialsStringBuilder.ToString();
            }
        }
    }
}
