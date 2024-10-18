using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroApi.Seguridad.Domain.Models.Mongo
{
    public class Evidencia
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Inci_Id { get; set; }
        public string Soporte { get; set; }
        public DateTime FechaCargue { get; set; }
    }
}