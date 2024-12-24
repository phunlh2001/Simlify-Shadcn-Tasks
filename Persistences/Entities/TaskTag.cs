﻿namespace TaskManagement.Persistences.Entities
{
    public class TaskTag
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public TaskModel Task { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
