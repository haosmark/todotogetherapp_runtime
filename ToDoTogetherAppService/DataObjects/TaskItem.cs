using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoTogetherAppService.DataObjects
{
    public class TaskItem : EntityData
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public bool Complete { get; set; }
    }
}