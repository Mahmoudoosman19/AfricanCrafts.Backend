using Product.Application.SharedDTOs.CreatesQuestions;

namespace Product.Application.Features.SizeTable.Commands.CreatesSizeTable
{
    public class CreatesQuestionsCommand:ICommand
    {
        public List<CreatesQuestionsDto> Questions{ get; set; }
        public Guid SizeGroupId { get;  set; }

    }
}
