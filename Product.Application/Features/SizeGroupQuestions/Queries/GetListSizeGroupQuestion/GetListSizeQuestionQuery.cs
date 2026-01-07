namespace Product.Application.Features.SizeGroupQuestions.Queries.GetListSizeGroupQuestion
{
    public class GetListSizeQuestionQuery : IQuery<IEnumerable<SizeQuestionResponse>>
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public Guid? SizeGroupId { get; set; }
    }
}
