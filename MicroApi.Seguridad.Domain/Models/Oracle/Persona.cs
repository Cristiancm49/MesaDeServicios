using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Oracle
{
    public class Persona
    {
        public int PeGe_Id { get; set; } // PersonaGeneral
        public string PeGe_DocumentoIdentidad { get; set; } // PersonaGeneral
        public string PeNG_PrimerApellido { get; set; } // PersonaNaturalGeneral
        public string PeNG_SegundoApellido { get; set; } // PersonaNaturalGeneral
        public string PeNG_PrimerNombre { get; set; } // PersonaNaturalGeneral
        public string PeNG_SegundoNombre { get; set; } // PersonaNaturalGeneral
    }
}
