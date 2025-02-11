﻿using System.Runtime.InteropServices;

namespace DbsEnvManagementService.Models.Base
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
