using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookingSystem.Data.Common
{
    public class EntityBase: Entity, ICreatedOn, IModifiedOn
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DeletedOn { get; set; }

        public string DeletedBy { get; set; }
    }
}
