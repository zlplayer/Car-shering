﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Storage.Dtos.AssetsCreateDto
{
    public class CreatePrinterDto : CreateAssetDto
    {
        public bool IsColor { get; set; }
        public int PrintSpeed { get; set; }
        public string? PrinterType { get; set; }
        public bool HasScanner { get; set; }
        public int PaperCapacity { get; set; }
    }
}
