using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDoTogetherAppService.DataObjects
{
    public class Project : EntityData
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<User> Collaborators { get; set; }
    }
}