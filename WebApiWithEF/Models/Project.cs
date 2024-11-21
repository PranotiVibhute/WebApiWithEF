using System;
using System.Collections.Generic;

namespace WebApiWithEF.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string? ProjectName { get; set; }
}
