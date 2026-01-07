using System.ComponentModel;

namespace Product.Application.Features.SizeGroupQuestions.Commands.DeleteQuestions
{
    public class DeleteQuestionCommand:ICommand
    {
        [DisplayName("مجموعه الاسئله")]
        public Guid SizeGroupQuestionId { get; set; }
    }
}
