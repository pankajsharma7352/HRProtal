namespace HRPortal.Web
{
    public class EmployeeModel
    {
        public string PAndLDate { get; set; }
        public string AccountNo { get; set; }
        public string PLBalance { get; set; }
        public string Name { get; set; }
        public string DueDate { get; set; }
        public string Terms { get; set; }
        public string PaidIn { get; set; }
        public string LastPd { get; set; }
        public string PDue { get; set; }
        public string Col { get; set; }
        public string SeqNo { get; set; }
        public string PLCode { get; set; }
        
    }

    public class StoreTotal
    {
        public decimal Total { get; set; }
    }
}
