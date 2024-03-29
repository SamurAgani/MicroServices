﻿using System;

namespace Course.Services.Discount.Model
{
    [Dapper.Contrib.Extensions.Table("discount")]
    public class Discount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
