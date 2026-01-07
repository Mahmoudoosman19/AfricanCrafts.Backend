namespace Product.Application.Features.SizeGroupQuestions.Queries.GetListSizeGroupQuestion
{
    public class SizeQuestionResponse
    {
        public Guid Id { get; private set; }
        public string QuestionAr { get; private set; } 
        public string QuestionEn { get; private set; } 
        public Guid SizeGroupId { get; private set; }
        public string SizeGroupNameAr { get; set; }
        public string SizeGroupNameEn {  get; set; }    
    }
}
