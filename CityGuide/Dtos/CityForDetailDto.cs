﻿using CityGuide.Models;
using System.Collections.Generic;

namespace CityGuide.Dtos
{
    public class CityForDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos{ get; set; }
    }
}
