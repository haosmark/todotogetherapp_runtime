using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoTogetherAppService.DataObjects
{
    public class Assignment : EntityData
    {
        public string UserId { get; set; }
        public string ProjectId { get; set; }
    }
}