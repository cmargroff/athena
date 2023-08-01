using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.Scripts.UIViewModels
{
    public  class ShopVM : UIVM
    {
        [VisualElementBind]
        public string Coins;
        [VisualElementEventAttribute]
        public Action Done;

    }
}
