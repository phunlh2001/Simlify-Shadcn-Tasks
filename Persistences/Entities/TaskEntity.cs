﻿using TaskManagement.Persistences.Enums;

namespace TaskManagement.Persistences.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskEntityStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<Tag> Tags { get; set; } = [];
        public List<TaskTag> TaskTags { get; set; } = [];
    }
}
