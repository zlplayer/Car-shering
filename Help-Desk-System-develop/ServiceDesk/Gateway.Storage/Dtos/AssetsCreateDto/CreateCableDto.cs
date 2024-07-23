﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Storage.Dtos.AssetsCreateDto
{
    public class CreateCableDto : CreateAssetDto
    {
        public string CableType { get; set; }
        public int Length { get; set; }
        public string ConnectorType { get; set; }
        public bool IsShielded { get; set; }
    }
}