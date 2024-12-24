﻿using TaskManagement.Persistences.Entities;

namespace TaskManagement.Presentations.DTOs.Request
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskModelStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public IList<TagView> Tags { get; set; }
    }

    public class TagView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
