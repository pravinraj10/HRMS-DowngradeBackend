namespace GEOMASTER.Models
{
    using System;
    using System.Collections.Generic;

    namespace GEOMASTER.Models
    {
        public partial class Tblrole
        {
            public int Id { get; set; }

            public string RoleName { get; set; } = null!;

            public int DepartmentId { get; set; }

            public string? RoleType { get; set; }

            public string? Description { get; set; }

            public bool IsActive { get; set; }

            public bool IsDeleted { get; set; }

            public DateTime? CreatedAt { get; set; }

            public DateTime? UpdatedAt { get; set; }

            public string? CreatedBy { get; set; }

            public string? UpdatedBy { get; set; }

            //  Navigation Properties
            public virtual Tbldepartment? Department { get; set; }

            public virtual ICollection<Tblemployee> Employees { get; set; }
                = new List<Tblemployee>();

        }
    }
}
