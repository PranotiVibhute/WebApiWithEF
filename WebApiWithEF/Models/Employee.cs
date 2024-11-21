using System;
using System.Collections.Generic;

namespace WebApiWithEF.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public int? ManagerId { get; set; }

    public int? DepartmentId { get; set; }

    public int? ProjectId { get; set; }
}
