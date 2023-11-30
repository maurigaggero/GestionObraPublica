namespace GOP.Client.Pages.Helpers
{
    public struct SelectorMultipleModel
    {
        public SelectorMultipleModel(int clave, string valor)
        {
            Clave = clave;
            Valor = valor;
        }

        public int Clave { get; set; }
        public string Valor { get; set; }
    }
}
