using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model;

[BsonIgnoreExtraElements]
public class Person
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { set; get; }

    [BsonElement("FirstName")]
    public string FirstName { set; get; }

    [BsonElement("LastName")]
    public string LastName { set; get; }

    [BsonElement("Suffix")]
    public string Suffix { set; get; }

    [BsonElement("Email")]
    public string Email { set; get; }

    [BsonElement("AdditionalContactInfo")]
    public string AdditionalContactInfo { set; get; }

    [BsonElement("ModifiedDate")]
    public DateTime ModifiedDate { set; get; }


    [BsonElement("CreateDate")]
    public DateTime CreateDate { set; get; }


}


