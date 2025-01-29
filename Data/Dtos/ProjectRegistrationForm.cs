﻿using Business.Models;

namespace Data.Dtos;

public class ProjectRegistrationForm
{
    public string Name { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string Status { get; set; } = null!;

    public Time TimePeriod { get; set; } = null!;

    public Manager ProjectManager { get; set; } = null!;

    public Client Client { get; set; } = null!;
}
