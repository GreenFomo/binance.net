using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.models {
    public class AccountInfo {
        public float MakerCommission { get; set; }
        public float TakerCommission { get; set; }
        public float BuyerCommission { get; set; }
        public float SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public List<AccountBalance> Balances { get; set; }		  
		  
		  
    }
}
