using System;


namespace Assets.Scripts.UIViewModels
{
    public  class ShopVM : UIVM
    {
        [VisualElementBind]
        public string Title;

        [VisualElementBind]
        public string Coins;
        [VisualElementEvent]
        public Action Done;

    }
}
