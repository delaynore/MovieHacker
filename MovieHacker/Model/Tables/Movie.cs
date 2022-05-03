﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.Tables
{
    public class Movie
    {
        public int Id { get; set; }
        public string? MovieName { get; set; }
        public Genre? Genre { get; set; }
        public int DurationInMinutes { get; set; }
    }
}