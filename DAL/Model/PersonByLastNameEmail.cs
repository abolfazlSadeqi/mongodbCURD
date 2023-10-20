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
public class PersonByLastNameEmail
{

   


    [BsonElement("LastName")]
    public string LastName { set; get; }

    

    [BsonElement("Email")]
    public string Email { set; get; }



}


