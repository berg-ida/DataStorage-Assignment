﻿using System.ComponentModel.DataAnnotations;

namespace Data.Dtos;

public class ManagerUpdateForm
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
