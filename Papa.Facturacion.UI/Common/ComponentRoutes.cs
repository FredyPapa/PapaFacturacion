namespace Papa.Facturacion.UI.Common
{
    public static class ComponentRoutes
    {
        public static class Clients
        {
            public const string List = "/maintenance/clients";
            public const string Create = "/maintenance/clients/create";
            public const string Edit = "/maintenance/clients/edit";
            public const string EditNav = "/maintenance/clients/edit/{id:int}";
        }

        public static class Products
        {
            public const string List = "/maintenance/products";
            public const string Create = "/maintenance/products/create";
            public const string Edit = "/maintenance/products/edit";
            public const string EditNav = "/maintenance/products/edit/{id:int}";
        }

        public static class Invoices
        {
            public const string List = "/maintenance/invoices";
            public const string Create = "/maintenance/invoices/create";
        }
    }
}
