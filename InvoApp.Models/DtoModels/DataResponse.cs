﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.DtoModels
{
    public class DataResponse<T>
    {
        public bool Status { get; set; }
        public string? StatusMessage { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}