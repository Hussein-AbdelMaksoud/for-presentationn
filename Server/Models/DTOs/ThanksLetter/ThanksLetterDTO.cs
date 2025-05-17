namespace Server.Models.DTOs.ThanksLetter
{
    public class ThanksLetterDTO
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public String EmpId { get; set; }
        public string? LetterName { get; set; }
        public string? Geha { get; set; }
        public DateOnly? DecisionDate { get; set; }
        public int? DecisionNo { get; set; }
    }
}
