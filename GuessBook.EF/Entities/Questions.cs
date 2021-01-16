using System;
using System.Collections.Generic;

namespace GuessBook.EF.Entities
{
    public partial class Questions
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public string Qlink { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte? CategoryId { get; set; }
        public string Text { get; set; }
        public byte? QType { get; set; }
        public byte? QTypeMin { get; set; }
        public byte? QTypeMax { get; set; }
        public double? MaxSelect { get; set; }
        public double? MinSelect { get; set; }
        public double? IncrementSelect { get; set; }
        public double? Score { get; set; }
        public int? TotalAnswer { get; set; }
        public byte? SponsorIndex { get; set; }
        public string PhotoUrl { get; set; }
        public string Cat1Parents { get; set; }
    }
}
