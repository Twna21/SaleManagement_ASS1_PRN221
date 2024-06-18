using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject;

public partial class Member
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
    public int MemberId { get; set; }

    public string? Email { get; set; }

    public string? CompanyName { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
