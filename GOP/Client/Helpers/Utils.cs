using GOP.BD.Data.Entity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace GOP.Client.Helpers
{
    public class Utils
    {
        private static IJSRuntime JS;   
        public Utils(IJSRuntime runtime)
        {
            JS = runtime;
        }

        public static async Task<DocumentoDTO> FileToBase64(IReadOnlyList<IBrowserFile> files)
        {
            string base64String = "";
            IBrowserFile archivo;
            DocumentoDTO documento = new DocumentoDTO();  
            
            foreach (var file in files)
            {
                if (Path.GetExtension(file.Name).Contains("jpeg") || Path.GetExtension(file.Name).Contains("jpg"))
                    archivo = await file.RequestImageFileAsync("jpeg", 500, 500);
                else
                    archivo = file;

                if (archivo.Size > 0)
                {
                    await using MemoryStream fs = new MemoryStream();
                    await archivo.OpenReadStream(maxAllowedSize: 100000000).CopyToAsync(fs);
                    FileToBytes convert = new FileToBytes();
                    byte[] somBytes = convert.GetBytes(fs);
                    base64String = Convert.ToBase64String(somBytes, 0, somBytes.Length);
                    documento.PathDoc = Path.GetFileName(archivo.Name);
                    documento.Extension = Path.GetExtension(archivo.Name);
                }
                else
                    await JS.InvokeVoidAsync("simple", "Archivo vacio", "El archivo no posee informacion.", "info");
            }

            documento.File = base64String;

            return documento;
        }
    }
}
