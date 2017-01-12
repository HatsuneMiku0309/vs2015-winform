﻿
namespace Serene1.Northwind
{
    using Serenity;

    public class CustomerOrderDialog : OrderDialog
    {
        protected override void UpdateInterface()
        {
            base.UpdateInterface();

            EditorUtils.SetReadOnly(form.CustomerID, true);
        }
    }
}