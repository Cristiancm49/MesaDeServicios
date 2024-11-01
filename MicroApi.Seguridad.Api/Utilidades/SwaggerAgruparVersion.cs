using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MicroApi.Seguridad.Api.Utilidades
{
    public class SwaggerAgruparVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var namespaceContralador = controller.ControllerType.Namespace; // Controller.Versiones.V1
            var versionAPI = namespaceContralador.Split('.').Last().ToLower(); // v1
            controller.ApiExplorer.GroupName = versionAPI;
        }
    }
}
